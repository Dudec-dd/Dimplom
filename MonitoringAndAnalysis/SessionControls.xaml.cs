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
    /// Логика взаимодействия для SessionControls.xaml
    /// </summary>
    public partial class SessionControls : UserControl
    {
        public SessionControls()
        {
            InitializeComponent();
            List<LoginToShow> res = new List<LoginToShow>();
            using(AppDbContext context = new AppDbContext())
            {
                foreach (var log in context.LogIns)
                {
                    string fullName = $"{context.Users.First(u => u.Id == log.UserId).Name}" +
                        $" {context.Users.First(u => u.Id == log.UserId).SurName}" +
                        $" {context.Users.First(u => u.Id == log.UserId).MiddleName}"; 
                    LoginToShow l = new LoginToShow(log.UserId, fullName, log.SessionStart,log.SessionEnd);
                    res.Add(l);
                }
            }
            DG.ItemsSource = res;
        }
    }
    public class LoginToShow
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime {  get; set; }
        public DateTime EndTime { get; set; }
        public LoginToShow(int id, string username, DateTime starttime, DateTime endtime) 
        {
            Id = id;
            UserName = username;
            StartTime = starttime;
            EndTime = endtime;
        }
    }
}
