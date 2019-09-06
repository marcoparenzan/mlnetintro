using AnomalyDetectionWinForms.Controls;
using AnomalyDetectionWinForms.MLSessions;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnomalyDetectionWinForms.Views
{
    public partial class DetectAnomaliesForm : Form
    {
        public DetectAnomaliesForm()
        {
            InitializeComponent();
        }

        internal static DetectAnomaliesForm New(IMLSession session)
        {
            var form = new DetectAnomaliesForm();
            form.Init(session);
            form.Text = session.Title;
            return form;
        }

        private void Init(IMLSession session)
        {
            var parameters = new MLSessionControl
            {
                Location = new Point(10, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Parameters = session.Parameters
            };
            panel1.Controls.Add(parameters);

            void Add(string name, DateTime t, double value, bool diamond)
            {
                var dp = new DataPoint();
                dp.XValue = t.ToFileTimeUtc();
                dp.YValues = new double[] { value };
                if (diamond)
                {
                    dp.MarkerStyle = MarkerStyle.Diamond;
                }
                chart1.Series[name].Points.Add(dp);
            };

            var transformDoesntChange = false;
            void Transform()
            {
                try
                {
                    if (transformDoesntChange) return;

                    var predictions = session.Predict();

                    chart1.Series["C"].Points.Clear();
                    chart1.Series["CE"].Points.Clear();
                    chart1.Series["F"].Points.Clear();
                    chart1.Series["FE"].Points.Clear();
                    foreach (var p in predictions
                        .Skip(session.Parameter("Skip").Value * predictions.Count() / 100)
                        .Take(session.Parameter("Take").Value * predictions.Count() / 100)
                    )
                    {
                        if (CCheck.Checked) Add("C", p.T, p.C, p.CScore[0] == 1);
                        if (CECheck.Checked) Add("CE", p.T, p.CE, p.CEScore[0] == 1);
                        if (FCheck.Checked) Add("F", p.T, p.F, p.FScore[0] == 1);
                        if (FECheck.Checked) Add("FE", p.T, p.FE, p.FEScore[0] == 1);
                    }
                }
                catch (Exception ex)
                {
                    transformDoesntChange = true;
                    parameters.Rollback();
                    transformDoesntChange = false;
                    MessageBox.Show(ex.Message);
                }
            };

            parameters.ParameterChanged += (s, e) => Transform();

            CCheck.CheckStateChanged += (s, e) => Transform();
            CECheck.CheckStateChanged += (s, e) => Transform();
            FCheck.CheckStateChanged += (s, e) => Transform();
            FECheck.CheckStateChanged += (s, e) => Transform();

            Transform();
        }
    }
}
