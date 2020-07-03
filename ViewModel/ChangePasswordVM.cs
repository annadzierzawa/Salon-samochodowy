

using System.Windows;
using System.Windows.Input;
using Salon_samochodowy.DAL.Repozytoria;
using Salon_samochodowy.View;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;

    class ChangePasswordVM : ViewModelBase
    {
        #region SkładowePrywatne

        private Model model = null;
        private string oldPassword, newPassword;
        private bool changeEnable;

        #endregion

        #region Konstruktory


        public ChangePasswordVM(Model model)
        {
            this.model = model;
            Pracownicy = model.Pracownicy;
        }

        #endregion

        #region Właściwości

        public ObservableCollection<Pracownik> Pracownicy { get; set; }

        public string OldPassword
        {
            get => oldPassword;
            set
            {
                oldPassword = value;
                onPropertyChanged(nameof(OldPassword));
            }
        }

        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                onPropertyChanged(nameof(NewPassword));
            }
        }

        public bool ChangeEnable
        {
            get => changeEnable;
            set
            {
                changeEnable = value;
                onPropertyChanged(nameof(ChangeEnable));
            }
        }


        private void ClearAll()
        {
            OldPassword = "";
            NewPassword = "";
            ChangeEnable = true;
        }

        #endregion

        #region Komendy

        private ICommand zmieniaj = null;
        public ICommand Zmieniaj
        {
            get
            {
                if (zmieniaj == null)
                    zmieniaj = new RelayCommand(
                        arg =>
                        {
                            var userContext = UserContext.Instance;
                            var pracownik = userContext.CurrentUser;

                            if (pracownik.Password == OldPassword)
                            {
                                pracownik.Password = NewPassword;
                                if (pracownik.Id != null)
                                    RepoPracowników.EdytujPracownika(pracownik, (sbyte) pracownik.Id);

                                MessageBox.Show("Pomyślnie zmieniono hasło!");
                                var changepass = (ChangePassword)arg;
                                changepass.Close();
                            }
                            else
                            {
                                MessageBox.Show("Złe dane!");
                                ClearAll();
                            }
                        },
                        arg => (OldPassword != "") && (NewPassword != "")
                    );

                return zmieniaj;
            }
        }


        #endregion

    }
}