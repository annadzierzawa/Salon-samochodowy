using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using DAL.Encje;
    using Model;

    class AddCarVM : ViewModelBase
    {
        #region Składowe prywatne

        private Model model = null;
        private string marka, modelPojazdu, krajProdukcji, kolor, silnik, rokProdukcji;
        private double cena;
        private int moc;
        private int zaznaczonySamochod;
        public ObservableCollection<string> samochodyLista = new ObservableCollection<string>();

        #endregion

        #region Konstruktory

        //Konkstruktor przyjmujący obiekt modelu
        public AddCarVM(Model model)
        {
            this.model = model;
            ZaladujSamochodyDoListy();
        }

        public void ZaladujSamochodyDoListy()
        {
            Samochody = model.Samochody;
            samochodyLista = new ObservableCollection<string>();
            foreach (var samochod in Samochody)
            {
                samochodyLista.Add($"{samochod.Marka} {samochod.ModelPojazdu}");
            }
        }

        #endregion

        #region Właściwości

        public ObservableCollection<Samochod> Samochody { get; set; }
        
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

        public ObservableCollection<string> SamochodyLista
        {
            get => samochodyLista;
            set
            {
                samochodyLista = value;
                onPropertyChanged(nameof(SamochodyLista));
            }
        }

        #endregion

        //Czyszczenie formularza
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

        //Ładowanie informacji do formularza
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


        //Dodawanie samochodu
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
                        ZaladujSamochodyDoListy();
                        ClearAll();
                        onPropertyChanged(nameof(samochodyLista));
                        onPropertyChanged(nameof(marka));
                        onPropertyChanged(nameof(modelPojazdu));
                        onPropertyChanged(nameof(cena));
                        onPropertyChanged(nameof(moc));
                        onPropertyChanged(nameof(kolor));
                        onPropertyChanged(nameof(rokProdukcji));
                        onPropertyChanged(nameof(krajProdukcji));
                        onPropertyChanged(nameof(silnik));
                        System.Windows.MessageBox.Show($"{Marka} {ModelPojazdu} został dodany do bazy!");
                    },
                    arg => (Marka != "") && (ModelPojazdu != "") && (Silnik != "") && (Kolor != "") && (KrajProdukcji != "") && (RokProdukcji != "") && (Moc != 0) && (Cena != 0)
                    );
                return dodajSamochod;
            }
        }

        //Edycja samochodu
        private ICommand edytuj = null;
        public ICommand Edytuj
        {
            get
            {
                if (edytuj != null) return edytuj;
                edytuj = new RelayCommand(
                    arg =>
                    {
                        var samochod = new Samochod(Marka, modelPojazdu, Silnik, Kolor, KrajProdukcji, rokProdukcji, Cena, Moc);
                        var idSamo = ZaznaczonySamochod;
                        sbyte id = model.CheckIDSamochodu((sbyte)idSamo);

                        if (!model.EdytujSamochod(samochod, id)) return;
                        System.Windows.MessageBox.Show($"Samochod został edytowany! \n " +
                                                       $"{Marka} {modelPojazdu}");
                        ZaladujSamochodyDoListy();
                        ClearAll();
                        onPropertyChanged(nameof(samochodyLista));
                        onPropertyChanged(nameof(marka));
                        onPropertyChanged(nameof(modelPojazdu));
                        onPropertyChanged(nameof(cena));
                        onPropertyChanged(nameof(moc));
                        onPropertyChanged(nameof(kolor));
                        onPropertyChanged(nameof(rokProdukcji));
                        onPropertyChanged(nameof(krajProdukcji));
                        onPropertyChanged(nameof(silnik));

                    },
                    arg => (Marka != "") && (ModelPojazdu != "") && (Silnik != "") && (Kolor != "") && (KrajProdukcji != "") && (RokProdukcji != "") && (Cena > 0) && (Moc > 0)
                );

                return edytuj;
            }
        }

        //Usunięcie samochodu
        private ICommand usun = null;
        public ICommand Usun
        {
            get
            {
                if (usun != null) return usun;
                usun = new RelayCommand(
                    arg =>
                    {
                        var idToRemove = ZaznaczonySamochod;
                        var s = model.CheckIDSamochodu((sbyte)idToRemove);
                        model.UsunSamochod(s);
                        MessageBox.Show($" Samochod {Marka} {modelPojazdu} został usunięty z bazy.");
                        ZaladujSamochodyDoListy();
                        onPropertyChanged(nameof(samochodyLista));
                    },
                    arg => ZaznaczonySamochod != -1
                );

                return usun;
            }
        }

        //Ładowanie informacji w formularzu
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
                                //MessageBox.Show("Złe dane!");
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
