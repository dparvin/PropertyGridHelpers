using System;
using System.Collections.Generic;
using PropertyGridHelpers.UIEditors;
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
#elif NET48
namespace PropertyGridHelpersTest.net48.UIEditor
#elif NET5_0
namespace PropertyGridHelpersTest.net50.UIEditor
#endif
{
    /// <summary>
    ///
    /// </summary>
    public class CollectionUIEditorTest
    {
#if NET35
#else
        readonly ITestOutputHelper OutputHelper;
        /// <summary>
        /// Collection UI Editor Test
        /// </summary>
        /// <param name="output">xunit output implementation</param>
        public CollectionUIEditorTest(ITestOutputHelper output)

        {
            OutputHelper = output;
        }
#endif

        /// <summary>
        /// Adds the item to list test.
        /// </summary>
        [Fact]
        public void AddItemToListTest()
        {
            CollectionUIEditor<string> testItem = new CollectionUIEditor<string>();
            testItem.EditValue(null, "test");
        }

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
#if NET35
        private static void Output(string message)
#else
        private void Output(string message)
#endif
        {
#if NET35
            Console.WriteLine(message);
#else
            OutputHelper.WriteLine(message);
#endif
        }
    }
}
