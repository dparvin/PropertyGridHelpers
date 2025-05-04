using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.UIEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Provides a type converter that restricts input to selectable values only.
    /// </summary>
    /// <remarks>
    /// This converter is used with the <see cref="ResourcePathEditor" /> to 
    /// prevent users from manually editing text in the PropertyGrid, while still 
    /// allowing selection from a predefined dropdown list.
    /// </remarks>
    /// <example>
    /// <code language="csharp">
    /// [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
    /// [LocalizedCategory("Category_TestItems")]
    /// [LocalizedDescription("Description_ResourcePath")]
    /// [LocalizedDisplayName("DisplayName_ResourcePath")]
    /// [Editor(typeof(ResourcePathEditor), typeof(UITypeEditor))]
    /// [TypeConverter(typeof(OnlySelectableTypeConverter))]
    /// [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    /// public string ResourcePath { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="StringConverter" />
    /// <seealso cref="AllowBlankAttribute"/>
    /// <seealso cref="ResourcePathEditor"/>
    public class OnlySelectableTypeConverter : StringConverter
    {
        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Value cannot be blank and no valid options are available.</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // If the incoming value is an empty string, or just white space
#if NET35
            if (value is string str && string.IsNullOrEmpty(str))
#else
            if (value is string str && string.IsNullOrWhiteSpace(str))
#endif
            {
                // check to see if blanks are allowed or not
                if (!AllowBlankAttribute.IsBlankAllowed(context))
                {
                    // If blank is not allowed, then we need to return the name of the first resource path
                    var first = GetStandardValues(context).Cast<string>().FirstOrDefault();
                    // If we have a resource path, return it, or throw an error if there are no resource paths available
                    return first != null ? (object)first : throw new ArgumentException("Value cannot be blank and no valid options are available.");
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Gets the standard values supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Gets the standard values exclusive.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Gets a list of resource paths.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var assembly = context?.Instance?.GetType().Assembly;
            if (assembly == null)
#if NET8_0_OR_GREATER || NET462_OR_GREATER
                return new StandardValuesCollection(Array.Empty<string>());
#else
                return new StandardValuesCollection(new string[0]);
#endif

            var resources = assembly.GetManifestResourceNames();
            var list = new List<string>();
            var prefix = $"{assembly.GetName().Name}.";

            foreach (var res in resources)
            {
                if (res.EndsWith(".resources", StringComparison.InvariantCultureIgnoreCase))
                {
#if NET8_0_OR_GREATER
                    var name = res[..^".resources".Length];
#else
                    var name = res.Substring(0, res.Length - ".resources".Length);
#endif
                    if (name.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
#if NET8_0_OR_GREATER
                        name = name[prefix.Length..];
#else
                        name = name.Substring(prefix.Length);
#endif
                    list.Add(name);
                }
            }

            return new StandardValuesCollection(list.OrderBy(s => s).ToArray());
        }
    }
}
