using PropertyGridHelpers.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// UITypeEditor for flag Enums
    /// </summary>
    /// <seealso cref="UITypeEditor" />
    public class FlagEnumUIEditor : UITypeEditor, IDisposable
    {
        /// <summary>
        /// The flag enum CheckBox
        /// </summary>
        protected FlagCheckedListBox FlagEnumCB { get; set; }

        /// <summary>
        /// The object is disposed
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor"/> class.
        /// </summary>
        public FlagEnumUIEditor()
        {
            FlagEnumCB = new FlagCheckedListBox
            {
                BorderStyle = BorderStyle.None
            };
        }

        /// <summary>
        /// Edits the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            if (context != null &&
                context.Instance != null &&
                provider != null)
            {

                var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (edSvc != null)
                {
                    var e = (Enum)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType, CultureInfo.CurrentCulture);
                    FlagEnumCB.EnumValue = e;
                    edSvc.DropDownControl(FlagEnumCB);
                    return FlagEnumCB.EnumValue;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    FlagEnumCB.Dispose();
                }

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FlagEnumUIEditor()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// UITypeEditor for flag Enums
    /// </summary>
    /// <typeparam name="T">EnumConverter to use to make the text in the drop-down list</typeparam>
    /// <seealso cref="UITypeEditor" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="UITypeEditor" />
    public class FlagEnumUIEditor<T> : FlagEnumUIEditor where T : EnumConverter, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagEnumUIEditor" /> class.
        /// </summary>
        public FlagEnumUIEditor() : base()
        {
            FlagEnumCB.Converter = new T();
        }
    }
}
