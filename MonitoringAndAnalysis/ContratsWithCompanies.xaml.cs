using LiveCharts.Configurations;
using LiveCharts;
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
using Microsoft.EntityFrameworkCore;
using LiveCharts.Helpers;

namespace MonitoringAndAnalysis
{
    /// <summary>
    /// Логика взаимодействия для ContratsWithCompanies.xaml
    /// </summary>
    public partial class ContratsWithCompanies : UserControl
    {
        
        public ContratsWithCompanies()
        {
            InitializeComponent();

            using(AppDbContext context = new AppDbContext())
            {
                Results = new ChartValues<ContractsForCompany>();
                CompanyNames = new List<string>();
                context.Contracts.Where(c => c.IsActive == true).GroupBy(x => x.Company.OfficialName)
                    .Select(x => x.First()).ToList().ForEach(comp=> CompanyNames.Add(context.Companies.First(x=>x.Id == comp.Companyid).OfficialName));
                foreach(string companyName in CompanyNames)
                {
                    ContractsForCompany contrForCompany = new ContractsForCompany();
                    contrForCompany.ContractsCount= context.Contracts.Where(c => c.Company.OfficialName == companyName).ToList().Count;
                    contrForCompany.Companyid = context.Companies.First(x=>x.OfficialName == companyName).Id;
                    context.ContractsForCompanies.Add(contrForCompany);
                    context.SaveChanges();
                }
                Results = context.ContractsForCompanies.OrderByDescending(x => x.ContractsCount).AsChartValues();
            }
            
            Mapper = Mappers.Xy<ContractsForCompany>()
                .X((city, index) => index)
                .Y(contract => contract.ContractsCount);

            Labels = new ObservableCollection<string>(CompanyNames);


            DataContext = this;
        }
        public ChartValues<ContractsForCompany> Results { get; set; }
        public List<string> CompanyNames { get; set; }
        public ObservableCollection<string> Labels { get; set; }

        public object Mapper { get; set; }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var q = (Query.Text ?? string.Empty).ToLower();
            using(AppDbContext context = new AppDbContext()) { 
            var records = context.ContractsForCompanies
                .Where(x => x.Company.OfficialName.ToLower().Contains(q))
                .OrderByDescending(x => x.ContractsCount)
                .ToArray();
            Results.Clear();
            Results.AddRange(records);
            

            Labels.Clear();
                records.ToList().ForEach(comp => Labels.Add(context.Companies.First(x => x.Id == comp.Companyid).OfficialName));
            }
        }
    }
}
