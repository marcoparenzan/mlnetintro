using AnomalyDetectionWinForms.MLSessions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms.Controls
{
    public partial class MLSessionControl : UserControl
    {
        public MLSessionControl()
        {
            InitializeComponent();
        }

        private IEnumerable<MLSessionParameter> parameters;
        public IEnumerable<MLSessionParameter> Parameters
        {
            get => parameters;
            set 
            {
                parameters = value;

                var y = 0;
                this.Controls.Clear();
                foreach (var parameter in parameters)
                {
                    var control = new MLSessionParameterControl
                    {
                        Parameter = parameter,
                        Location = new Point(0, y)
                    };
                    control.ParameterChanged += Control_ParameterChanged;
                    this.Controls.Add(control);
                    y += control.Height;
                }
                this.Size = new Size(500, y);

            }
        }

        private void Control_ParameterChanged(object sender, MLSessionParameterControl.ParameterChangedEventArgs e)
        {
            parameterChanged?.Invoke(this, new ParameterChangedEventArgs
            {
                Parameter = e.Parameter
            });
        }

        public class ParameterChangedEventArgs : EventArgs
        {
            public MLSessionParameter Parameter { get; set; }
        }

        private EventHandler<ParameterChangedEventArgs> parameterChanged;


        public event EventHandler<ParameterChangedEventArgs> ParameterChanged
        {
            add => parameterChanged += value;
            remove => parameterChanged -= value;
        }

        public void Rollback()
        {
            foreach (var control in Controls)
            {
                if (control is MLSessionParameterControl pc)
                {
                    pc.Rollback();
                }
            }
        }
    }
}
