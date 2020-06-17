using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Repozytoria
{
    using Encje;
    public class RepoSprzedazy
    {

        #region ZAPYTANIA
        private const string WSZYSTKIE_SPRZEDAZE = "SELECT * FROM Sprzedaz";
        private const string DODAJ_SPRZEDAZ = "INSERT INTO `Sprzedaz`(`idPracownika`, `idModelu`, `cena`) VALUES ";
        private const string USUN_SPRZEDAZ = "DELETE FROM `Sprzedaz` WHERE idSprzedazy=";

        #endregion

        public static List<Sprzedaz> PobierzWszystkieSprzedaze()
        {
            List<Sprzedaz> sprzedaze = new List<Sprzedaz>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_SPRZEDAZE, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    sprzedaze.Add(new Sprzedaz(reader));
                connection.Close();
            }
            return sprzedaze;
        }

        public static bool DodajSprzedazDoBazy(Sprzedaz sprzedaz)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_SPRZEDAZ} {sprzedaz.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                sprzedaz.IdSprzedazy = (sbyte)command.LastInsertedId;
                connection.Close();
            }

            return stan;
        }

        public static bool UsunSprzedaz(sbyte idSprzedazy)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand($"{USUN_SPRZEDAZ} {idSprzedazy}", connection);
                connection.Open();
                command.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

    }
}