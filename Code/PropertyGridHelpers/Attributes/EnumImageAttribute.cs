using PropertyGridHelpers.Enums;
using PropertyGridHelpers.UIEditors;
using System;

namespace PropertyGridHelpers.Attributes
{
    /// <summary>
    /// Associates an image with an Enum field for display in a property grid.
    /// </summary>
    /// <remarks>
    /// This attribute allows an Enum field to have an associated image, which can be used
    /// in UI components such as property grids. The image is identified by a text-based key 
    /// and can be stored as an embedded resource or in an external location.
    /// 
    /// To display the image in a property grid, apply this attribute to the Enum items and 
    /// use <see cref="ImageTextUIEditor"/> as the UI editor for the corresponding property.
    /// </remarks>
    /// <example>
    /// <code>
    /// public enum Status
    /// {
    ///     [EnumImage("PendingIcon.png")]
    ///     Pending,
    ///
    ///     [EnumImage("ApprovedIcon.png")]
    ///     Approved,
    ///
    ///     [EnumImage("RejectedIcon.png", ImageLocation.External)]
    ///     Rejected
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Attribute"/>
    /// <seealso cref="ImageTextUIEditor"/>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumImageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class
        /// with default values (null image, embedded location).
        /// </summary>
        public EnumImageAttribute()
        {
            EnumImage = null;
            ImageLocation = ImageLocation.Embedded;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class
        /// with the specified image location.
        /// </summary>
        /// <param name="imageLocation">The storage location of the image (embedded or external).</param>
        public EnumImageAttribute(ImageLocation imageLocation)
        {
            EnumImage = null;
            ImageLocation = imageLocation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class
        /// with a specified image identifier and location.
        /// </summary>
        /// <param name="text">The name or resource key of the image.</param>
        /// <param name="imageLocation">The storage location of the image (default: embedded).</param>
        public EnumImageAttribute(string text, ImageLocation imageLocation = ImageLocation.Embedded)
        {
            EnumImage = text;
            ImageLocation = imageLocation;
        }

        /// <summary>
        /// Gets the name or resource key of the image associated with the Enum item.
        /// </summary>
        /// <value>
        /// The name or resource key of the image associated with the Enum item.
        /// </value>
        public string EnumImage
        {
            get;
        }

        /// <summary>
        /// Gets the storage location of the associated image.
        /// </summary>
        /// <value>
        /// The storage location of the associated image.
        /// </value>
        public ImageLocation ImageLocation
        {
            get;
        }

        /// <summary>
        /// Retrieves the name of the image associated with a given Enum value.
        /// </summary>
        /// <param name="value">The Enum value.</param>
        /// <returns>The image name if found, otherwise the Enum name.</returns>
        public static string GetEnumImage(Enum value)
        {
            var attribute = Get(value);
            if (attribute == null)
                return string.Empty;
            var result = attribute.EnumImage;
            if (string.IsNullOrEmpty(result))
                result = Enum.GetName(value.GetType(), value);
            return result;
        }

        /// <summary>
        /// Determines whether an <see cref="EnumImageAttribute"/> exists for the given Enum value.
        /// </summary>
        /// <param name="value">The Enum value.</param>
        /// <returns><c>true</c> if an image attribute is associated with the value; otherwise, <c>false</c>.</returns>
        public static bool Exists(Enum value) => Get(value) != null;

        /// <summary>
        /// Retrieves the <see cref="EnumImageAttribute"/> associated with a given Enum value.
        /// </summary>
        /// <param name="value">The Enum value.</param>
        /// <returns>The corresponding <see cref="EnumImageAttribute"/> instance, or <c>null</c> if none is found.</returns>
        public static EnumImageAttribute Get(Enum value)
        {
            if (value == null)
                return null;
            var field = value.GetType().GetField(Enum.GetName(value.GetType(), value));
            return (EnumImageAttribute)GetCustomAttribute(field, typeof(EnumImageAttribute));
        }
    }
}