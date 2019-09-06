using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public static class MouseEventArgsExtension
    {
        public static bool IsLeftButtonDown(this MouseEventArgs that) => (that.Button & MouseButtons.Left) == MouseButtons.Left;
        public static bool IsLeftButtonUp(this MouseEventArgs that) => (that.Button & MouseButtons.Left) != MouseButtons.Left;
        public static bool IsRightButtonDown(this MouseEventArgs that) => (that.Button & MouseButtons.Right) == MouseButtons.Right;
        public static bool IsRightButtonUp(this MouseEventArgs that) => (that.Button & MouseButtons.Right) != MouseButtons.Right;
    }
}
