using LiveCharts.Defaults;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
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

namespace MonitoringAndAnalysis
{
    /// <summary>
    /// Логика взаимодействия для AgeOfPotentialEmployers.xaml
    /// </summary>
    public partial class AgeOfPotentialEmployers : UserControl
    {
        public AgeOfPotentialEmployers()
        {
            InitializeComponent();
            using(AppDbContext dbContext = new AppDbContext()) { 
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "18-25",
                    Values = new ChartValues<int>{ dbContext.Clients.Where(c=> DateTime.Now.Year - c.BirthdayDate.Year >17 
                    && DateTime.Now.Year - c.BirthdayDate.Year < 26).ToArray().Length },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "26-35",
                    Values = new ChartValues<int> { dbContext.Clients.Where(c=> DateTime.Now.Year -c.BirthdayDate.Year >25 
                    && DateTime.Now.Year -c.BirthdayDate.Year < 36).ToArray().Length },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "35-50",
                    Values = new ChartValues<int> {  dbContext.Clients.Where(c=> DateTime.Now.Year - c.BirthdayDate.Year >34 
                    && DateTime.Now.Year - c.BirthdayDate.Year < 51).ToArray().Length},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "51+",
                    Values = new ChartValues<int> { dbContext.Clients.Where(c=>DateTime.Now.Year - c.BirthdayDate.Year >50).ToArray().Length },
                    DataLabels = true
                }
            };
            }

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
   

