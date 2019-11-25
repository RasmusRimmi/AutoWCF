using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace AutoWCF.model
{
    public class DatabaseHallinta
    {
       
        SqlConnection dbYhteys = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Autokauppa;" +
        "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public DatabaseHallinta()
        {
           
        }

        public bool connectDatabase()
        {           
            try
            { 
                dbYhteys.Open();
                
                return true;
            }
            catch(Exception e)
            {
                
                Console.WriteLine("Virheilmoitukset:" + e);
                //dbYhteys.Close();
                return false;
            }          
        }

        public void disconnectDatabase()
        {
            dbYhteys.Close();
        }

        public bool saveAutoIntoDatabase(Auto newAuto)
        {
            bool palaute = false;
            return palaute;
        }

        // Lisää Automerkit listaan tietokannasta
        public List<autonMerkit> getAllAutoMakersFromDatabase()
        {
            dbYhteys.Open();
            SqlCommand query1 = new SqlCommand("SELECT * FROM  AutonMerkki", dbYhteys);
            SqlDataReader merkki = query1.ExecuteReader();
            List<autonMerkit> merkkilista = new List<autonMerkit>();

            while (merkki.Read())
            {
                autonMerkit merkit = new autonMerkit();
                merkit.Id = (int)merkki["ID"];
                merkit.MerkkiNimi = (string)merkki["Merkki"];
                merkkilista.Add(merkit);
            }
            dbYhteys.Close();
            return merkkilista;
        }

        // Lisää valitun automerkin mallit listaan merkkiId:n kautta tietokannasta
        public List<autonMallit> getAutoModelsByMakerId(int MerkkiId)           
        {
            dbYhteys.Open();
            SqlCommand query2 = new SqlCommand("SELECT * FROM AutonMallit WHERE AutonMerkkiID = " + MerkkiId + "", dbYhteys);
            SqlDataReader malli = query2.ExecuteReader();
            List<autonMallit> mallilista = new List<autonMallit>();

            while (malli.Read())
            {
                autonMallit mallit = new autonMallit();
                mallit.Id = (int)malli["ID"];
                mallit.MalliNimi = (string)malli["Auton_mallin_nimi"];
                mallit.MerkkiId = (int)malli["AutonMerkkiID"];
                mallilista.Add(mallit);
            }
            dbYhteys.Close();
            return mallilista;
        }

        // Lisää värit listaan tietokannasta
        public List<Varit> GetAutoColors()
        {
            dbYhteys.Open();
            SqlCommand query3 = new SqlCommand("SELECT * FROM  Varit", dbYhteys);
            SqlDataReader vari = query3.ExecuteReader();
            List<Varit> varilista = new List<Varit>();

            while (vari.Read())
            {
                Varit varit = new Varit();
                varit.Id = (int)vari["ID"];
                varit.Varin_nimi = (string)vari["Varin_nimi"];
                varilista.Add(varit);
            }
            dbYhteys.Close();
            return varilista;
        }

        // Lisää polttoaineet listaan tietokannasta
        public List<Polttoaine> GetAutoFuels()
        {
            dbYhteys.Open();
            SqlCommand query4 = new SqlCommand("SELECT * FROM  Polttoaine", dbYhteys);
            SqlDataReader polttoaine = query4.ExecuteReader();
            List<Polttoaine> polttoainelista = new List<Polttoaine>();

            while (polttoaine.Read())
            {
                Polttoaine polttoaineet = new Polttoaine();
                polttoaineet.Id = (int)polttoaine["ID"];
                polttoaineet.Polttoaineen_nimi = (string)polttoaine["Polttoaineen_nimi"];
                polttoainelista.Add(polttoaineet);
            }
            dbYhteys.Close();
            return polttoainelista;
        }
        
        // Tallenna auto tietokantaan
        public bool SaveCar(Auto pirssi)
        {
            dbYhteys.Open();
            SqlCommand query5 = new SqlCommand("INSERT INTO Auto(Hinta, Rekisteri_paivamaara, Moottorin_tilavuus, Mittarilukema, AutonMerkkiID, AutonMalliID, VaritID, PolttoaineID)"+
            "VALUES(@Hinta, @Rekisteri_pvm, @Moottorin_Tilavuus,@Mittarilukema,@AutonMerkkiID,@AutonMalliID,@VaritID,@PolttoaineID)", dbYhteys);
            SqlParameter Hinta = new SqlParameter("@Hinta", pirssi.Hinta);
            query5.Parameters.Add(Hinta);
            SqlParameter Rekisteri_paivamaara = new SqlParameter("@Rekisteri_pvm", pirssi.Rekisteri_paivamaara);
            query5.Parameters.Add(Rekisteri_paivamaara);
            SqlParameter Moottorin_tilavuus = new SqlParameter("@Moottorin_tilavuus", pirssi.Moottorin_Tilavuus);
            query5.Parameters.Add(Moottorin_tilavuus);
            SqlParameter Mittarilukema = new SqlParameter("@Mittarilukema", pirssi.Mittarilukema);
            query5.Parameters.Add(Mittarilukema);
            SqlParameter AutonMerkkiId = new SqlParameter("@AutonMerkkiID", pirssi.AutonMerkkiID);
            query5.Parameters.Add(AutonMerkkiId);
            SqlParameter AutonMalliId = new SqlParameter("@AutonMalliID", pirssi.AutonMalliID);
            query5.Parameters.Add(AutonMalliId);
            SqlParameter VaritID = new SqlParameter("@VaritID", pirssi.VaritID);
            query5.Parameters.Add(VaritID);
            SqlParameter PolttoaineID = new SqlParameter("@PolttoaineID", pirssi.PolttoaineID);
            query5.Parameters.Add(PolttoaineID);


            query5.ExecuteNonQuery();

            dbYhteys.Close();
            return true;
        }

        //Näytä Seuraava auto
        public Auto NextCar(int ID)
        {
            Auto biili = new Auto();
            dbYhteys.Open();
            SqlCommand query6 = new SqlCommand("SELECT TOP 1 * FROM auto WHERE ID > "+ID+" ORDER BY ID ASC", dbYhteys);

            SqlDataReader seurAuto = query6.ExecuteReader();

            while (seurAuto.Read())
            {
                biili.Id = (int)seurAuto["ID"];
                biili.Hinta = (decimal)seurAuto["Hinta"];
                biili.Rekisteri_paivamaara = (DateTime)seurAuto["Rekisteri_paivamaara"];
                biili.Moottorin_Tilavuus = (decimal)seurAuto["Moottorin_tilavuus"];
                biili.Mittarilukema = (int)seurAuto["Mittarilukema"];
                biili.AutonMerkkiID = (int)seurAuto["AutonMerkkiID"];
                biili.AutonMalliID = (int)seurAuto["AutonMalliID"];
                biili.VaritID = (int)seurAuto["VaritID"];
                biili.PolttoaineID = (int)seurAuto["PolttoaineID"];
            }

            dbYhteys.Close();
            return biili;
        }

        // Näytä edellinen auto
        public Auto PreviousCar(int ID)
        {
            Auto biili = new Auto();
            dbYhteys.Open();
            SqlCommand query7 = new SqlCommand("SELECT TOP 1 * FROM auto WHERE ID < " +ID+ " ORDER BY ID DESC", dbYhteys);

            SqlDataReader prevAuto = query7.ExecuteReader();

            while (prevAuto.Read())
            {
                biili.Id = (int)prevAuto["ID"];
                biili.Hinta = (decimal)prevAuto["Hinta"];
                biili.Rekisteri_paivamaara = (DateTime)prevAuto["Rekisteri_paivamaara"];
                biili.Moottorin_Tilavuus = (decimal)prevAuto["Moottorin_tilavuus"];
                biili.Mittarilukema = (int)prevAuto["Mittarilukema"];
                biili.AutonMerkkiID = (int)prevAuto["AutonMerkkiID"];
                biili.AutonMalliID = (int)prevAuto["AutonMalliID"];
                biili.VaritID = (int)prevAuto["VaritID"];
                biili.PolttoaineID = (int)prevAuto["PolttoaineID"];
            }

            dbYhteys.Close();
            return biili;
        }

        // Näytä Viimeinen auto
        public Auto ViimeinenAuto(int ID)
        {
            Auto biili = new Auto();
            dbYhteys.Open();
            SqlCommand query8 = new SqlCommand("SELECT TOP 1 * FROM auto ORDER BY ID DESC", dbYhteys);

            SqlDataReader viimeinenAuto = query8.ExecuteReader();

            while (viimeinenAuto.Read())
            {
                biili.Id = (int)viimeinenAuto["ID"];
                biili.Hinta = (decimal)viimeinenAuto["Hinta"];
                biili.Rekisteri_paivamaara = (DateTime)viimeinenAuto["Rekisteri_paivamaara"];
                biili.Moottorin_Tilavuus = (decimal)viimeinenAuto["Moottorin_tilavuus"];
                biili.Mittarilukema = (int)viimeinenAuto["Mittarilukema"];
                biili.AutonMerkkiID = (int)viimeinenAuto["AutonMerkkiID"];
                biili.AutonMalliID = (int)viimeinenAuto["AutonMalliID"];
                biili.VaritID = (int)viimeinenAuto["VaritID"];
                biili.PolttoaineID = (int)viimeinenAuto["PolttoaineID"];
            }

            dbYhteys.Close();
            return biili;

            

        }

        // Näytä Ensimmäinen auto
        public Auto EnsimmainenAuto(int ID)
        {
            Auto biili = new Auto();
            dbYhteys.Open();
            SqlCommand query9 = new SqlCommand("SELECT TOP 1 * FROM auto ORDER BY ID ASC", dbYhteys);

            SqlDataReader ensimmainenAuto = query9.ExecuteReader();

            while (ensimmainenAuto.Read())
            {
                biili.Id = (int)ensimmainenAuto["ID"];
                biili.Hinta = (decimal)ensimmainenAuto["Hinta"];
                biili.Rekisteri_paivamaara = (DateTime)ensimmainenAuto["Rekisteri_paivamaara"];
                biili.Moottorin_Tilavuus = (decimal)ensimmainenAuto["Moottorin_tilavuus"];
                biili.Mittarilukema = (int)ensimmainenAuto["Mittarilukema"];
                biili.AutonMerkkiID = (int)ensimmainenAuto["AutonMerkkiID"];
                biili.AutonMalliID = (int)ensimmainenAuto["AutonMalliID"];
                biili.VaritID = (int)ensimmainenAuto["VaritID"];
                biili.PolttoaineID = (int)ensimmainenAuto["PolttoaineID"];
            }

            dbYhteys.Close();
            return biili;
        }

    }
}
