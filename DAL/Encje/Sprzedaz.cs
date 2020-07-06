using MySql.Data.MySqlClient;

namespace Salon_samochodowy.DAL.Encje
{
    public class Sprzedaz
    {
        #region Pola
        public sbyte? IdSprzedazy { get; set; }
        public sbyte IdPracownika { get; set; }
        public sbyte IdSamochodu { get; set; }
        public double Cena { get; set; }
        #endregion

        #region Kontruktory

        //tworzymy obiekt na podstawie MySQLDataReader
        public Sprzedaz(MySqlDataReader reader)
        {
            IdSprzedazy = sbyte.Parse(reader["idSprzedazy"].ToString());
            IdPracownika = sbyte.Parse(reader["idPracownika"].ToString());
            IdSamochodu = sbyte.Parse(reader["idModelu"].ToString());
            Cena = double.Parse(reader["cena"].ToString());
        }

        //tworzymy obiekt który jeszcze nie istnieje w bazie - brak ID
        public Sprzedaz(sbyte idPracownika, sbyte idSamochodu, double cena)
        {
            IdSprzedazy = null;
            IdPracownika = idPracownika;
            IdSamochodu = idSamochodu;
            Cena = cena;
        }
        #endregion

        #region Metody

        //generowanie stringa dla INSERT QUERY
        public string ToInsert()
        {
            return $"('{IdPracownika}', '{IdSamochodu}', '{Cena}')";
        }

        #endregion
    }
}