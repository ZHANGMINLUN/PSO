using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PSO
{
    public partial class Form1 : Form
    {
        PSO pso;
        Color[] colors = { Color.Red, Color.Blue, Color.LightGray, Color.Black, Color.Aquamarine };
        int Num = 50;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            pso = new PSO(Num);
            pso.initialize();

            chart1.ChartAreas[0].AxisX.Minimum = pso.Smin;
            chart1.ChartAreas[0].AxisX.Maximum = pso.Smax;
            chart1.ChartAreas[0].AxisY.Minimum = pso.Smin;
            chart1.ChartAreas[0].AxisY.Maximum = pso.Smax;

            for (int i = 0; i < Num; i++)
            {
                Series series = new Series("Particle" + i, 1000);
                series.ChartType = SeriesChartType.Point;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 7;
                chart1.Series.Add(series);
                chart1.Series[i].Points.AddXY(pso.X[i][0], pso.X[i][1]);
            }

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pso.optimize();
            for (int i = 0; i < Num; i++)
            {
                chart1.Series[i].Points.Clear();
                chart1.Series[i].Points.AddXY(pso.X[i][0], pso.X[i][1]);
            }
            label1.Text = "gBest: " + pso.lossBest.ToString("0.00000000");
            label2.Text = "pBest: " + pso.localBest.ToString("0.0000000");
            label3.Text = "Iteration: " + pso.iteration.ToString();
            if (pso.iteration == 150) timer1.Enabled = false;
        }
    }
}
