using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();

            using (var context = new AppDbContext())
            {
                context.InitDB();
                Role r = new Role();
                r.Name = "User";
                context.Roles.Add(r);
                context.SaveChanges();
                User a = new User();
                a.Login = "Andrey";
                a.Password = "123";
                a.RoleId = (int)Roless.User;
                a.Name = "Андрей";
                a.SurName = "Звягин";
                a.MiddleName = "Владиславович";
                Role ro = new Role();
                ro.Name = "Director";
                context.Roles.Add(ro);
                User aa = new User();
                aa.Login = "s";
                aa.Password = "s";
                aa.RoleId = (int)Roless.Director;
                aa.Name = "Ярослав";
                aa.SurName = "Сапожников";
                aa.MiddleName = "Витальевич";
                context.Users.Add(a);
                context.Users.Add(aa);
                context.SaveChanges();
                //for (int i = 0; i < 2; i++) { 
                //LogIn log = new LogIn();
                //log.UserId = context.Users.ToList()[i].Id;
                //log.SessionStart = DateTime.Now;
                //log.SessionEnd = DateTime.Now;
                //context.LogIns.Add(log);
                //}
                //context.SaveChanges();
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext()) 
            {
                foreach (var User in context.Users.ToList())
                {
                    if (User.Login == LoginTBox.Text && User.Password == PasswordBox.Password)
                    {
                        Global.CurrentUser = User;
                        switch (User.RoleId)
                        {
                            case 1:
                                GenUsers(10);
                                GenClients(10);
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 2:

                                GenUsers(10);
                                GenClients(10);
                                NavigationService.Navigate(new MainPage());
                                break;
                        }
                    }
                }
            }
        }

        

        void GenClients(int count)
        {
            List<string> slavicNames = new List<string>
        {
            "Владимир", "Наталия", "Станислав", "Мария", "Игорь", "Александра", "Андрей", "Ольга", "Петр", "Екатерина",
            "Алексей", "Татьяна", "Василий", "Надежда", "Дмитрий", "Любовь", "Иван", "Елена", "Сергей", "Юлия",
            "Валентин", "Анна", "Даниил", "Ярослав", "Евгения", "Владислав", "Оксана", "Максим", "Светлана", "Николай",
            "Галина", "Роман", "Лариса", "Степан", "Елена", "Виктор", "Вера", "Денис", "Тамара", "Артем",
            "Валентина", "Ирина", "Влад", "Ольга", "Владислава", "Никита", "Юлия", "Егор", "Татьяна",
            "Маргарита", "Кирилл", "Екатерина", "Илья", "Анастасия", "Павел", "Алёна", "Сергей", "Александра",
            "Семён", "Марина", "Борис", "Антонина", "Фёдор", "Наталья", "Алексей", "Валентина", "Александр",
            "Наталья", "Иван", "Светлана", "Дмитрий", "Жанна", "Андрей", "Юлия", "Александр", "Елена",
            "Игорь", "Оксана", "Владимир", "Мария", "Валерий", "Людмила", "Андрей", "Валентина", "Пётр",
            "Наталья", "Михаил", "Татьяна", "Александр", "Ольга", "Владимир", "Елена", "Артём", "Виктория",
            "Сергей", "Анна", "Алексей", "Антонина", "Юрий", "Тамара", "Николай", "Ирина", "Евгений"
        };
            using (var context = new AppDbContext())
            {
                Random r = new Random();
                for (int i = 0; i < count; i++)
                {
                    Client u = new Client();
                    string name = slavicNames[r.Next(0, slavicNames.Count - 1)];
                    if (context.Users.Where(n => n.Login == name).Count() == 0)
                    {
                        u.Name = slavicNames[r.Next(0, slavicNames.Count - 1)];
                        u.SurName = "";
                        u.MiddleName = "";
                        u.Company = context.Companies.ToList()[r.Next(0, context.Companies.ToList().Count - 1)];
                        u.BirthdayDate = new DateTime(r.Next(1950, 2000), 3, 1);
                        context.Clients.Add(u);
                        
                    }
                }
                context.SaveChanges();
            }
        }
        void GenUsers(int amount)
        {
            List<string> slavicNames = new List<string>
        {
            "Владимир", "Наталия", "Станислав", "Мария", "Игорь", "Александра", "Андрей", "Ольга", "Петр", "Екатерина",
            "Алексей", "Татьяна", "Василий", "Надежда", "Дмитрий", "Любовь", "Иван", "Елена", "Сергей", "Юлия",
            "Валентин", "Анна", "Даниил", "Ярослав", "Евгения", "Владислав", "Оксана", "Максим", "Светлана", "Николай",
            "Галина", "Роман", "Лариса", "Степан", "Елена", "Виктор", "Вера", "Денис", "Тамара", "Артем",
            "Валентина", "Ирина", "Влад", "Ольга", "Владислава", "Никита", "Юлия", "Егор", "Татьяна",
            "Маргарита", "Кирилл", "Екатерина", "Илья", "Анастасия", "Павел", "Алёна", "Сергей", "Александра",
            "Семён", "Марина", "Борис", "Антонина", "Фёдор", "Наталья", "Алексей", "Валентина", "Александр",
            "Наталья", "Иван", "Светлана", "Дмитрий", "Жанна", "Андрей", "Юлия", "Александр", "Елена",
            "Игорь", "Оксана", "Владимир", "Мария", "Валерий", "Людмила", "Андрей", "Валентина", "Пётр",
            "Наталья", "Михаил", "Татьяна", "Александр", "Ольга", "Владимир", "Елена", "Артём", "Виктория",
            "Сергей", "Анна", "Алексей", "Антонина", "Юрий", "Тамара", "Николай", "Ирина", "Евгений"
        };
            using (AppDbContext context = new AppDbContext())
            {
                Random r = new Random();
                for(int i=0; i < amount; i++)
                {
                    User u = new User();
                    string name = slavicNames[r.Next(0, slavicNames.Count - 1)];
                    if(context.Users.Where(n=>n.Login == name).Count() == 0) 
                    { 
                        u.Login = name;
                        u.Password = GeneratePassword(10);
                        u.Role = context.Roles.ToList()[0];
                        u.Name = slavicNames[r.Next(0, slavicNames.Count - 1)];
                        u.SurName = "";
                        u.MiddleName = "";
                        u.BirthdayDate = new DateTime(r.Next(1950,2000),3,1);
                        context.Users.Add(u);
                        context.SaveChanges();
                    }
                }
                
            }
            
        }
        string GeneratePassword(int length)
        {
            const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(CharSet.Length);
                password.Append(CharSet[index]);
            }

            return password.ToString();
        }
    }
}
