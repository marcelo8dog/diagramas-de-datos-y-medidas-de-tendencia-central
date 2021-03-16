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

namespace diagramas_de_datos_y_medidas_de_tendencia_central
{
    public partial class Form1 : Form
    {

        private Random generador = new Random();
        private int alturaMin = 160;
        private int alturaMax = 200;
        private int numeroDeDatos = 100;
        private double []numeros;
        private double arriba1, arriba2, arriba3, arriba4;
        private double abajo1, abajo2, abajo3, abajo4;

        public Form1()
        {
            InitializeComponent();
            inicializarGrafica();
            generarDatosAleatorios();
            
        }

        private void calcularMedidasTendenciaCentral()
        {
            double promedioOut = promedio(numeros);
            double varOut = var(numeros);
            double stdOut = std(numeros);

            textBox1.Text = Convert.ToString(promedioOut);
            textBox2.Text = Convert.ToString(varOut);
            textBox3.Text = Convert.ToString(stdOut);

            arriba1 = promedioOut + 1 * stdOut;
            graficarFuncionConstante(1,arriba1);
            arriba2 = promedioOut + 2 * stdOut;
            graficarFuncionConstante(2, arriba2);
            arriba3 = promedioOut + 3 * stdOut;
            graficarFuncionConstante(3, arriba3);
            arriba4 = promedioOut + 4 * stdOut;
            graficarFuncionConstante(4, arriba4);

            abajo1 = promedioOut - 1 * stdOut;
            graficarFuncionConstante(5, abajo1);
            abajo2 = promedioOut - 2 * stdOut;
            graficarFuncionConstante(6, abajo2);
            abajo3 = promedioOut - 3 * stdOut;
            graficarFuncionConstante(7, abajo3);
            abajo4 = promedioOut - 4 * stdOut;
            graficarFuncionConstante(8, abajo4);

            graficarFuncionConstante(9, promedioOut);
        }

        private void graficarFuncionConstante(int serie, double valorConstante)
        {
            chart1.Series[serie].ChartType = SeriesChartType.Line;
            chart1.Series[serie].IsVisibleInLegend = false;
            limpiarSerie(serie);
            for (int i = 0; i < numeroDeDatos -1 ; i++)
            {
                chart1.Series[serie].Points.AddY(valorConstante);
            }
        }

        private void generarDatosAleatorios()
        {
            

            for (int i = 1; i <= numeroDeDatos; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                fila.CreateCells(dataGridView1);
                fila.Cells[0].Value = i;
                fila.Cells[1].Value = generador.Next(alturaMin, alturaMax);

                dataGridView1.Rows.Add(fila);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //chart1.Series.Clear();
            //chart1.Series[0].Points.Clear();
            limpiarSerie(0);
            //chart1.Series.Add("1");
            numeros = new double[dataGridView1.Rows.Count];
            //numeros = null;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                chart1.Series[0].Points.AddY(dataGridView1.Rows[i].Cells[1].Value);
                numeros[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
            }
            calcularMedidasTendenciaCentral();

        }

        private void limpiarSerie(int serie)
        {
            chart1.Series[serie].Points.Clear();
        }

        private void inicializarGrafica()
        {
            
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            //chart1.Series.Add("V");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].IsVisibleInLegend = false;
            //chart1.ChartAreas[0].AxisY.Minimum = alturaMin-20;
            //chart1.ChartAreas[0].AxisY.Maximum = 5;
        }

        static double promedio(double[] input)
        {
            double output = 0;

            for (int i = 0; i < input.Length; i++)
            {
                output += input[i];
            }

            output = output / input.Length;

            return output;
        }

        static double var(double[] input)
        {
            double media = promedio(input);
            double v = 0;
            int N = input.Length;
            for (int i = 0; i < N; i++)
            {
                v += Math.Pow(input[i] - media, 2);
            }
            v /= (N - 1);
            return v;
        }

        static double std(double[] input)
        {
            double v = var(input);
            double s = Math.Sqrt(v);

            return s;
        }

      
    }
}
