using System.Windows.Input;
using Salon_samochodowy.View;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;
    using System.Windows;

    class LoginVM : ViewModelBase
    {
        #region SkładowePrywatne

        private Model model = null;
        private string login, password;
        private bool loginDostepny = true;

        #endregion

        #region Konstruktor

        public LoginVM(Model model)
        {
            this.model = model;
            Pracownicy = model.Pracownicy;
        }

        #endregion

        #region Właściwości
        public ObservableCollection<Pracownik> Pracownicy { get; set; }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                onPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                onPropertyChanged(nameof(Password));
            }
        }
        public bool LoginDostepny
        {
            get { return loginDostepny; }
            set
            {
                loginDostepny = value;
                onPropertyChanged(nameof(LoginDostepny));
            }
        }

        #endregion


        private void ClearAll()
        {
            Login = "";
            Password = "";
            LoginDostepny = false;
        }


        #region Komendy

        private ICommand loguj = null;
        public ICommand Loguj
        {
            get
            {
                if (loguj == null)
                    loguj = new RelayCommand(
                        arg =>
                        {
                            var userContext = UserContext.Instance;
                            var pracownik = model.ZnajdzPracownikaPoLoginie(Login);
                            if (pracownik != null && Password == pracownik.Password)
                            {
                                model.Zalogowany = pracownik;
                                userContext.CurrentUser = pracownik;
                                var mainWindow = new MainWindow();
                                mainWindow.Show();
                                var loginWindow = (LoginScreen)arg;
                                loginWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show("Złe dane!");
                                ClearAll();
                            }
                        },
                        arg => (Login != "") && (Password != "")
                    );

                return loguj;
            }
        }

        #endregion

    }
}