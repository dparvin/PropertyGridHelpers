using PropertyGridHelpers.UIEditors;
using System;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PropertyGridHelpers.UIEditors.ImageTextUIEditor&lt;T&gt;" />
#if NET35
    public class TestImageTextUIEditor<T> : ImageTextUIEditor<T> where T : struct
    {
        /// <summary>
        /// Initializes the <see cref="TestImageTextUIEditor{T}"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentException">T must be an enumerated type</exception>
        static TestImageTextUIEditor()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        /// <summary>
        /// Gets the type of the test enum.
        /// </summary>
        /// <value>
        /// The type of the test enum.
        /// </value>
        public Type TestEnumType => EnumType;
        /// <summary>
        /// Gets the test resource path.
        /// </summary>
        /// <value>
        /// The test resource path.
        /// </value>
        public string TestResourcePath => ResourcePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestImageTextUIEditor{T}"/> class.
        /// </summary>
        public TestImageTextUIEditor() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestImageTextUIEditor{T}"/> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public TestImageTextUIEditor(string ResourcePath) : base(ResourcePath) { }
    }
#elif NET40_OR_GREATER || NET5_0_OR_GREATER
    public class TestImageTextUIEditor<T> : ImageTextUIEditor<T> where T : struct, Enum
    {
        /// <summary>
        /// Gets the type of the test enum.
        /// </summary>
        /// <value>
        /// The type of the test enum.
        /// </value>
        public Type TestEnumType => EnumType;
        /// <summary>
        /// Gets the test resource path.
        /// </summary>
        /// <value>
        /// The test resource path.
        /// </value>
        public string TestResourcePath => ResourcePath;
        /// <summary>
        /// Initializes a new instance of the <see cref="TestImageTextUIEditor{T}"/> class.
        /// </summary>
        public TestImageTextUIEditor() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TestImageTextUIEditor{T}"/> class.
        /// </summary>
        /// <param name="ResourcePath">The path to the resources where the images are stored</param>
        public TestImageTextUIEditor(string ResourcePath) : base(ResourcePath) { }
    }
#endif
}
