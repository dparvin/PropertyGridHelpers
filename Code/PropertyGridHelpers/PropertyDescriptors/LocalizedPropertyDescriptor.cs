using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace PropertyGridHelpers.PropertyDescriptors
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// A specialized <see cref="PropertyDescriptor"/> that supports localizing
    /// the category, description, and display name of a property based on
    /// resource attributes.
    /// </summary>
    /// <param name="baseProperty">The wrapped base property descriptor to decorate.</param>
    /// <remarks>
    /// This class is intended to wrap a standard property descriptor
    /// and provide localized versions of its metadata (category, description,
    /// display name) using associated <see cref="LocalizedTextAttribute"/>-derived
    /// attributes. At runtime, it retrieves localized strings from the
    /// appropriate resource files configured through the <see cref="ResourcePathAttribute"/>.
    /// </remarks>
    /// <seealso cref="PropertyDescriptor"/>
    public class LocalizedPropertyDescriptor(PropertyDescriptor baseProperty) : PropertyDescriptor(baseProperty)
    {
        private readonly PropertyDescriptor baseProperty = baseProperty;
#else
    /// <summary>
    /// A specialized <see cref="PropertyDescriptor"/> that supports localizing
    /// the category, description, and display name of a property based on
    /// resource attributes.
    /// </summary>
    /// <remarks>
    /// This class is intended to wrap a standard property descriptor
    /// and provide localized versions of its metadata (category, description,
    /// display name) using associated <see cref="LocalizedTextAttribute"/>-derived
    /// attributes. At runtime, it retrieves localized strings from the
    /// appropriate resource files configured through the <see cref="ResourcePathAttribute"/>.
    /// </remarks>
    /// <seealso cref="PropertyDescriptor"/>
    public class LocalizedPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor baseProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedPropertyDescriptor"/> class.
        /// </summary>
        /// <param name="baseProperty">The wrapped base property descriptor to decorate.</param>
        public LocalizedPropertyDescriptor(PropertyDescriptor baseProperty)
            : base(baseProperty) =>
            this.baseProperty = baseProperty;
#endif
        /// <summary>
        /// Gets the localized category name for this property.
        /// </summary>
        public override string Category =>
            GetLocalizedString<LocalizedCategoryAttribute>(baseProperty.Category);

        /// <summary>
        /// Gets the localized description for this property.
        /// </summary>
        public override string Description =>
            GetLocalizedString<LocalizedDescriptionAttribute>(baseProperty.Description);

        /// <summary>
        /// Gets the localized display name for this property.
        /// </summary>
        public override string DisplayName =>
            GetLocalizedString<LocalizedDisplayNameAttribute>(baseProperty.DisplayName);

        /// <summary>
        /// Retrieves a localized string from the specified attribute type, or
        /// returns the default string if no localization can be resolved.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="LocalizedTextAttribute"/> to look for.
        /// </typeparam>
        /// <param name="defaultValue">The fallback value if localization fails.</param>
        /// <returns>
        /// The localized string if found; otherwise the default value.
        /// </returns>
        internal string GetLocalizedString<T>(string defaultValue) where T : LocalizedTextAttribute
        {
            if (baseProperty.Attributes[typeof(T)] is T localizedAttribute)
            {
                var resourcePath = string.Empty;
                var resourceAssembly = GetResourceTypeFromProperty(ref resourcePath);

                if (resourceAssembly != null && !string.IsNullOrEmpty(resourcePath))
                {
                    var resourceManager = new ResourceManager(resourcePath, resourceAssembly);
                    try
                    {
                        return resourceManager.GetString(localizedAttribute.ResourceKey, CultureInfo.CurrentCulture) ?? defaultValue;
                    }
                    catch (MissingManifestResourceException)
                    {
                        return defaultValue;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Determines the resource assembly and fully qualified resource path
        /// used to resolve localized strings for this property.
        /// </summary>
        /// <param name="resourcePath">
        /// Outputs the computed resource path to the relevant resource file.
        /// </param>
        /// <returns>
        /// The assembly containing the resource type if found; otherwise null.
        /// </returns>
        internal Assembly GetResourceTypeFromProperty(ref string resourcePath)
        {
            ResourcePathAttribute resourcePathAttribute = null;
#if NET35
            var attributes = baseProperty.ComponentType.GetCustomAttributes(typeof(ResourcePathAttribute), false);
            if (attributes.Length > 0)
                resourcePathAttribute = (ResourcePathAttribute)attributes[0];
#else
            resourcePathAttribute = baseProperty.ComponentType.GetCustomAttribute<ResourcePathAttribute>();
#endif
            if (resourcePathAttribute == null) return null;

            resourcePath = $"{baseProperty.ComponentType.Assembly.GetName().Name}.{resourcePathAttribute.ResourcePath}";

            return !string.IsNullOrEmpty(resourcePathAttribute.ResourceAssembly) ?
                   resourcePathAttribute.GetAssembly() :
                   baseProperty.ComponentType.Assembly;
        }

        /// <inheritdoc/>
        public override bool CanResetValue(object component) =>
            baseProperty.CanResetValue(component);

        /// <inheritdoc/>
        public override Type ComponentType =>
            baseProperty.ComponentType;

        /// <inheritdoc/>
        public override object GetValue(object component) =>
            baseProperty.GetValue(component);

        /// <inheritdoc/>
        public override bool IsReadOnly =>
            baseProperty.IsReadOnly;

        /// <inheritdoc/>
        public override Type PropertyType =>
            baseProperty.PropertyType;

        /// <inheritdoc/>
        public override void ResetValue(object component) =>
            baseProperty.ResetValue(component);

        /// <inheritdoc/>
        public override void SetValue(object component, object value) =>
            baseProperty.SetValue(component, value);

        /// <inheritdoc/>
        public override bool ShouldSerializeValue(object component) =>
            baseProperty.ShouldSerializeValue(component);
    }
}
