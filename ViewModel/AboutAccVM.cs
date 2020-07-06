using System.Windows.Input;

namespace Salon_samochodowy.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Encje;
    
    class AboutAccVM : ViewModelBase
    {
        #region SkładowePrywatne

        private Model model = null;
        private Pracownik zalogowany;
        private string imieNazwisko, id, typUsera;

        #endregion

        #region Konstruktor

        public AboutAccVM(Model model)
        {
            this.model = model;
            zalogowany = model.Zalogowany;
        }

        #endregion

        #region Właściwości

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

        #endregion

        public void ZaladujDane(Pracownik pr)
        {

            imieNazwisko = pr.Imie + " " + pr.Nazwisko;
            id = pr.Id.ToString();
            typUsera = pr.Id == 1 ? "Właściciel" : "Pracownik";

            onPropertyChanged(nameof(imieNazwisko));
            onPropertyChanged(nameof(id));
            onPropertyChanged(nameof(typUsera));
        }

        #region Komendy

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

        #endregion


    }
}