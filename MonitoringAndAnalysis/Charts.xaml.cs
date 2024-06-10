using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Threading;

namespace MonitoringAndAnalysis
{
    /// <summary>
    /// Логика взаимодействия для Charts.xaml
    /// </summary>
    public partial class Charts : UserControl
    {
        public List<string> Labels { get; set; }
        public ChartValues<Contract> Results { get; set; }
        public List<string> TempLabels { get; set; }
        public Object Mapper { get; set; }
        System.Timers.Timer timer { get; set; }
        public Charts()
        {
            InitializeComponent();
            TempLabels = new List<string>();
            using (AppDbContext dbContext = new AppDbContext())
            {
                Random r = new Random();
                Mapper = Mappers.Xy<Contract>()
                .X((contract, id) => id)
                .Y(contract => contract.EmployersAmount);
                Results = dbContext.Contracts.Where(c => c.IsActive == true).AsChartValues();
                Labels = new List<string>();
                List<Company> comp = dbContext.Companies.ToList();
                List<Contract> cc = dbContext.Contracts.Where(co => co.IsActive == true).ToList();
                cc.ForEach(v => Labels.Add(v.Company.OfficialName));
            }
            DataContext = this;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Button_Click;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        private void Button_Click(object? sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(()=>
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    Random r = new Random();
                    Mapper = Mappers.Xy<Contract>()
                    .X((contract, id) => id)
                    .Y(contract => contract.EmployersAmount);
                    Results = dbContext.Contracts.Where(c => c.IsActive == true).AsChartValues();
                    Labels = new List<string>();
                    List<Company> comp = dbContext.Companies.ToList();
                    List<Contract> cc = dbContext.Contracts.Where(co => co.IsActive == true).ToList();
                    cc.ForEach(v => Labels.Add(v.Company.OfficialName));
                }
                if (TempLabels.Count < Labels.Count) 
                { 
                    DataContext = null;
                    DataContext = this;
                }
                TempLabels = Labels;

            });
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }

}