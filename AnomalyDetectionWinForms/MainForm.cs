using AnomalyDetectionWinForms.MLSessions;
using AnomalyDetectionWinForms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            OpenRecentList();

            this.newRS485ToolStripMenuItem.Click += (s,e) => NewRS485();
            this.newWaveEditorToolStripMenuItem.Click += (s, e) => NewWaveEditor();
            this.newMonitorEventsToolStripMenuItem.Click += (s, e) => NewMonitorEvents();
            this.openToolStripMenuItem.Click += (s, e) => OpenWithDialog();
            this.openToolStripButton.Click += (s, e) => OpenWithDialog();
            this.openRecentToolStripMenuItem.DropDownItemClicked += (s, e) =>
            {
                OpenFile(e.ClickedItem.Text);
            };

            this.exitToolStripMenuItem.Click += (s, e) => Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        // 

        private void NewMonitorEvents()
        {
            var form = MonitorEventsForm.New("");
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void NewWaveEditor()
        {
            var form = WaveEditorForm.New("");
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void NewRS485()
        {
            var form = RS485Form.New("");
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        readonly string RecentFileName = "recent.txt";

        private void OpenFile(string filename)
        {
            var session = DetectSpikeBySsaSession.New(filename);
            var form = DetectAnomaliesForm.New(session);
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();

            AppendToRecentList(filename);
        }

        string[] GetRecentList()
        {
            if (!File.Exists(RecentFileName)) return new string[] { };
            string[] results = default;
            using (var reader = File.OpenText(RecentFileName))
            {
                results = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            return results;
        }

        private void OpenRecentList()
        {
            openRecentToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in GetRecentList())
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void OpenWithDialog()
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenFile(openFileDialog1.FileName);
            }
        }

        private void AppendToRecentList(string filename)
        {
            var items = new List<string>();
            items.Add(filename);
            foreach (var item in GetRecentList())
            {
                if (items.Count == 5) break;
                if (items.Contains(item)) continue;
                items.Add(item);
            }
            File.WriteAllText(RecentFileName, string.Join("\r\n", items.ToArray()));
        }

    }
}
