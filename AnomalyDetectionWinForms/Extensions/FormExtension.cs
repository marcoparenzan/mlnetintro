using System;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public static partial class FormExtension
    {
        public static TControl New<TControl>(this Form that, string name = null)
            where TControl: Control
        {
            var control = Activator.CreateInstance<TControl>();
            control.WithSuggestedName(name);
            control.AnchorLT();
            that.Controls.Add(control);
            return control;
        }
    }
}
