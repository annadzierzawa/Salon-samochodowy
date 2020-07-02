using System.Windows.Input;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;

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
                            System.Windows.MessageBox.Show(" pomyslnie");
                            if (model.ZnajdzPracownikaPoLoginie(Login) != null)
                            {
                                var pracownik = model.ZnajdzPracownikaPoLoginie(Login);
                                if (Password == pracownik.Password)
                                {
                                    System.Windows.MessageBox.Show("Zalogowano pomyslnie");
                                }
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