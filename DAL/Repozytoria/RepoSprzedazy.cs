using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using Renci.SshNet.Messages;

namespace Salon_samochodowy.DAL.Repozytoria
{
    using Encje;
    public class RepoSprzedazy
    {

        #region SQL_QUERIES
        private const string WSZYSTKIE_SPRZEDAZE = "SELECT * FROM Sprzedaz";
        private const string DODAJ_SPRZEDAZ = "INSERT INTO `Sprzedaz`(`idPracownika`, `idModelu`, `cena`) VALUES ";
        private const string USUN_SPRZEDAZ = "DELETE FROM `Sprzedaz` WHERE idSprzedazy=";

        #endregion

        #region CRUD

        //MYSQL - Pobranie wszystkich sprzedaży z bazy
        public static List<Sprzedaz> PobierzWszystkieSprzedaze()
        {
            List<Sprzedaz> sprzedaze = new List<Sprzedaz>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_SPRZEDAZE, connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                    sprzedaze.Add(new Sprzedaz(reader));
                connection.Close();
            }
            return sprzedaze;
        }

        //MYSQL - Dodanie sprzedaży do bazy
        public static bool DodajSprzedazDoBazy(Sprzedaz sprzedaz)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_SPRZEDAZ} {sprzedaz.ToInsert()}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                var id = command.ExecuteNonQuery();
                stan = true;
                sprzedaz.IdSprzedazy = (sbyte)command.LastInsertedId;
                connection.Close();
            }

            return stan;
        }

        //MYSQL - Usunięcie sprzedaży z bazy [brak implementacji]
        public static bool UsunSprzedaz(sbyte idSprzedazy)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand($"{USUN_SPRZEDAZ} {idSprzedazy}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                command.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

        #endregion
    }
}