namespace PairwiseRegressionAnalysis
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelRegrassionEquation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCorrelationTest = new System.Windows.Forms.Label();
            this.labelCorrelationCoefficient = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.comboBoxY = new System.Windows.Forms.ComboBox();
            this.comboBoxX = new System.Windows.Forms.ComboBox();
            this.chart_regression = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.labelRegressionError = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_regression)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelRegressionError);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelRegrassionEquation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelCorrelationTest);
            this.groupBox1.Controls.Add(this.labelCorrelationCoefficient);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonPlot);
            this.groupBox1.Controls.Add(this.comboBoxY);
            this.groupBox1.Controls.Add(this.comboBoxX);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelRegrassionEquation
            // 
            this.labelRegrassionEquation.AutoSize = true;
            this.labelRegrassionEquation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegrassionEquation.Location = new System.Drawing.Point(325, 187);
            this.labelRegrassionEquation.Name = "labelRegrassionEquation";
            this.labelRegrassionEquation.Size = new System.Drawing.Size(25, 24);
            this.labelRegrassionEquation.TabIndex = 7;
            this.labelRegrassionEquation.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Уравнение линейной регрессии:";
            // 
            // labelCorrelationTest
            // 
            this.labelCorrelationTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCorrelationTest.Location = new System.Drawing.Point(13, 158);
            this.labelCorrelationTest.Name = "labelCorrelationTest";
            this.labelCorrelationTest.Size = new System.Drawing.Size(466, 29);
            this.labelCorrelationTest.TabIndex = 5;
            this.labelCorrelationTest.Text = "Коэффициент корреляции статистически - ...";
            // 
            // labelCorrelationCoefficient
            // 
            this.labelCorrelationCoefficient.AutoSize = true;
            this.labelCorrelationCoefficient.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCorrelationCoefficient.Location = new System.Drawing.Point(52, 130);
            this.labelCorrelationCoefficient.Name = "labelCorrelationCoefficient";
            this.labelCorrelationCoefficient.Size = new System.Drawing.Size(25, 24);
            this.labelCorrelationCoefficient.TabIndex = 4;
            this.labelCorrelationCoefficient.Text = "...";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 25);
            this.label1.TabIndex = 3;
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(13, 92);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(86, 23);
            this.buttonPlot.TabIndex = 2;
            this.buttonPlot.Text = "Построение";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // comboBoxY
            // 
            this.comboBoxY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxY.FormattingEnabled = true;
            this.comboBoxY.Location = new System.Drawing.Point(7, 47);
            this.comboBoxY.Name = "comboBoxY";
            this.comboBoxY.Size = new System.Drawing.Size(356, 21);
            this.comboBoxY.TabIndex = 1;
            this.comboBoxY.SelectedIndexChanged += new System.EventHandler(this.comboBoxY_SelectedIndexChanged);
            // 
            // comboBoxX
            // 
            this.comboBoxX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxX.FormattingEnabled = true;
            this.comboBoxX.Location = new System.Drawing.Point(7, 20);
            this.comboBoxX.Name = "comboBoxX";
            this.comboBoxX.Size = new System.Drawing.Size(356, 21);
            this.comboBoxX.TabIndex = 0;
            this.comboBoxX.SelectedIndexChanged += new System.EventHandler(this.comboBoxX_SelectedIndexChanged);
            // 
            // chart_regression
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_regression.ChartAreas.Add(chartArea1);
            this.chart_regression.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart_regression.Legends.Add(legend1);
            this.chart_regression.Location = new System.Drawing.Point(518, 0);
            this.chart_regression.Name = "chart_regression";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "SeriesCorrelationField";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "SeriesRegressionEquation";
            this.chart_regression.Series.Add(series1);
            this.chart_regression.Series.Add(series2);
            this.chart_regression.Size = new System.Drawing.Size(645, 450);
            this.chart_regression.TabIndex = 1;
            this.chart_regression.Text = "chart_regression";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Стандартная ошибка регрессии: ";
            // 
            // labelRegressionError
            // 
            this.labelRegressionError.AutoSize = true;
            this.labelRegressionError.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegressionError.Location = new System.Drawing.Point(325, 228);
            this.labelRegressionError.Name = "labelRegressionError";
            this.labelRegressionError.Size = new System.Drawing.Size(25, 24);
            this.labelRegressionError.TabIndex = 9;
            this.labelRegressionError.Text = "...";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 450);
            this.Controls.Add(this.chart_regression);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_regression)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_regression;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.ComboBox comboBoxY;
        private System.Windows.Forms.ComboBox comboBoxX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCorrelationCoefficient;
        private System.Windows.Forms.Label labelCorrelationTest;
        private System.Windows.Forms.Label labelRegrassionEquation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelRegressionError;
        private System.Windows.Forms.Label label3;
    }
}

