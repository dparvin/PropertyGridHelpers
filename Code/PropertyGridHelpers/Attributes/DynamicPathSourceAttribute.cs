using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute to point to a different property that contains the path to the resource
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="DynamicPathSourceAttribute"/> class.
    /// </remarks>
    /// <param name="pathPropertyName">Name of the path property.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DynamicPathSourceAttribute(string pathPropertyName) : Attribute
#else
    /// <summary>
    /// Attribute to point to a different property that contains the path to the resource
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="DynamicPathSourceAttribute"/> class.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DynamicPathSourceAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the name of the path property.
        /// </summary>
        /// <value>
        /// The name of the path property.
        /// </value>
        public string PathPropertyName
        {
            get;
#if NET8_0_OR_GREATER
        } = pathPropertyName;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicPathSourceAttribute"/> class.
        /// </summary>
        /// <param name="pathPropertyName">Name of the path property.</param>
        public DynamicPathSourceAttribute(string pathPropertyName) => PathPropertyName = pathPropertyName;
#endif
    }
}
