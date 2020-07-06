using Salon_samochodowy.View;
using Salon_samochodowy.ViewModel;
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

namespace Salon_samochodowy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model.Model model;
        public UserContext user;
        public MainWindow()
        {
            InitializeComponent();
            model = new Model.Model();
            DataContext = new SellingVM(model);
            user = UserContext.Instance;
            txtUserName.Text = user.CurrentUser.Imie + " " + user.CurrentUser.Nazwisko;
        }

        private void ItemCreate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext = new AddCarVM(model);
        }

        private void About_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext = new AboutVM();
        }

        private void Exit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ItemHome_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext = new SellingVM(model);
        }

        private void Changepassword_OnClick(object sender, RoutedEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.Show();
        }

        private void SellersStats_OnClick(object sender, RoutedEventArgs e)
        {
            if (user.CurrentUser.Id != 1)
            {
                MessageBox.Show("Nie jesteś właścicielem, nie masz uprawnień do przeglądania tej zawartości.");
                return;
            }
            DataContext = new SellersStatsVM(model);
        }

        private void AddEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            if (user.CurrentUser.Id != 1)
            {
                MessageBox.Show("Nie jesteś właścicielem, nie masz uprawnień do przeglądania tej zawartości.");
                return;
            }
            DataContext = new AddEmployeeVM(model);
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            LoginScreen ls = new LoginScreen();
            ls.Show();
            Close();
        }

        private void AccountInfo_OnClick(object sender, RoutedEventArgs e)
        {
            AboutAcc ab = new AboutAcc();
            ab.Show();
        }

        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutVM();
        }
    }
}
