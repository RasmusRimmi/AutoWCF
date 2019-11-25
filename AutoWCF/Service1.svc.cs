using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AutoWCF.model;
using System.Data;

namespace AutoWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DatabaseHallinta dbController = new DatabaseHallinta();

        public bool TestDatabaseConnection()
        {
            bool doesItWork = dbController.connectDatabase();
            dbController.disconnectDatabase();
            return doesItWork;
        }

        public bool saveAuto(Auto pirssi)
        {
            bool didItGoIntoDatabase = dbController.SaveCar(pirssi);
            return didItGoIntoDatabase;
        }

        public List<autonMerkit> getAllAutoMakers()
        {
            return dbController.getAllAutoMakersFromDatabase();
        }

        public List<autonMallit> getAutoModels(int merkkiId)
        {
            return dbController.getAutoModelsByMakerId(merkkiId);
        }

        public List<Varit> GetCarColors()
        {
            return dbController.GetAutoColors();
        }

        public List<Polttoaine> GetCarFuels()
        {
            return dbController.GetAutoFuels();
        }

        public Auto NextAuto(int ID)
        {

            if (ID == 0)
            {
                return dbController.EnsimmainenAuto(ID);
            }

            else
            {
                Auto NxtCar = dbController.NextCar(ID);

                if (NxtCar.Id == 0)
                {
                    return dbController.EnsimmainenAuto(ID);
                }

                return dbController.NextCar(ID);
            }

        }

        public Auto PrevAuto(int ID)
        {
            if (ID <= 1)
            {
                return dbController.ViimeinenAuto(ID);
            }

            else
            {
                Auto PrevCar = dbController.PreviousCar(ID);

                if (PrevCar.Id <= 1)
                {
                    return dbController.ViimeinenAuto(ID);
                }

                return PrevCar;
            }

        }
    }
}
