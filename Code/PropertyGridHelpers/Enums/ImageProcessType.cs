namespace PropertyGridHelpers.Enums
{
    /// <summary>
    /// Select the location to look for the image to display
    /// </summary>
    public enum ImageLocation
    {
        /// <summary>
        /// The image is an embedded resource
        /// </summary>
        Embedded,
        /// <summary>
        /// The image is a resource in the project properties
        /// </summary>
        Resource,
        /// <summary>
        /// The image is a file in a relative path to the project
        /// </summary>
        File,
    }
}
