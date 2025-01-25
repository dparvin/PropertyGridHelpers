using PropertyGridHelpers.UIEditors;
using System;
using System.Reflection;
using Xunit;
#if NET35
#else
using Xunit.Abstractions;
#endif

#if NET35
namespace PropertyGridHelpersTest.net35.UIEditor
#elif NET452
namespace PropertyGridHelpersTest.net452.UIEditor
#elif NET462
namespace PropertyGridHelpersTest.net462.UIEditor
#elif NET472
namespace PropertyGridHelpersTest.net472.UIEditor
#elif NET481
namespace PropertyGridHelpersTest.net481.UIEditor
#elif NET8_0
namespace PropertyGridHelpersTest.net80.UIEditor
#elif NET9_0
namespace PropertyGridHelpersTest.net90.UIEditor
#endif
{
    /// <summary>
    /// Collection UI Editor Tests
    /// </summary>
    public class CollectionUIEditorTest
    {
#if NET35
#else
        private readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Collection UI Editor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public CollectionUIEditorTest(ITestOutputHelper output) => OutputHelper = output;
#endif

        /// <summary>
        /// Adds the item to list test.
        /// </summary>
        [Fact]
        public void AddItemToListTest()
        {
            var testItem = new CollectionUIEditor<string>();
            _ = testItem.EditValue(null, "test");
            Output(testItem.ToString());
        }

        /// <summary>
        /// Tests the CreateInstance method.
        /// </summary>
        [Fact]
        public void CreateInstanceTest()
        {
            // Arrange
            var collectionEditor = new CollectionUIEditor<string>();

            // Use reflection to access the protected CreateInstance method
            var method = typeof(CollectionUIEditor<string>).GetMethod(
                "CreateInstance",
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(method); // Ensure the method exists

            // Act
#if NET8_0_OR_GREATER
            var result = method.Invoke(collectionEditor, [typeof(string)]);
#else
            var result = method.Invoke(collectionEditor, new object[] { typeof(string) });
#endif

            // Assert
            Assert.NotNull(result);
            _ = Assert.IsType<string>(result);
            Assert.Equal(string.Empty, result); // Verify it returns an empty string for typeof(string)

            // Test with a different type
#if NET8_0_OR_GREATER
            var intResult = method.Invoke(collectionEditor, [typeof(int)]);
#else
            var intResult = method.Invoke(collectionEditor, new object[] { typeof(int) });
#endif
            Assert.NotNull(intResult);
            _ = Assert.IsType<int>(intResult);
            Assert.Equal(0, intResult); // Default value of int

            Output($"CreateInstance returned: {result} for string, {intResult} for int.");
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message) =>
            Console.WriteLine(message);
#else
        private void Output(string message) =>
            OutputHelper.WriteLine(message);
#endif
    }
}
