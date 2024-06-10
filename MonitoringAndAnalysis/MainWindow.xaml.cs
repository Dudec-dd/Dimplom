using System.Windows;

namespace MonitoringAndAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = new LogInPage();
            List<string> slavicNames = new List<string>
            {
            "ABC Company", "XYZ Corporation", "Smith & Sons", "Johnson Enterprises", "Brown Industries", "White Co.", "Green Group", "Blue Ventures", "Red Solutions", "Tech Innovations"

        };
            Random r = new Random();
            using (AppDbContext dbContext = new AppDbContext())
            {
                for (int i = 0; i < 40; i++)
                {
                    Contract contract = new Contract();
                    contract.User = dbContext.Users.ToList()[r.Next(0, dbContext.Users.ToList().Count - 1)];
                    contract.EmployersAmount = r.Next(1, 100);
                    Company c = new Company();
                    c.OfficialName = slavicNames[r.Next(0, slavicNames.Count - 1)];
                    c.INN = r.Next() * 2;
                    c.KPP = r.Next() * 5;
                    c.ORGN = r.Next() * 7;
                    contract.Company = c;
                    contract.Description = "";
                    contract.IsActive = r.Next() % 2 == 0 ? false : true;
                    dbContext.Contracts.Add(contract);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}