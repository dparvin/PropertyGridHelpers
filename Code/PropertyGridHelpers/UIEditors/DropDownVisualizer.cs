using PropertyGridHelpers.Support;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// A reusable drop-down UITypeEditor that hosts a custom Windows Forms 
    /// control to edit a property value.
    /// </summary>
    /// <typeparam name="TControl">
    /// The type of the Windows Forms control to be shown in the dropdown. 
    /// Must implement <see cref="IDropDownEditorControl"/>.
    /// </typeparam>
    /// <remarks>
    /// This class simplifies building custom UI editors for use in <see cref="PropertyGrid"/> 
    /// by wrapping a control that supports editing a specific data type. The 
    /// dropdown closes automatically when the control raises the 
    /// <see cref="IDropDownEditorControl.ValueCommitted"/> event.
    ///
    /// To use it, decorate your property with:
    /// <code>
    /// [Editor(typeof(DropDownVisualizer&lt;MyControl&gt;), typeof(UITypeEditor))]
    /// public MyValueType MyProperty { get; set; }
    /// </code>
    ///
    /// The provided control type must:
    /// - Be a subclass of <see cref="Control"/>
    /// - Implement <see cref="IDropDownEditorControl"/>
    /// - Have a parameterless constructor
    /// </remarks>
    /// <example>
    /// A simple calculator bound to a decimal property:
    /// <code>
    /// [Editor(typeof(DropDownVisualizer&lt;CalculatorControl&gt;), typeof(UITypeEditor))]
    /// public decimal Total { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="UITypeEditor" />
    /// <seealso cref="IDisposable" />
    public class DropDownVisualizer<TControl> : UITypeEditor, IDisposable
        where TControl : Control, IDropDownEditorControl, new()
    {
        private bool disposedValue;

        /// <summary>
        /// The instance of the drop-down control currently used by the 
        /// editor. This can be configured before editing starts (e.g., 
        /// setting styles, converters, or event handlers).
        /// </summary>
        /// <value>The drop-down control instance.</value>
        public TControl DropDownControl { get; private set; } = new TControl();

        /// <summary>
        /// Gets the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) =>
            UITypeEditorEditStyle.DropDown;

        /// <summary>
        /// Displays the editor control in a dropdown and returns the updated 
        /// value after editing completes.
        /// </summary>
        /// <param name="context">
        /// Provides context information about the design-time environment.
        /// </param>
        /// <param name="provider">
        /// A service provider that can provide an <see cref="IWindowsFormsEditorService"/>.
        /// </param>
        /// <param name="value">
        /// The current value of the property being edited.
        /// </param>
        /// <returns>
        /// The edited value as returned by the control, or the original value 
        /// if editing is canceled or fails.
        /// </returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null &&
                context.Instance != null &&
                provider != null &&
                context.PropertyDescriptor != null)
            {
                if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService edSvc)
                {
                    DropDownControl.Value = value;

                    void OnValueCommitted(object s, EventArgs e) => edSvc.CloseDropDown();

                    DropDownControl.ValueCommitted += OnValueCommitted;

                    edSvc.DropDownControl(DropDownControl);

                    DropDownControl.ValueCommitted -= OnValueCommitted;

                    return DropDownControl.Value;
                }
            }

            Debug.WriteLine("DropDownVisualizer failed due to missing context or services.");
            return null;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        /// <remarks>
        /// This editor implements <see cref="IDisposable"/> and ensures the 
        /// hosted control is disposed after use.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DropDownControl.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

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
}
