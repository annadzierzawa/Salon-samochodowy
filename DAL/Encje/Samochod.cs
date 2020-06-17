using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Encje
{
    public class Samochod
    {
        #region Pola
        public sbyte? Id { get; set; }
        public string Nazwa { get; set; }
        public string Silnik { get; set; }
        public string Kolor { get; set; }
        public string KrajProdukcji { get; set; }
        public string DataProdukcji { get; set; }
        public double Cena { get; set; }
        #endregion


        #region Konstruktory

        //obiekt na podstawie MySQLDataReader
        public Samochod(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["idModelu"].ToString());
            Nazwa = reader["nazwa"].ToString();
            Silnik = reader["silnik"].ToString();
            Kolor = reader["kolor"].ToString();
            KrajProdukcji = reader["krajProdukcji"].ToString();
            DataProdukcji = reader["dataProdukcji"].ToString();
            Cena = double.Parse(reader["cenaModelu"].ToString());
        }

        //obiekt który nie istnieje w bazie - brak ID
        public Samochod(string nazwa, string silnik, string kolor, 
                        string krajProdukcji, string dataProdukcji, double cena)
        {
            Id = null;
            Nazwa = nazwa.Trim();
            Silnik = silnik.Trim();
            Kolor = kolor.Trim();
            KrajProdukcji = krajProdukcji.Trim();
            DataProdukcji = dataProdukcji.Trim();
            Cena = cena;
        }

        //kopiowanie obiektu
        public Samochod(Samochod pracownik)
        {
            Id = pracownik.Id;
            Nazwa = pracownik.Nazwa;
            Silnik = pracownik.Silnik;
            Kolor = pracownik.Kolor;
            KrajProdukcji = pracownik.KrajProdukcji;
            DataProdukcji = pracownik.DataProdukcji;
            Cena = pracownik.Cena;
        }

        #endregion
    }
}