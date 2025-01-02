using PropertyGridHelpers.Enums;
using PropertyGridHelpers.UIEditors;
using System;

namespace PropertyGridHelpers.Attributes
{
    /// <summary>
    /// Apply an image to an Enum item for use in a property page
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <seealso cref="ImageTextUIEditor" />
    /// <remarks>
    /// Apply this attribute to an Enum item to associate an image with the item and then apply 
    /// the ImageTextUIEditor to a property in your class to display the image in a property grid.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class EnumImageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute"/> class.
        /// </summary>
        public EnumImageAttribute()
        {
            EnumImage = null;
            ImageLocation = ImageLocation.Embedded;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute" /> class.
        /// </summary>
        /// <param name="imageLocation">The image location.</param>
        public EnumImageAttribute(ImageLocation imageLocation)
        {
            EnumImage = null;
            ImageLocation = imageLocation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumImageAttribute" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="imageLocation">The image location.</param>
        public EnumImageAttribute(string text, ImageLocation imageLocation = ImageLocation.Embedded)
        {
            EnumImage = text;
            ImageLocation = imageLocation;
        }

        /// <summary>
        /// Gets the name of the image associated with the Enum item.
        /// </summary>
        /// <value>
        /// The Enum text.
        /// </value>
        public string EnumImage
        {
            get;
        }
        /// <summary>
        /// Gets the image location.
        /// </summary>
        /// <value>
        /// The image location.
        /// </value>
        public ImageLocation ImageLocation
        {
            get;
        }
    }
}