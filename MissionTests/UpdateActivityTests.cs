using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PineApple;

namespace MissionTests
{
    [TestClass]
    public class UpdateActivityTests
    {
        [TestMethod]//On vérifie que la bonne activité se met à jour, en gardant son numéro de référence
        public void UpdateActivityInMission()
        {
            // Création d'une mission
            Mission MTest = new Mission("Voyage Spatial", 200);
            List<int> lastro = new List<int> { 1, 3 };
            //Ajout d'activités dans la mission, chaque jour reçoit l'activité Sleeping de minuit à 7h à partir de la 2e semaine
            for (int i = 0; i < 10; i++)
            {
                MTest.newActivity("Nuit n°" + i,
                                   0,
                                   1,
                                   0,
                                   lastro,
                                   false,
                                   false,
                                   false,
                                   new MDate(i + 14, 0, 0),
                                   new MDate(i + 14, 7, 0)
                                 );
            }
            // On récupère l'index et le numéro de référence de l'activité que l'on veut modifier ici
            Activity a = MTest.getActivities().Find(x => x.getDescription() == "Nuit n°2");
            int numref = a.getNumber();
            List<int> lastroUpdate = new List<int> { 0, 2, 3 };
            //Activité remplaçant Sleeping : Exploration de la zone 51 le jour 16 de minuit à 5h30
            Activity updated = new Activity("Mission d'exploration près de la zone 51",
                                            1,
                                            0,
                                            51,
                                            lastroUpdate,
                                            true,
                                            false,
                                            true,
                                            new MDate(16, 0, 0),
                                            new MDate(16, 5, 30));
            //MAJ
            MTest.updateActivity(numref, updated);
            // ON teste tous les champs de l'activité pour voir s'ils ont bien changé (sauf pour le refNumber)
            bool ans = a.getNumber() == numref;
            bool ans2 = a.getDescription() == "Mission d'exploration près de la zone 51";
            bool ans3 = a.getIndexOfGenericType() == 1;
            bool ans4 = a.getIndexOfType() == 0;
            bool ans5 = a.getLocation() == 51;
            bool ans6 = a.getAstronautes() == lastroUpdate;
            bool ans7 = a.getExternBool();
            bool ans8 = !a.getSpaceVehicule();
            bool ans9 = a.getScaph();
            bool ans10 = a.getStartDate().getDay() == 16 && a.getStartDate().getHours() == 0 && a.getStartDate().getMinutes() == 0;
            bool ans11 = a.getEndDate().getDay() == 16 && a.getEndDate().getHours() == 5 && a.getEndDate().getMinutes() == 30;
            Assert.IsTrue(ans && ans2 && ans3 && ans4 && ans5 && ans6 && ans7 && ans8 && ans9 && ans10 && ans11, "Echec de la mise à jour de l'activité");
        }
    }
}
