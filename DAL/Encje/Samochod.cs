using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Encje
{
    public class Samochod
    {
        #region Pola
        public sbyte? Id { get; set; }
        public string Marka { get; set; }
        public string ModelPojazdu { get; set; }
        public string Silnik { get; set; }
        public string Kolor { get; set; }
        public string KrajProdukcji { get; set; }
        public string DataProdukcji { get; set; }
        public double Cena { get; set; }
        public int Moc { get; set; }
        #endregion


        #region Konstruktory

        //obiekt na podstawie MySQLDataReader
        public Samochod(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["idModelu"].ToString());
            Marka = reader["marka"].ToString();
            ModelPojazdu = reader["model"].ToString();
            Silnik = reader["silnik"].ToString();
            Kolor = reader["kolor"].ToString();
            KrajProdukcji = reader["krajProdukcji"].ToString();
            DataProdukcji = reader["dataProdukcji"].ToString();
            Cena = double.Parse(reader["cenaModelu"].ToString());
            Moc = int.Parse(reader["moc"].ToString());
        }

        //obiekt który nie istnieje w bazie - brak ID
        public Samochod(string marka, string modelPojazdu, string silnik, string kolor, 
                        string krajProdukcji, string dataProdukcji, double cena, int moc)
        {
            Id = null;
            Marka = marka.Trim();
            ModelPojazdu = modelPojazdu.Trim();
            Silnik = silnik.Trim();
            Kolor = kolor.Trim();
            KrajProdukcji = krajProdukcji.Trim();
            DataProdukcji = dataProdukcji.Trim();
            Cena = cena;
            Moc = moc;
        }

        //kopiowanie obiektu
        public Samochod(Samochod pracownik)
        {
            Id = pracownik.Id;
            Marka = pracownik.Marka;
            ModelPojazdu = pracownik.ModelPojazdu;
            Silnik = pracownik.Silnik;
            Kolor = pracownik.Kolor;
            KrajProdukcji = pracownik.KrajProdukcji;
            DataProdukcji = pracownik.DataProdukcji;
            Cena = pracownik.Cena;
            Moc = pracownik.Moc;
        }
        #endregion

        #region Metody
        //metoda generuje string dla INSERT
        public string ToInsert()
        {
            return $"('{Marka}', '{ModelPojazdu}', '{Silnik}', '{Moc}', '{Kolor}', '{KrajProdukcji}', '{DataProdukcji}', '{Cena}')";
        }
        #endregion
    }
}