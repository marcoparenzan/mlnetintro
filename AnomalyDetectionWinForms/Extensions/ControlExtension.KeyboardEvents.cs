using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public static partial class ControlExtension
    {
        public static TControl OnKeyPress<TControl>(this TControl that, KeyPressEventHandler handler)
            where TControl: Control
        {
            that.KeyPress += handler;
            return that;
        }

        public static TControl OnKeyDown<TControl>(this TControl that, KeyEventHandler handler)
         where TControl : Control
        {
            that.KeyDown += handler;
            return that;
        }
    }
}
