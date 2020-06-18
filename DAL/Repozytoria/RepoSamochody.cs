using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Repozytoria
{
    using Encje;
    public class RepoSamochody
    {
        #region SQL_QUERIES
        private const string WSZYSTKIE_SAMOCHODY = "SELECT * FROM Samochody";
        private const string DODAJ_SAMOCHOD = "INSERT INTO `Samochody`(`nazwa`, `silnik`, `kolor`, `krajProdukcji`,`dataProdukcji`, `cenaModelu`) VALUES ";
        private const string USUN_SAMOCHOD = "DELETE FROM `Samochody` WHERE idModelu=";
        private const string USUN_SPRZEDAZE_SAMOCHODU = "DELETE FROM `Sprzedaz` WHERE idModelu=";
        #endregion

        #region CRUD


        public static List<Samochod> PobierzWszystkieSamochody()
        {
            List<Samochod> samochody = new List<Samochod>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_SAMOCHODY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    samochody.Add(new Samochod(reader));
                connection.Close();
            }

            return samochody;
        }

        public static bool DodajSamochodDoBazy(Samochod samochod)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_SAMOCHOD} {samochod.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                samochod.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }

            return stan;
        }

        public static bool UsunSamochod(sbyte idSamochodu)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command1 = new MySqlCommand($"{USUN_SAMOCHOD} {idSamochodu}", connection);
                MySqlCommand command2 = new MySqlCommand($"{USUN_SPRZEDAZE_SAMOCHODU} {idSamochodu}", connection);
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