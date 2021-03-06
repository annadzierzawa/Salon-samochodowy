﻿using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Repozytoria
{
    using Encje;
    public class RepoSamochody
    {

        #region SQL_QUERIES
        private const string WSZYSTKIE_SAMOCHODY = "SELECT * FROM Samochody";
        private const string DODAJ_SAMOCHOD = "INSERT INTO `Samochody`(`marka`, `model`, `silnik`, `moc`, `kolor`, `krajProdukcji`,`dataProdukcji`, `cenaModelu`) VALUES ";
        private const string USUN_SAMOCHOD = "DELETE FROM `Samochody` WHERE idModelu=";
        private const string USUN_SPRZEDAZE_SAMOCHODU = "DELETE FROM `Sprzedaz` WHERE idModelu=";
        #endregion

        #region CRUD

        //MYSQL - Pobranie wszystkich samochodów z bazy
        public static List<Samochod> PobierzWszystkieSamochody()
        {
            List<Samochod> samochody = new List<Samochod>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_SAMOCHODY, connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                    samochody.Add(new Samochod(reader));
                connection.Close();
            }

            return samochody;
        }

        //MYSQL - dodanie samochodu do bazy
        public static bool DodajSamochodDoBazy(Samochod samochod)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_SAMOCHOD} {samochod.ToInsert()}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                var id = command.ExecuteNonQuery();
                stan = true;
                samochod.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }

            return stan;
        }

        //MYSQL - Edytowanie samochodu w bazie
        public static bool EdytujSamochod(Samochod samochod, sbyte idSamochodu)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ_SAMOCHOD = $"UPDATE Samochody SET marka='{samochod.Marka}', model='{samochod.ModelPojazdu}', " +
                                           $"silnik='{samochod.Silnik}', moc='{samochod.Moc}'," +
                                           $"kolor='{samochod.Kolor}', krajProdukcji='{samochod.KrajProdukcji}'," +
                                           $"dataProdukcji='{samochod.DataProdukcji}', cenaModelu='{samochod.Cena}'" +
                                           $"WHERE idModelu={idSamochodu};";

                MySqlCommand command = new MySqlCommand(EDYTUJ_SAMOCHOD, connection);
                MessageBox.Show(EDYTUJ_SAMOCHOD);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        //MYSQL - Usunięcie samochodu z bazy
        public static bool UsunSamochod(sbyte idSamochodu)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command1 = new MySqlCommand($"{USUN_SAMOCHOD} {idSamochodu}", connection);
                MySqlCommand command2 = new MySqlCommand($"{USUN_SPRZEDAZE_SAMOCHODU} {idSamochodu}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Błąd połączenia z baza MYSQL!"); Application.Current.Shutdown(); }
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