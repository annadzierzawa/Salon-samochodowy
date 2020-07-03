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
    using System.Collections.ObjectModel;
    using System.Data.SqlTypes;
    using System.Windows.Input;

    class AddCarVM : ViewModelBase
    {
        #region Składowe prywatne

        private Model model = null;
        private string marka, modelPojazdu, krajProdukcji, kolor, silnik, rokProdukcji;
        private double cena;
        private int moc;

        #endregion

        #region Konstruktory

        public AddCarVM(Model model)
        {
            this.model = model;
            Samochody = model.Samochody;
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

        #region Komendy

        private ICommand dodajSamochod = null;
        public ICommand DodajSamochod
        {
            get
            {
                if (dodajSamochod != null)
                    return dodajSamochod;
                dodajSamochod = new RelayCommand(
                    arg =>
                    {
                        var samochod = new Samochod(Marka, modelPojazdu, Silnik, Kolor, KrajProdukcji, rokProdukcji, Cena, Moc);
                        if (!model.DodajSamochod(samochod))
                            return;
                        ClearAll();
                        System.Windows.MessageBox.Show($"{Marka} {ModelPojazdu} został dodany do bazy!");
                    },
                    arg => (Marka != "") && (ModelPojazdu != "") && (Silnik != "") && (Kolor != "") && (KrajProdukcji != "") && (RokProdukcji != "") && (Cena > 0) && (Moc > 0)
                    );
                return dodajSamochod;
            }
        }

        #endregion

    }
}
