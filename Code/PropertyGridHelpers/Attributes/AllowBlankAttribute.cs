using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute to allow blank values for properties.
    /// </summary>
    /// <param name="allow">if set to <c>true</c> allow blank values in the property.</param>
    /// <param name="includeItem">if set to <c>true</c> include item in the dropdown to indicate blank.</param>
    /// <param name="resourceItem">The resource item to use for displaying the blank item (Leaves it empty when this is not provided).</param>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowBlankAttribute(bool allow = true, bool includeItem = false, string resourceItem = "") : Attribute
    {
#else

    /// <summary>
    /// Attribute to allow blank values for properties.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowBlankAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllowBlankAttribute" /> class.
        /// </summary>
        /// <param name="allow">if set to <c>true</c> allow blank values in the property.</param>
        /// <param name="includeItem">if set to <c>true</c> include item in the dropdown to indicate blank.</param>
        /// <param name="resourceItem">The resource item to use for displaying the blank item (Leaves it empty when this is not provided).</param>
        public AllowBlankAttribute(bool allow = true, bool includeItem = false, string resourceItem = "")
        {
            Allow = allow;
            IncludeItem = includeItem;
            ResourceItem = resourceItem;
        }
#endif

        /// <summary>
        /// Gets a value indicating whether this <see cref="AllowBlankAttribute"/> is allowing blank values.
        /// </summary>
        /// <value>
        ///   <c>true</c> if allow blank values; otherwise, <c>false</c>.
        /// </value>
        public bool Allow
        {
            get;
#if NET8_0_OR_GREATER
        } = allow;
#else
        }
#endif
        /// <summary>
        /// Gets a value indicating whether to include item an item in the dropdown to indicate blank.
        /// </summary>
        /// <value>
        ///   <c>true</c> if including an item in the dropdown to indicate blank; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeItem
        {
            get;
#if NET8_0_OR_GREATER
        } = includeItem;
#else
        }
#endif

        /// <summary>
        /// Gets the resource item to use for displaying the blank item (Leaves it empty when this is not provided).
        /// </summary>
        /// <value>
        /// The resource item to use for displaying the blank item (Leaves it empty when this is not provided).
        /// </value>
        public string ResourceItem
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceItem;
#else
        }
#endif
        /// <summary>
        /// Gets the allow blank attribute.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static AllowBlankAttribute GetAllowBlankAttribute(ITypeDescriptorContext context)
        {
            var prop = context?.PropertyDescriptor;
            return prop?.Attributes.OfType<AllowBlankAttribute>().FirstOrDefault();
        }
        /// <summary>
        /// Determines whether blank is allowed in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if blank is allowed in the specified context; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBlankAllowed(ITypeDescriptorContext context)
        {
            var prop = GetAllowBlankAttribute(context);
            return prop != null && prop.Allow;
        }

        /// <summary>
        /// Gets the blank label.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static string GetBlankLabel(
            ITypeDescriptorContext context)
        {
            if (context?.PropertyDescriptor == null || context.Instance == null)
                return string.Empty;

            // Check for AllowBlankAttribute on the property
            var allowBlankAttr = AllowBlankAttribute.GetAllowBlankAttribute(context);
            if (allowBlankAttr == null || !allowBlankAttr.Allow)
                return string.Empty;

            // Determine the baseName from the class's ResourcePathAttribute
            var resourcePathAttr = context.Instance.GetType()
                .GetCustomAttributes(typeof(ResourcePathAttribute), true);

            string baseName = null;
            if (resourcePathAttr.Length > 0 && resourcePathAttr[0] is ResourcePathAttribute rpa)
                baseName = rpa.ResourcePath;

            // If we want to include an item in the list and have a resource item name, use it
            if (allowBlankAttr.IncludeItem && !string.IsNullOrEmpty(allowBlankAttr.ResourceItem))
            {
                try
                {
                    if (!string.IsNullOrEmpty(baseName))
                    {
                        var manager = new ComponentResourceManager(context.Instance.GetType().Assembly.GetType(baseName));
                        var localized = manager.GetString(allowBlankAttr.ResourceItem, CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(localized))
                            return localized;
                    }
                }
                catch
                {
                    // Fall through to default
                }
            }

            // Default to "(none)" if no localized string is found or we aren't including the label
            return allowBlankAttr.IncludeItem ? "(none)" : string.Empty;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() =>
            $"AllowBlank: {Allow}, IncludeItem: {IncludeItem}, ResourceItem: '{ResourceItem}'";
    }
}