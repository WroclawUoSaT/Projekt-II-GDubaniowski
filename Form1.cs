using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PA_Projekt_II
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double funckja(double x)
        {

           // return (Math.Pow(x, 2) + 2 * Math.Sin(2 * x));
            return (Math.Pow(2, x) * Math.Sin(Math.Pow(2, x)));
        }



        private void wykres_Click(object sender, EventArgs e)
        {
            wykres.ChartAreas[0].AxisY.ScaleView.Zoom(-15, 15);
            wykres.ChartAreas[0].AxisX.ScaleView.Zoom(-15, 2);
            wykres.ChartAreas[0].CursorX.IsUserEnabled = true;
            wykres.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            wykres.ChartAreas[0].AxisX.ScaleView.Zoomable = true;


            for (int i = -15; i < 2; i++)
            {
                wykres.Series[0].Points.AddXY(i, funckja(i));
                wykres.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }


        }





    }
}
