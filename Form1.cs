using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace WindowsFormsApplication13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool seria ;










        private void button1_Click(object sender, EventArgs e)
        {
            wykres.Series.Clear();


            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\temp";
            openFileDialog1.Title = "Browse txt File";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Pliki csv (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string cos = File.ReadAllText(openFileDialog1.FileName);
                string[] rows = cos.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                //DataTable table = new DataTable();
                //table.Columns.Add("xValue", typeof(decimal));
                //table.Columns.Add("yValue", typeof(decimal));


                DataTable table = new DataTable();
                try
                {
                    using (TextFieldParser csvReader = new TextFieldParser(openFileDialog1.FileName))
                    {
                        csvReader.SetDelimiters(new string[] { ";" });
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            table.Columns.Add(datecolumn);

                        }
                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            //Making empty value as null

                            table.Rows.Add(fieldData);
                        }
                    }
                }
                catch (Exception ex)
                {
                }


                dataGridView1.DataSource = table;
                foreach (DataGridViewColumn dcol in dataGridView1.Columns)
                {
                    dcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }









              




                    wykres.Series.Add("series");
                    wykres.Series["series"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    wykres.Series["series"].XValueMember = "Seria1 - X";
                    wykres.Series["series"].YValueMembers = "Seria1 - Y";
                    wykres.DataSource = table;
                    wykres.DataBind();

                    DataPoint min = wykres.Series["series"].Points.FindMinByValue("Y");
                    DataPoint max = wykres.Series["series"].Points.FindMaxByValue("Y");
                    double x1 = min.XValue;
                    double[] y1 = min.YValues;
                    double x = max.XValue;
                    double[] y = max.YValues;
                    string s = "(" + x.ToString() + " , " + y[0].ToString() + ")";
                    string t = "(" + x1.ToString() + " , " + y1[0].ToString() + ")";
                    wykres.Series["series"].Points.FindMaxByValue("Y").Label = s;
                    wykres.Series["series"].Points.FindMinByValue("Y").Label = t;

                    seria = true;



                
               

            }
        }







        private void button2_Click_1(object sender, EventArgs e)
        {
            Axis ax = wykres.ChartAreas[0].AxisX;
            ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
                                (ax.Maximum - ax.Minimum) / 2 : ax.ScaleView.Size /= 2;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Axis ax = wykres.ChartAreas[0].AxisX;
            ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
                                ax.Maximum : ax.ScaleView.Size *= 2;
            if (ax.ScaleView.Size > ax.Maximum - ax.Minimum)
            {
                ax.ScaleView.Size = ax.Maximum;
                ax.ScaleView.Position = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wykres.Series["series"].Points.Clear();
            dataGridView1.DataSource = null;


        }

        private void wykres_Click(object sender, EventArgs e)
        {

        }


    }
}

