namespace AnomalyDetectionWinForms.Views
{
    partial class DetectAnomaliesForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FECheck = new System.Windows.Forms.CheckBox();
            this.FCheck = new System.Windows.Forms.CheckBox();
            this.CECheck = new System.Windows.Forms.CheckBox();
            this.CCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1115, 724);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 11;
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Name = "C";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Name = "CE";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Name = "F";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series16.Name = "FE";
            this.chart1.Series.Add(series13);
            this.chart1.Series.Add(series14);
            this.chart1.Series.Add(series15);
            this.chart1.Series.Add(series16);
            this.chart1.Size = new System.Drawing.Size(1115, 362);
            this.chart1.TabIndex = 10;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FECheck);
            this.panel1.Controls.Add(this.FCheck);
            this.panel1.Controls.Add(this.CECheck);
            this.panel1.Controls.Add(this.CCheck);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1115, 358);
            this.panel1.TabIndex = 11;
            // 
            // FECheck
            // 
            this.FECheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FECheck.AutoSize = true;
            this.FECheck.Location = new System.Drawing.Point(1012, 175);
            this.FECheck.Name = "FECheck";
            this.FECheck.Size = new System.Drawing.Size(39, 17);
            this.FECheck.TabIndex = 25;
            this.FECheck.Text = "FE";
            this.FECheck.UseVisualStyleBackColor = true;
            // 
            // FCheck
            // 
            this.FCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FCheck.AutoSize = true;
            this.FCheck.Checked = true;
            this.FCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FCheck.Location = new System.Drawing.Point(1012, 143);
            this.FCheck.Name = "FCheck";
            this.FCheck.Size = new System.Drawing.Size(32, 17);
            this.FCheck.TabIndex = 24;
            this.FCheck.Text = "F";
            this.FCheck.UseVisualStyleBackColor = true;
            // 
            // CECheck
            // 
            this.CECheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CECheck.AutoSize = true;
            this.CECheck.Location = new System.Drawing.Point(1012, 104);
            this.CECheck.Name = "CECheck";
            this.CECheck.Size = new System.Drawing.Size(40, 17);
            this.CECheck.TabIndex = 23;
            this.CECheck.Text = "CE";
            this.CECheck.UseVisualStyleBackColor = true;
            // 
            // CCheck
            // 
            this.CCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CCheck.AutoSize = true;
            this.CCheck.Checked = true;
            this.CCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CCheck.Location = new System.Drawing.Point(1012, 68);
            this.CCheck.Name = "CCheck";
            this.CCheck.Size = new System.Drawing.Size(33, 17);
            this.CCheck.TabIndex = 22;
            this.CCheck.Text = "C";
            this.CCheck.UseVisualStyleBackColor = true;
            // 
            // DetectSpikeBySsaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 724);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DetectSpikeBySsaForm";
            this.Text = "Detect Spike By Ssa";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox FECheck;
        private System.Windows.Forms.CheckBox FCheck;
        private System.Windows.Forms.CheckBox CECheck;
        private System.Windows.Forms.CheckBox CCheck;
    }
}

