using AnomalyDetectionWinForms.MLSessions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms.Controls
{
    public partial class MLSessionParameterControl : UserControl
    {
        public MLSessionParameterControl()
        {
            InitializeComponent();

            this.textBox.DataBindings.Add("Text", slider, "Value");
            this.slider.ValueChanged += Slider_ValueChanged;
        }

        int? lastValue = default;
        bool noNotify = false;

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            lastValue = parameter.Value;
            parameter.Value = slider.Value;
            if (!noNotify)
            {
                parameterChanged?.Invoke(this, new ParameterChangedEventArgs
                {
                    Parameter = parameter
                });
            }
        }

        public void Rollback()
        {
            if (lastValue == null) return;

            parameter.Value = default;
            var c = lastValue.Value;
            noNotify = true;
            this.slider.Value = c;
            noNotify = false;
        }

        public class ParameterChangedEventArgs: EventArgs
        {
            public MLSessionParameter Parameter { get; set; }
        }

        private EventHandler<ParameterChangedEventArgs> parameterChanged;

        public event EventHandler<ParameterChangedEventArgs> ParameterChanged
        {
            add => parameterChanged += value;
            remove => parameterChanged -= value;
        }

        private MLSessionParameter parameter;

        public MLSessionParameter Parameter
        {
            get => parameter;
            set
            {
                parameter = value;
                label.Text = parameter.Label;
                switch (parameter.Range)
                {
                    case RangeType.Percentage:
                        slider.Minimum = 0;
                        slider.Maximum = 100;
                        break;
                    case RangeType.Zero100:
                        slider.Minimum = 0;
                        slider.Maximum = 100;
                        break;
                    case RangeType.Zero1000:
                        slider.Minimum = 0;
                        slider.Maximum = 1000;
                        break;
                }
            }
        }
    }
}
