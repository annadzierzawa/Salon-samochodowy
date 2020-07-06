using System.Windows;
using System.Windows.Input;
using Salon_samochodowy.DAL.Encje;
using Salon_samochodowy.DAL.Repozytoria;
using Salon_samochodowy.ViewModel.BaseClass;
using Salon_samochodowy.Model;
using Salon_samochodowy.View;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    using System.Collections.ObjectModel;

    class AboutAccVM : ViewModelBase
    {
        private Model model = null;
        private Pracownik zalogowany;
        private string imieNazwisko, id, typUsera;


        public AboutAccVM(Model model)
        {
            this.model = model;
            zalogowany = model.Zalogowany;
           
        }

        public string ImieNazwisko
        {
            get => imieNazwisko;
            set
            {
                imieNazwisko = value;
                onPropertyChanged(nameof(ImieNazwisko));
            }
        }
        public string Id
        {
            get => id;
            set
            {
                id = value;
                onPropertyChanged(nameof(Id));
            }
        }
        public string TypUsera
        {
            get => typUsera;
            set
            {
                typUsera = value;
                onPropertyChanged(nameof(TypUsera));
            }
        }

        public void ZaladujDane(Pracownik pr)
        {

            imieNazwisko = pr.Imie + " " + pr.Nazwisko;
            id = pr.Id.ToString();
            typUsera = pr.Id == 1 ? "Właściciel" : "Pracownik";

            onPropertyChanged(nameof(imieNazwisko));
            onPropertyChanged(nameof(id));
            onPropertyChanged(nameof(typUsera));
        }


        private ICommand laduj = null;
        public ICommand Laduj
        {
            get
            {
                if (laduj == null)
                    laduj = new RelayCommand(
                        arg =>
                        {
                            ZaladujDane(zalogowany);
                        },
                        arg => (true)
                    );

                return laduj;
            }
        }

    }
}