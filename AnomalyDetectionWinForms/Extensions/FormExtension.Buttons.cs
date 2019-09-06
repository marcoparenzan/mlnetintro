using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public static partial class ControlExtension
    {
        public static Button Button(this Form that, EventHandler handler)
        {
            var control = that.New<Button>();
            control.Click += handler;
            return control;
        }
    }
}
