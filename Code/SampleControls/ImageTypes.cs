using PropertyGridHelpers.Attributes;

namespace SampleControls
{
    /// <summary>
    ///Enum for selecting what Image to show in the control
    /// </summary>
    public enum ImageTypes
    {
        /// <summary>
        /// The no image displayed
        /// </summary>
        [EnumText("None")]
        None,
        /// <summary>
        /// A Happy Image
        /// </summary>
        [EnumText("A Happy Image")]
        [EnumImage("Happy")]
        Happy,
        /// <summary>
        /// A Neutral Image
        /// </summary>
        [EnumText("A Neutral Image")]
        [EnumImage("Neutral")]
        Neutral,
        /// <summary>
        /// A Sad Image
        /// </summary>
        [EnumText("A Sad Image")]
        [EnumImage("Sad")]
        Sad,
    }
}
