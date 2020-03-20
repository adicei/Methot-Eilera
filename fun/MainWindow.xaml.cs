using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jarvis;
using OxyPlot;
using LiveCharts.Wpf;
using EO.Internal;
using LiveCharts;
using LiveCharts.Defaults;
//using LiveCharts.Configurations;
//using System.Windows.Forms.DataVisualization.Charting;
//using LiveCharts.Defaults;

namespace fun
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Helper.addvar();

        }
        //public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        LiveCharts.Wpf.CartesianChart chart;
       // public IList<DataPoint> Points { get; private set; }

        public ObservableCollection<XY> collection { get; set; }
        public ChartValues<ObservablePoint> ValuesY { get; private set; }
        //public ChartValues<Axis> ValuesX { get; private set; }
        // public ChartValues<Axis> Valuesxy { get; private set; }

        private void EX_Click(object sender, RoutedEventArgs e) {
            TA.Text = "0";
            TB.Text = "0,5";
            Num.Text = "6";
            Func.Text = "(y+x)^2";
            toch.Text = "tan(x)-x";
        }
        private void PUPH_Click(object sender, RoutedEventArgs e)
        {
            if (a1.IsChecked == true)
            {
                // string F = Func.Text;
                double a = Convert.ToDouble(TA.Text);
                double b = Convert.ToDouble(TB.Text);
                int n = Convert.ToInt32(Num.Text);
                double[] x = new double[n];
                double[] y = new double[n];
                double[] dy = new double[n];
                double[] yt = new double[n];
                double[] ye = new double[n];

                double h = (b - a) / (n - 1);
                x[0] = a;
                y[0] = a;
                for (int i = 1; i < n; i++)
                {
                    x[i] = x[i - 1] + h;
                    //y[i] = f(x[i]);

                }
                //y[0] = a;

                List<tabl> tabls = new List<tabl>(n);
                //tabls.Add(new tabl(x[0], y[0] - h * f(x[i - 1], y[i - 1]), h * f(x[i - 1], y[i - 1]), ft(x[i]), ft(x[i] - y[0] - h * f(x[i - 1], y[i - 1]))
                for (int i = 0; i < n; i++)
                {
                    if (i == 0)
                    {
                        dy[i] = a;
                        y[i] = a;
                        yt[i] = a;
                        ye[i] = a;

                    }
                    else
                    {
                        dy[i] = h * f(x[i - 1], y[i - 1]);
                        y[i] = y[i - 1] + dy[i];
                        yt[i] = ft(x[i]);
                        ye[i] = yt[i] - y[i];
                    }



                    tabls.Add(new tabl(x[i], y[i], dy[i], yt[i], ye[i]));
                }
                List<XY> FU = new List<XY>(n);
                for (int i = 0; i < n; i++)
                {
                    FU.Add(new XY(x[i], y[i]));
                }
                Tabl.ItemsSource = FU;
                vidp.ItemsSource = tabls;


                //-------------------------------график---------------
                DataContext = null;
                MyValues = new ChartValues<ObservablePoint>();
                MyValuesT = new ChartValues<ObservablePoint>();
                for (int i = 0; i < n; i++)
                {
                    MyValues.Add(new ObservablePoint(x[i], y[i]));
                    MyValuesT.Add(new ObservablePoint(x[i], yt[i]));
                }
                var lineSeries = new LineSeries
                {
                    Values = MyValues,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "шукана",
                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };
                var lineSeries2 = new LineSeries
                {
                    Values = MyValuesT,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "уточнена",

                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };
                SeriesCollection = new SeriesCollection { lineSeries, lineSeries2 };
                DataContext = this;
            }
            else if (a2.IsChecked == true)
            {
                // string F = Func.Text;
                double a = Convert.ToDouble(TA.Text);
                double b = Convert.ToDouble(TB.Text);
                int n = Convert.ToInt32(Num.Text);
                double[] x = new double[n];
                double[] y = new double[n];
                double[] yp = new double[n];

                double[] dy = new double[n];
                double[] yt = new double[n];
                double[] ye = new double[n];

                double h = (b - a) / (n - 1);
                x[0] = a;
                y[0] = a;
                for (int i = 1; i < n; i++)
                {
                    x[i] = x[i - 1] + h;
                    //y[i] = f(x[i]);
                    
                }
                //y[0] = a;

                List<tabl> tabls = new List<tabl>(n);
                //tabls.Add(new tabl(x[0], y[0] - h * f(x[i - 1], y[i - 1]), h * f(x[i - 1], y[i - 1]), ft(x[i]), ft(x[i] - y[0] - h * f(x[i - 1], y[i - 1]))
                for (int i = 0; i < n; i++)
                {
                    if (i == 0)
                    {
                        dy[i] = a;
                        y[i] = a;
                        yt[i] = a;
                        ye[i] = a;
                        yp[i] = a;


                    }
                    else
                    {
                        dy[i] = h * f(x[i - 1], y[i - 1]);
                        y[i] = y[i - 1] + dy[i];
                        yp[i] = y[i - 1] + h * f(x[i], y[i]);
                        y[i] = yp[i];
                        yt[i] = ft(x[i]);
                        ye[i] = yt[i] - y[i];
                    }



                    tabls.Add(new tabl(x[i], y[i], dy[i], yt[i], ye[i]));
                }
                List<XY> FU = new List<XY>(n);
                for (int i = 0; i < n; i++)
                {
                    FU.Add(new XY(x[i], y[i]));
                }
                Tabl.ItemsSource = FU;
                vidp.ItemsSource = tabls;


                //-------------------------------график---------------
                DataContext = null;
                MyValues = new ChartValues<ObservablePoint>();
                MyValuesT = new ChartValues<ObservablePoint>();
                for (int i = 0; i < n; i++)
                {
                    MyValues.Add(new ObservablePoint(x[i], y[i]));
                    MyValuesT.Add(new ObservablePoint(x[i], yt[i]));
                }
                var lineSeries = new LineSeries
                {
                    Values = MyValues,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "шукана",
                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };
                var lineSeries2 = new LineSeries
                {
                    Values = MyValuesT,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "уточнена",

                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };
                SeriesCollection = new SeriesCollection { lineSeries, lineSeries2 };
                DataContext = this;
            }
            else
            {
                double a = Convert.ToDouble(TA.Text);
                double b = Convert.ToDouble(TB.Text);
                int n = Convert.ToInt32(Num.Text);
                double[] x = new double[n];
                double[] y = new double[n];
                double[] yp = new double[n];
                double[] dy = new double[n];
                double[] yt = new double[n];
                double[] ye = new double[n];

                double h = (b - a) / (n - 1);
                x[0] = a;
                y[0] = a;
                for (int i = 1; i < n; i++)
                {
                    x[i] = x[i - 1] + h;
                    //y[i] = f(x[i]);

                }
                //y[0] = a;

                List<tabl> tabls = new List<tabl>(n);
                //tabls.Add(new tabl(x[0], y[0] - h * f(x[i - 1], y[i - 1]), h * f(x[i - 1], y[i - 1]), ft(x[i]), ft(x[i] - y[0] - h * f(x[i - 1], y[i - 1]))
                for (int i = 0; i < n; i++)
                {
                    if (i == 0)
                    {
                        dy[i] = a;
                        y[i] = a;
                        yt[i] = a;
                        ye[i] = a;
                        yt[i] = a;

                    }
                    else
                    {
                        dy[i] = h * f(x[i - 1], y[i - 1]);
                        y[i] = y[i - 1] + dy[i];
                        yp[i] = y[i - 1] + (h / 2) * (f(x[i - 1], y[i - 1]) + f(x[i], y[i]));
                        y[i] = yp[i];
                        yt[i] = ft(x[i]);
                        ye[i] = yt[i] - y[i];
                    }



                    tabls.Add(new tabl(x[i], y[i], dy[i], yt[i], ye[i]));
                }
                List<XY> FU = new List<XY>(n);
                for (int i = 0; i < n; i++)
                {
                    FU.Add(new XY(x[i], y[i]));
                }
                Tabl.ItemsSource = FU;
                vidp.ItemsSource = tabls;
                DataContext = null;
                MyValues = new ChartValues<ObservablePoint>();
                MyValuesT = new ChartValues<ObservablePoint>();
                for (int i = 0; i < n; i++)
                {
                    MyValues.Add(new ObservablePoint(x[i], y[i]));
                    MyValuesT.Add(new ObservablePoint(x[i], yt[i]));
                }
                var lineSeries = new LineSeries
                {
                    Values = MyValues,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "шукана",
                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };
                var lineSeries2 = new LineSeries
                {
                    Values = MyValuesT,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Title = "уточнена",

                    //PointGeometrySize = 10,
                    //PointGeometry = DefaultGeometries.Circle,
                    DataLabels = false,

                };

                SeriesCollection = new SeriesCollection { lineSeries, lineSeries2 };
                DataContext = this;
            }

        }
        public ChartValues<ObservablePoint> MyValues { get; set; }
        public ChartValues<ObservablePoint> MyValuesT { get; private set; }

        public SeriesCollection SeriesCollection { get; set; }
        //public ChartValues<ObservablePoint> kek { get; set; }
        //public SeriesCollection kek { get; set; }
           // CartesianChart_SizeChanged
        public double f(double x, double y)
    {
        string res = Convert.ToString(Math.Round((float)Helper.Function_from_string(x, y, Func.Text), 5));
        return Convert.ToDouble(res);

    }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            MessageBox.Show(pressed.Content.ToString());
        }

        public double ft(double x)
    {
        string res = Convert.ToString(Math.Round((float)Helper.Function_from_string(x, toch.Text), 5));
        return Convert.ToDouble(res);
    }

        private void CartesianChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
    public class XY
    {
        public XY(double X,double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public double X { get; set; }
        public double Y { get; set; }
        
        //public XY(double X, double Y)
        //{
        //    this.X = X;
        //    this.Y = Y;
        //}
    }
    public class tabl
    {
        public tabl(double X, double Y,double DY,double YT,double EY)
        {
            this.X = X;
            this.Y = Y;
            this.DY = DY;
            this.YT = YT;
            this.EY = EY;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double DY { get; set; }
        public double YT { get; set; }
        public double EY { get; set; }

        //public XY(double X, double Y)
        //{
        //    this.X = X;
        //    this.Y = Y;
        //}
    }
    
}
