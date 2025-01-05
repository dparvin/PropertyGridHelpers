using PropertyGridHelpers.Attributes;

namespace PropertyGridHelpersTest.Enums
{
    /// <summary>
    /// Image File Extensions
    /// </summary>
    public enum ImageFileExtension
    {
        /// <summary>
        /// The None
        /// </summary>
        None,
        /// <summary>
        /// The JPG
        /// </summary>
        [EnumText("jpg - jpeg file")]
        jpg,
        /// <summary>
        /// The PNG
        /// </summary>
        png,
        /// <summary>
        /// The BMP
        /// </summary>
        bmp,
        /// <summary>
        /// The GIF
        /// </summary>
        gif
    }
}
