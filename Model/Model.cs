using System.Linq;

namespace Salon_samochodowy.Model
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;

    public class Model
    {
        //bazy danych
        public ObservableCollection<Pracownik> Pracownicy { get; set; } = new ObservableCollection<Pracownik>();
        public ObservableCollection<Samochod> Samochody { get; set; } = new ObservableCollection<Samochod>();
        public ObservableCollection<Sprzedaz> Sprzedaze { get; set; } = new ObservableCollection<Sprzedaz>();

        public Pracownik Zalogowany { get; set; }


        public Model()
        {
            //pobranie danych do kolekcjii
            var pracownicy = RepoPracowników.PobierzWszystkichPracownikow();
            var samochody = RepoSamochody.PobierzWszystkieSamochody();
            var sprzedaze = RepoSprzedazy.PobierzWszystkieSprzedaze();

            foreach (var p in pracownicy)
            {
                Pracownicy.Add(p);
            }
            foreach (var s in samochody)
            {
                Samochody.Add(s);
            }
            foreach (var sp in sprzedaze)
            {
                Sprzedaze.Add(sp);
            }

            Zalogowany = null;
        }


        private Pracownik ZnajdzPracownikaPoID(sbyte id)
        {
            return Pracownicy.FirstOrDefault(p => p.Id == id);
        }

        public Pracownik ZnajdzPracownikaPoLoginie(string login)
        {
            return Pracownicy.FirstOrDefault(p => p.Login == login);
        }

        private Samochod ZnajdzSamochodPoID(sbyte id)
        {
            return Samochody.FirstOrDefault(s => s.Id == id);
        }

        private Sprzedaz ZnajdzSprzedazPoID(sbyte id)
        {
            return Sprzedaze.FirstOrDefault(sp => sp.IdSprzedazy == id);
        }

        public bool IfSprzedazInDB(Sprzedaz sprzedaz) => Sprzedaze.Contains(sprzedaz);

        public bool DodajSprzedaz(Sprzedaz sprzedaz)
        {
            if (IfSprzedazInDB(sprzedaz)) return false;
            if (!RepoSprzedazy.DodajSprzedazDoBazy(sprzedaz)) return false;
            Sprzedaze.Add(sprzedaz);
            return true;
        }

        public bool IfPracownikInDB(Pracownik pracownik) => Pracownicy.Contains(pracownik);

        public bool DodajPracownika(Pracownik pracownik)
        {
            if (IfPracownikInDB(pracownik)) return false;
            if (!RepoPracowników.DodajPracownikaDoBazy(pracownik)) return false;
            Pracownicy.Add(pracownik);
            return true;
        }

        public bool IfSamochodInDB(Samochod samochod) => Samochody.Contains(samochod);

        public bool DodajSamochod(Samochod samochod)
        {
            if (IfSamochodInDB(samochod)) return false;
            if (!RepoSamochody.DodajSamochodDoBazy(samochod)) return false;
            Samochody.Add(samochod);
            return true;
        }

        public bool EdytujPracownika(Pracownik pracownik, sbyte idPracownika)
        {
            if (!RepoPracowników.EdytujPracownika(pracownik, idPracownika)) return false;
            for (int i = 0; i < Pracownicy.Count; i++)
            {
                if (Pracownicy[i].Id != idPracownika) continue;
                pracownik.Id = idPracownika;
                Pracownicy[i] = new Pracownik(pracownik);
            }

            return true;
        }


    }
  
}