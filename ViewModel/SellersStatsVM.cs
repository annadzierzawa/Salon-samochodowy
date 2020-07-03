using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Salon_samochodowy.View;
using Salon_samochodowy.ViewModel.BaseClass;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;

    class SellersStatsVM : ViewModelBase
    {

        #region SkładowePrywatne

        private Model model = null;
        private string pracownik;
        private string sprzedanePojazdy;
        private string cenaSprzedanychPojazdow, premiaZeSprzedazy;
        private int zaznaczonyPracownik;
        public List<string> pracownicyLista = new List<string>();
        

        #endregion

        #region Konstruktory

        public SellersStatsVM(Model model)
        {
            this.model = model;
            Pracownicy = model.Pracownicy;
            Sprzedaze = model.Sprzedaze;
            foreach (var prac in Pracownicy)
            {
                pracownicyLista.Add($"{prac.Imie} {prac.Nazwisko}");
            }
        }

        #endregion

        #region Właściwości
        public ObservableCollection<Pracownik> Pracownicy { get; set; }
        public ObservableCollection<Sprzedaz> Sprzedaze { get; set; }

        public int ZaznaczonyPracownik
        {
            get => zaznaczonyPracownik;
            set
            {
                zaznaczonyPracownik = value;
                onPropertyChanged(nameof(ZaznaczonyPracownik));
            }
        }

        public string Pracownik
        {
            get => pracownik;
            set
            {
                pracownik = value;
                onPropertyChanged(nameof(Pracownik));
            }
        }

        public List<string> PracownicyLista
        {
            get => pracownicyLista;
            set
            {
                pracownicyLista = value;
                onPropertyChanged(nameof(PracownicyLista));
            }
        }

        public string CenaSprzedanychPojazdow
        {
            get => cenaSprzedanychPojazdow;
            set
            {
                cenaSprzedanychPojazdow = value;
                onPropertyChanged(nameof(CenaSprzedanychPojazdow));
            }
        }

        public string PremiaZeSprzedazy
        {
            get => premiaZeSprzedazy;
            set
            {
                premiaZeSprzedazy = value;
                onPropertyChanged(nameof(PremiaZeSprzedazy));
            }
        }

        public string SprzedanePojazdy
        {
            get => sprzedanePojazdy;
            set
            {
                sprzedanePojazdy = value;
                onPropertyChanged(nameof(SprzedanePojazdy));
            }
        }
        

        #endregion

        private void ClearAll()
        {
            Pracownik = "";
            SprzedanePojazdy = "";
            CenaSprzedanychPojazdow = "";
            PremiaZeSprzedazy = "";
        }

        private void LadujInformacje(int IdPracownika)
        {
            double cenaSuma = 0;
            double premiasuma = 0;
            int sprzedaneCount = 0;
            
            foreach (var s in Sprzedaze)
            {
                if (IdPracownika + 1 == s.IdPracownika)
                {
                    cenaSuma += s.Cena;
                    premiasuma += 0.02 * s.Cena;
                    sprzedaneCount += 1;
                }
            }

            Pracownik = Pracownicy[IdPracownika].Imie + " " + Pracownicy[IdPracownika].Nazwisko;
            CenaSprzedanychPojazdow = cenaSuma.ToString();
            PremiaZeSprzedazy = premiasuma.ToString();
            SprzedanePojazdy = sprzedaneCount.ToString();
            
        }

        #region Komendy

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
                                MessageBox.Show("Złe dane!");
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