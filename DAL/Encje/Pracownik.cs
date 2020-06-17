using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Encje
{
    public class Pracownik
    {

        #region Pola
        public sbyte? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public double Premia { get; set; }
        #endregion


        #region Konstruktory

        //obiekt na podstawie MySQLDataReader
        public Pracownik(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["idPracownika"].ToString());
            Login = reader["login"].ToString();
            Password = reader["password"].ToString();
            Imie = reader["imie"].ToString();
            Nazwisko = reader["nazwisko"].ToString();
            Premia = double.Parse(reader["premia"].ToString());
        }

        //obiekt który nie istnieje w bazie - brak ID
        public Pracownik(string login, string password, string imie, string nazwisko, double premia)
        {
            Id = null;
            Login = login.Trim();
            Password = password.Trim();
            Imie = imie.Trim();
            Nazwisko = nazwisko.Trim();
            Premia = premia;
        }

        //kopiowanie obiektu
        public Pracownik(Pracownik pracownik)
        {
            Id = pracownik.Id;
            Login = pracownik.Login;
            Password = pracownik.Password;
            Imie = pracownik.Imie;
            Nazwisko = pracownik.Nazwisko;
            Premia = pracownik.Premia;
        }

        #endregion



        #region Metody
        //metoda generuje string dla INSERT
        public string ToInsert()
        {
            return $"('{Login}', '{Password}', {Imie},'{Nazwisko}', '{Premia}')";
        }

        //przeciążenie dla Contains w listach
        public override bool Equals(object obj)
        {
            //nie porównujemy ID
            var pracownik = obj as Pracownik;
            if (pracownik is null) return false;
            if (Login.ToLower() != pracownik.Login.ToLower()) return false;
            if (Password.ToLower() != pracownik.Password.ToLower()) return false;
            if (Imie.ToLower() != pracownik.Imie.ToLower()) return false;
            if (Nazwisko.ToLower() != pracownik.Nazwisko.ToLower()) return false;
            if (Premia != pracownik.Premia) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}