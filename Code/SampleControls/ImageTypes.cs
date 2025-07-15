using PropertyGridHelpers.Attributes;

namespace SampleControls
{
    /// <summary>
    ///Enum for selecting what Image to show in the control
    /// </summary>
    [ResourcePath("Properties.Resources")]
    public enum ImageTypes
    {
        /// <summary>
        /// The no image displayed
        /// </summary>
        [LocalizedEnumText("None")]
        [EnumText("None")]
        None,
        /// <summary>
        /// A Happy Image
        /// </summary>
        [LocalizedEnumText("ImageTypes_Happy")]
        [EnumText("A Happy Image")]
        [EnumImage("Happy", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        Happy,
        /// <summary>
        /// A Neutral Image
        /// </summary>
        [LocalizedEnumText("ImageTypes_Neutral")]
        [EnumText("A Neutral Image")]
        [EnumImage("Neutral", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        Neutral,
        /// <summary>
        /// A Sad Image
        /// </summary>
        [LocalizedEnumText("ImageTypes_Sad")]
        [EnumText("A Sad Image")]
        [EnumImage("Sad", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        Sad,
    }
}
