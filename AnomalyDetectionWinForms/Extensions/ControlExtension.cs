using System.Drawing;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public static partial class ControlExtension
    {
        public static TControl Text<TControl>(this TControl that, string text)
            where TControl : Control
        {
            that.Text = text;
            return that;
        }

        public static TControl WithSuggestedName<TControl>(this TControl that, string name)
            where TControl: Control
        {
            if (name.IsNullOrWhiteSpace())
                that.Name = $"{nameof(TControl)}{that.Controls.Count:00}";
            else
                that.Name = name;
            return that;
        }

        public static TControl Size<TControl>(this TControl that, int wh)
            where TControl : Control
        {
            return that.Size(wh, wh);
        }

        public static TControl Size<TControl>(this TControl that, int width, int height)
            where TControl : Control
        {
            that.Size = new Size(
                width > 0 ? width : that.Parent.ClientSize.Width-that.Location.X+width,
                height > 0 ? height : that.Parent.ClientSize.Height - that.Location.Y + height
            );
            return that;
        }

        public static TControl Location<TControl>(this TControl that, int x, int y)
          where TControl : Control
        {
            that.Location = new Point(
                x > 0 ? x : that.Parent.ClientSize.Width + x,
                y > 0 ? y : that.Parent.ClientSize.Height + y
            );
            return that;
        }

        public static TControl Location<TControl>(this TControl that, int xy)
          where TControl : Control
        {
            return that.Location(xy, xy);
        }

        public static TControl Anchor<TControl>(this TControl that, AnchorStyles styles)
          where TControl : Control
        {
            that.Anchor = styles;
            return that;
        }

        public static TControl AnchorLT<TControl>(this TControl that)
        where TControl : Control
        {
            return that.Anchor(AnchorStyles.Left | AnchorStyles.Top);
        }

        public static TControl AnchorRB<TControl>(this TControl that)
        where TControl : Control
        {
            return that.Anchor(AnchorStyles.Right | AnchorStyles.Bottom);
        }

        public static TControl AnchorAll<TControl>(this TControl that)
        where TControl : Control
        {
            return that.Anchor(AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
        }

        public static TControl Background<TControl>(this TControl that, Color? color = null)
          where TControl : Control
        {
            that.BackColor = color ?? Color.White;
            return that;
        }
    }
}
