using PropertyGridHelpers.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace PropertyGridHelpers.PropertyDescriptors
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Localized Property Descriptor
    /// </summary>
    /// <param name="baseProperty">The base property.</param>
    /// <seealso cref="PropertyDescriptor" />
    public class LocalizedPropertyDescriptor(PropertyDescriptor baseProperty) : PropertyDescriptor(baseProperty)
    {
        private readonly PropertyDescriptor baseProperty = baseProperty;
#else
    /// <summary>
    /// Localized Property Descriptor
    /// </summary>
    /// <seealso cref="PropertyDescriptor" />
    public class LocalizedPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor baseProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedPropertyDescriptor"/> class.
        /// </summary>
        /// <param name="baseProperty">The base property.</param>
        public LocalizedPropertyDescriptor(PropertyDescriptor baseProperty)
            : base(baseProperty) =>
            this.baseProperty = baseProperty;
#endif
        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public override string Category =>
            GetLocalizedString<LocalizedCategoryAttribute>(baseProperty.Category);

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description =>
            GetLocalizedString<LocalizedDescriptionAttribute>(baseProperty.Description);

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public override string DisplayName =>
            GetLocalizedString<LocalizedDisplayNameAttribute>(baseProperty.DisplayName);

        /// <summary>
        /// Gets the localized string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
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
        /// Gets the resource type from property.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified component can reset its value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        ///   <c>true</c> if the specified component can reset its value; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanResetValue(object component) => baseProperty.CanResetValue(component);

        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override Type ComponentType => baseProperty.ComponentType;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public override object GetValue(object component) => baseProperty.GetValue(component);

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public override bool IsReadOnly => baseProperty.IsReadOnly;

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        public override Type PropertyType => baseProperty.PropertyType;

        /// <summary>
        /// Resets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        public override void ResetValue(object component) => baseProperty.ResetValue(component);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="value">The value.</param>
        public override void SetValue(object component, object value) => baseProperty.SetValue(component, value);

        /// <summary>
        /// Determines whether the value of the specified component should be serialized.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component) => baseProperty.ShouldSerializeValue(component);
    }
}
