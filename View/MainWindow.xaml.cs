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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ItemCreate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Model.Model model = new Model.Model();
            DataContext = new AddCarVM(model);
        }

        private void About_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext = new AboutVM();
        }

        private void Exit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ItemHome_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext = new SellingVM();
        }
    }
}
