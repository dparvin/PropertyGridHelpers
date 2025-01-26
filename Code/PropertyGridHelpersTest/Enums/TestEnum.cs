using PropertyGridHelpers.Attributes;

namespace PropertyGridHelpersTest.Enums
{
    /// <summary>
    /// Enum to test the different types of image processing
    /// </summary>
    [ResourcePath("Images")]
    public enum TestEnum
    {
        /// <summary>
        /// The item with image
        /// </summary>
        [EnumImage("confetti-stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        ItemWithImage,
        /// <summary>
        /// The item with bitmap image
        /// </summary>
        [EnumImage("confetti-stars-bitmap", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        ItemWithBitmapImage,
        /// <summary>
        /// The confetti
        /// </summary>
        [EnumImage(PropertyGridHelpers.Enums.ImageLocation.Resource)]
        confetti,
        /// <summary>
        /// The item without image
        /// </summary>
        ItemWithoutImage,
        /// <summary>
        /// The item with embedded image
        /// </summary>
        [EnumImage("confetti-stars.jpg")]
        ItemWithEmbeddedImage,
        /// <summary>
        /// The item with embedded image
        /// </summary>
        [EnumImage("Resources.confetti-stars.jpg")]
        ItemWithEmbeddedImageResource,
        /// <summary>
        /// The item with file image
        /// </summary>
        [EnumImage("confetti-stars.jpg", PropertyGridHelpers.Enums.ImageLocation.File)]
        ItemWithFileImage,
        /// <summary>
        /// The item with invalid resource type
        /// </summary>
        [EnumImage("Stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        ItemWithInvalidResourceType,
    }
}