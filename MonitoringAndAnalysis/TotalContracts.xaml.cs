using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для TotalContracts.xaml
    /// </summary>
    public partial class TotalContracts : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public TotalContracts()
        {
            InitializeComponent();
            Labels = new List<string> { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сенябрь", "Октябрь", "Ноябрь", "Декабрь" };
            SeriesCollection = new SeriesCollection();
            ChartValues<int> values = new ChartValues<int>();
            for (int i = 0; i < 12; i++)
            {
                using(AppDbContext db = new AppDbContext())
                {
                    values.Add(db.Contracts.Where(c => c.ContractDateStart.Month == i + 1 && c.ContractDateStart.Year == DateTime.Now.Year).ToList().Count);
                }

            }
            Random r = new Random();
            LineSeries series = new LineSeries();
            series.Title = "Количество договоров";
            series.Values = values;
            SeriesCollection.Add(series);
            LineSeries predict = new LineSeries();
            predict.Title = "Ожидаемое количество договоров";
            ChartValues<int> predictvalues = new ChartValues<int>();
            for (int i = 1;i < 12;i++)
            {
                predictvalues.Add(values[i]+ (values[i] >5? r.Next(-5, values[i]):r.Next(0,3)));
            }
            predict.Values = predictvalues;
            SeriesCollection.Add(predict);
            DataContext = this;
        }
    }
}
