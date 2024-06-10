using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MonitoringAndAnalysis
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {

            InitializeComponent();
            Chart.Children.Add(new MainText());
            if ((Global.CurrentUser.RoleId == 2 ? Roless.Director : Roless.User) == Roless.Director) SessionsBtn.Visibility = Visibility.Visible;
            string state = ((DateTime.Now.Hour > 5) && (DateTime.Now.Hour < 10)) ? "Доброе утро" : ((DateTime.Now.Hour > 10) && (DateTime.Now.Hour < 18)) ? "Добрый день" : ((DateTime.Now.Hour > 18) && (DateTime.Now.Hour < 23)? "Добрый вечер" : ((DateTime.Now.Hour > 23) && (DateTime.Now.Hour < 5)? "Доброй Ночь":"nan"));
            LoginInfo.Content = $"{state} {Global.CurrentUser.Name} {Global.CurrentUser.MiddleName}!";
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Chart.Children.Clear();
            Chart.Children.Add(new ContratsWithCompanies());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Chart.Children.Clear();
            Chart.Children.Add(new AgeOfPotentialEmployers());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Chart.Children.Clear();
            Chart.Children.Add(new Charts());
        }

        private void SessionsBtn_Click(object sender, RoutedEventArgs e)
        {
            Chart.Children.Clear();
            Chart.Children.Add(new SessionControls());
        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            Chart.Children.Clear();
            Chart.Children.Add(new TotalContracts());
        }
    }
}
