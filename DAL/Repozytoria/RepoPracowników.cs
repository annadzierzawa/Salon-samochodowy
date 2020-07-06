using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Repozytoria
{
    using Encje;
    public class RepoPracowników
    {
        #region SQL_QUERIES
        private const string WSZYSCY_PRACOWNICY = "SELECT * FROM Pracownicy";
        private const string DODAJ_PRACOWNIKA = "INSERT INTO `Pracownicy`(`login`, `password`, `imie`, `nazwisko`,`premia`) VALUES ";
        private const string USUN_PRACOWNIKA = "DELETE FROM `Pracownicy` WHERE idPracownika=";
        private const string USUN_SPRZEDAZE_PRACOWNIKA = "DELETE FROM `Sprzedaz` WHERE idPracownika=";
        #endregion

        #region CRUD

        //MYSQL - pobranie wszystkich Pracowników
        public static List<Pracownik> PobierzWszystkichPracownikow()
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSCY_PRACOWNICY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) 
                    pracownicy.Add(new Pracownik(reader));
                connection.Close();
            }

            return pracownicy;
        }

        //MYSQL - Dodanie pracownika do bazy
        public static bool DodajPracownikaDoBazy(Pracownik pracownik)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_PRACOWNIKA} {pracownik.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                pracownik.Id = (sbyte) command.LastInsertedId;
                connection.Close();
            }

            return stan;
        }

        //MYSQL - Edytowanie pracownika w bazie
        public static bool EdytujPracownika(Pracownik pracownik, sbyte idPracownika)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ_PRACOWNIKA = $"UPDATE Pracownicy SET login='{pracownik.Login}', password='{pracownik.Password}', " +
                                      $"imie='{pracownik.Imie}', nazwisko='{pracownik.Nazwisko}',"+
                                      $"premia='{pracownik.Premia}' WHERE idPracownika={idPracownika};";

                MySqlCommand command = new MySqlCommand(EDYTUJ_PRACOWNIKA, connection);
                MessageBox.Show(EDYTUJ_PRACOWNIKA);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        //MYSQL - usunięcie pracownika z bazy
        public static bool UsunPracownika(sbyte idPracownika)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command1 = new MySqlCommand($"{USUN_PRACOWNIKA} {idPracownika}", connection);
                MySqlCommand command2 = new MySqlCommand($"{USUN_SPRZEDAZE_PRACOWNIKA} {idPracownika}", connection);
                connection.Open();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

        #endregion

    }
}