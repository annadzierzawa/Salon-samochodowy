using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;
    using System.Windows;

    class AddEmployeeVM : ViewModelBase
    {
        #region SkładowePrywatne

        private Model model = null;
        private string imie, nazwisko, login, password;
        private bool dodawanieDostepne = true;
        private bool edycjaDostepna = false;
        private int zaznaczonyPracownik;
        public ObservableCollection<string> pracownicyLista = new ObservableCollection<string>();

        #endregion


        #region Konstruktory
        public AddEmployeeVM(Model model)
        {
            this.model = model;
            Pracownicy = model.Pracownicy;
            ZaladujPracownikowDoListy();
        }

        private void ZaladujPracownikowDoListy()
        {
            pracownicyLista = new ObservableCollection<string>();
            foreach (var prac in Pracownicy)
            {
                pracownicyLista.Add($"{prac.Imie} {prac.Nazwisko}");
            }

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

        public int ZaznaczonyPracownik
        {
            get => zaznaczonyPracownik;
            set
            {
                zaznaczonyPracownik = value;
                onPropertyChanged(nameof(ZaznaczonyPracownik));
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

        public ObservableCollection<string> PracownicyLista
        {
            get => pracownicyLista;
            set
            {
                pracownicyLista = value;
                onPropertyChanged(nameof(PracownicyLista));
            }
        }
        #endregion

        //Czyszczenie formularza
        private void ClearAll()
        {
            Imie = "";
            Nazwisko = "";
            Login = "";
            Password = "";
            DodawanieDostepne = true;
            EdycjaDostepna = false;
        }

        //Ładowanie informacji do formularza
        private void LadujInformacje(int IdPracownika)
        {
            Imie = Pracownicy[IdPracownika].Imie;
            Nazwisko = Pracownicy[IdPracownika].Nazwisko;
            Login = Pracownicy[IdPracownika].Login ;
            Password = Pracownicy[IdPracownika].Password;
        }

        #region Komendy

        //Dodawanie pracownika
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
                        System.Windows.MessageBox.Show($"Pracownik został dodana do bazy! \n " +
                                                       $"{Imie} {Nazwisko} | {Login} {Password}");
                        ZaladujPracownikowDoListy();
                        ClearAll();
                        onPropertyChanged(nameof(pracownicyLista));
                        onPropertyChanged(nameof(imie));
                        onPropertyChanged(nameof(nazwisko));
                        onPropertyChanged(nameof(login));
                        onPropertyChanged(nameof(password));
                        
                    },
                    arg => (Imie != "") && (Nazwisko != "") && (Login != "") && (Password != "")
                );

                return dodaj;
            }
        }

        //Edycja pracownika
        private ICommand edytuj = null;
        public ICommand Edytuj
        {
            get
            {
                if (edytuj != null) return edytuj;
                edytuj = new RelayCommand(
                    arg =>
                    {
                        var pracownik = new Pracownik(Login, Password, Imie, Nazwisko, 0);
                        var idPracownika = ZaznaczonyPracownik;
                        sbyte id = model.CheckIDPracownika((sbyte)idPracownika);
                        
                        if (!model.EdytujPracownika(pracownik, id)) return;
                        System.Windows.MessageBox.Show($"Pracownik został edytowany! \n " +
                                                       $"{Imie} {Nazwisko} | {Login} {Password}");
                        ZaladujPracownikowDoListy();
                        ClearAll();
                        onPropertyChanged(nameof(pracownicyLista)); onPropertyChanged(nameof(imie));
                        onPropertyChanged(nameof(nazwisko));
                        onPropertyChanged(nameof(login));
                        onPropertyChanged(nameof(password));

                    },
                    arg => (Imie != "") && (Nazwisko != "") && (Login != "") && (Password != "")
                );

                return edytuj;
            }
        }

        //Usunięcie pracownika
        private ICommand usun = null;
        public ICommand Usun
        {
            get
            {
                if (usun != null) return usun;
                usun = new RelayCommand(
                    arg =>
                    {
                        var idToRemove = ZaznaczonyPracownik;
                        var s = model.CheckIDPracownika((sbyte)idToRemove);
                        model.UsunPracownika(s);
                        ZaladujPracownikowDoListy();
                        ClearAll();
                        onPropertyChanged(nameof(pracownicyLista));
                        MessageBox.Show($" Pracownik {Imie} {Nazwisko} został usunięty z bazy.");
                    },
                    arg => ZaznaczonyPracownik != -1
                );

                return usun;
            }
        }

        //Ładowanie danych do formularza 
        private ICommand zaladujInformacje = null;
        public ICommand ZaladujInformacje
        {
            get
            {
                if (zaladujInformacje == null)
                    zaladujInformacje = new RelayCommand(
                        arg =>
                        {
                            if (ZaznaczonyPracownik != -1)
                            {
                                LadujInformacje(ZaznaczonyPracownik);
                            }
                            else
                            {
                               // MessageBox.Show("Złe dane!");
                                ClearAll();
                            }
                        },
                        arg => true
                    );

                return zaladujInformacje;
            }
        }

        #endregion

    }
}