using PropertyGridHelpers.Attributes;

namespace PropertyGridHelpersTest.Enums
{
    /// <summary>
    /// Enum to test full paths to the images in the project
    /// </summary>
    public enum FullPathTestItems
    {
        /// <summary>
        /// The item1
        /// </summary>
        [EnumImage(".Properties.Resources.confetti-stars", PropertyGridHelpers.Enums.ImageLocation.Resource)]
        Item1,
        /// <summary>
        /// The item2
        /// </summary>
        [EnumImage("Resources.confetti-stars.jpg")]
        Item2,
    }
}
