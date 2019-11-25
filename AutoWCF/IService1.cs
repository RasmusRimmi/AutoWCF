using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace AutoWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        bool TestDatabaseConnection();

        [OperationContract]
        bool saveAuto(Auto pirssi);

        [OperationContract]
        List<autonMerkit> getAllAutoMakers();

        [OperationContract]
        List<autonMallit> getAutoModels(int merkkiId);
        
        [OperationContract]
        List<Varit> GetCarColors();

        [OperationContract]
        List<Polttoaine> GetCarFuels();

        [OperationContract]
        Auto NextAuto(int ID);

        [OperationContract]
        Auto PrevAuto(int ID);


        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Auto
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public decimal Hinta { get; set; }


        [DataMember]
        public DateTime Rekisteri_paivamaara { get; set; }


        [DataMember]
        public decimal Moottorin_Tilavuus { get; set; }


        [DataMember]
        public int Mittarilukema { get; set; }


        [DataMember]
        public int AutonMerkkiID { get; set; }


        [DataMember]
        public int AutonMalliID { get; set; }


        [DataMember]
        public int VaritID { get; set; }


        [DataMember]
        public int PolttoaineID { get; set; }

    }
    public class autonMerkit
    {
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string MerkkiNimi { get; set; }


    }
    public class autonMallit

   
    {
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string MalliNimi { get; set; }


        [DataMember]
        public int MerkkiId { get; set; }


    }
    public class Polttoaine
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Polttoaineen_nimi { get; set; }

    }
    public class Varit
    {
        [DataMember] 
        public int Id { get; set; }


        [DataMember]
        public string Varin_nimi { get; set; }

    }
}
