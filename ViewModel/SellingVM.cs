using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using DAL.Encje;
    using Model;
    using Salon_samochodowy.DAL.Repozytoria;
    using Salon_samochodowy.View;
    using System.Collections.ObjectModel;
    using System.Data.SqlTypes;
    using System.Windows;
    using System.Windows.Input;

    class SellingVM : ViewModelBase
    {
        #region Składowe prywatne

        private Model model = null;
        private string marka, modelPojazdu, krajProdukcji, kolor, silnik, rokProdukcji;
        private double cena;
        private int moc;
        private int zaznaczonySamochod;
        public List<string> samochodyLista = new List<string>();

        #endregion

        #region Konstruktory

        public SellingVM(Model model)
        {
            this.model = model;
            Samochody = model.Samochody;
            foreach (var samochod in Samochody)
            {
                samochodyLista.Add($"{samochod.Marka} {samochod.ModelPojazdu} z rocznika {samochod.DataProdukcji} w cenie {samochod.Cena} zł");
            }
        }

        #endregion

        #region Właściwości

        public ObservableCollection<Samochod> Samochody { get; set; }
        public Samochod BiezacySamochod { get; set; }

        public string Marka
        {
            get => marka;
            set
            {
                marka = value;
                onPropertyChanged(nameof(Marka));
            }
        }

        public int ZaznaczonySamochod
        {
            get => zaznaczonySamochod;
            set
            {
                zaznaczonySamochod = value;
                onPropertyChanged(nameof(ZaznaczonySamochod));
            }
        }

        public string ModelPojazdu
        {
            get => modelPojazdu;
            set
            {
                modelPojazdu = value;
                onPropertyChanged(nameof(ModelPojazdu));
            }
        }

        public string KrajProdukcji
        {
            get => krajProdukcji;
            set
            {
                krajProdukcji = value;
                onPropertyChanged(nameof(KrajProdukcji));
            }
        }

        public string Kolor
        {
            get => kolor;
            set
            {
                kolor = value;
                onPropertyChanged(nameof(Kolor));
            }
        }

        public string Silnik
        {
            get => silnik;
            set
            {
                silnik = value;
                onPropertyChanged(nameof(Silnik));
            }
        }

        public double Cena
        {
            get => cena;
            set
            {
                cena = value;
                onPropertyChanged(nameof(Cena));
            }
        }

        public int Moc
        {
            get => moc;
            set
            {
                moc = value;
                onPropertyChanged(nameof(Moc));
            }
        }

        public string RokProdukcji
        {
            get => rokProdukcji;
            set
            {
                rokProdukcji = value;
                onPropertyChanged(nameof(RokProdukcji));
            }
        }

        public List<string> SamochodyLista
        {
            get => samochodyLista;
            set
            {
                samochodyLista = value;
                onPropertyChanged(nameof(SamochodyLista));
            }
        }

        #endregion

        private void ClearAll()
        {
            Marka = "";
            ModelPojazdu = "";
            KrajProdukcji = "";
            Kolor = "";
            Silnik = "";
            Cena = 0;
            Moc = 0;
            RokProdukcji = "";
        }

        private void LadujInformacje(int IdSamochodu)
        {
            Marka = Samochody[IdSamochodu].Marka;
            ModelPojazdu = Samochody[IdSamochodu].ModelPojazdu;
            KrajProdukcji = Samochody[IdSamochodu].KrajProdukcji;
            Kolor = Samochody[IdSamochodu].Kolor;
            Silnik = Samochody[IdSamochodu].Silnik;
            Cena = Samochody[IdSamochodu].Cena;
            Moc = Samochody[IdSamochodu].Moc;
            RokProdukcji = Samochody[IdSamochodu].DataProdukcji;
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

                            if (ZaznaczonySamochod != -1)
                            {
                                LadujInformacje(ZaznaczonySamochod);
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

        private ICommand sprzedajPojazd = null;
        public ICommand SprzedajPojazd
        {
            get
            {
                if (sprzedajPojazd == null)
                    sprzedajPojazd = new RelayCommand(
                        arg =>
                        {
                            var sprzedaz = new Sprzedaz(Convert.ToSByte(model.Zalogowany.Id), Convert.ToSByte(zaznaczonySamochod + 1), Cena);
                            if (!model.DodajSprzedaz(sprzedaz))
                                return;
                            MessageBox.Show("Pojazd sprzedany!");
                        },
                        arg => true
                        ); 
                return sprzedajPojazd;
            }
        }

        #endregion
    }
}
