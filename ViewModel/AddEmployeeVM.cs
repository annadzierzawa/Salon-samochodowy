using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;

    class AddEmployeeVM : ViewModelBase
    {
        #region SkładowePrywatne

        private Model model = null;
        private string imie, nazwisko, login, password;
        private bool dodawanieDostepne = true;
        private bool edycjaDostepna = false;

        #endregion


        #region Konstruktory
        public AddEmployeeVM(Model model)
        {
            this.model = model;
            Pracownicy = model.Pracownicy;
        }

        #endregion


        #region Właściwości

        public ObservableCollection<Pracownik> Pracownicy { get; set; }
        public Pracownik BiezacyPracownik { get; set; }

        public string Imie
        {
            get => imie;
            set
            {
                imie = value;
                onPropertyChanged(nameof(Imie));
            }
        }

        public string Nazwisko
        {
            get => nazwisko;
            set
            {
                nazwisko = value;
                onPropertyChanged(nameof(Nazwisko));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                onPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                onPropertyChanged(nameof(Password));
            }
        }

        public bool DodawanieDostepne
        {
            get { return dodawanieDostepne; }
            set
            {
                dodawanieDostepne = value;
                onPropertyChanged(nameof(DodawanieDostepne));
            }
        }


        public bool EdycjaDostepna
        {
            get { return edycjaDostepna; }
            set
            {
                edycjaDostepna = value;
                onPropertyChanged(nameof(EdycjaDostepna));
            }
        }
        #endregion


        private void ClearAll()
        {
            Imie = "";
            Nazwisko = "";
            Login = "";
            Password = "";
            DodawanieDostepne = true;
            EdycjaDostepna = false;
        }


        #region Komendy

        private ICommand dodaj = null;
        public ICommand Dodaj
        {
            get
            {
                if (dodaj != null) return dodaj;
                dodaj = new RelayCommand(
                    arg =>
                    {
                        var pracownik = new Pracownik(Login, Password, Imie, Nazwisko, 0);
                        if (!model.DodajPracownika(pracownik)) return;
                        ClearAll();
                        System.Windows.MessageBox.Show($"Pracownik została dodana do bazy! \n " +
                                                       $"{Imie} {Nazwisko} | {Login} {Password}");
                    },
                    arg => (Imie != "") && (Nazwisko != "") && (Login != "") && (Password != "")
                );

                return dodaj;
            }
        }


        #endregion

    }
}