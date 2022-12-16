using System;
using System.Collections.Generic;
using ClassMap;
using Animals;
using Plant;
using System.Drawing;
using GeneratingAndDrawing;
using EverySquareDescription;
using System.Linq;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace StartSimulation
{
    public class StartSimulationStep
    {

        private List<Animal> animalsObjects;
        private List<Plants> plantsObjects;
        private List<Animal> animalsObjectsCopy;
        private List<Plants> plantsObjectsCopy;
        private GenerateAndDraw locality;
        private Map map;
        public string season;
        private int seasonChecker = 0;

        public StartSimulationStep(GenerateAndDraw Locality, Map MapFirst)
        {
            locality = Locality;
            map = MapFirst;
            plantsObjects = Locality.plantsObjects;
            animalsObjects = Locality.animalsObjects;
        }

        public void Tm_Tick()
        {
            seasonChecker += 1;
            if (seasonChecker <= 5)
            {
                season = "summer";
            }
            else
            {
                season = "winter";
                if (seasonChecker == 10)
                {
                    seasonChecker = 0;
                }
            }

            animalsObjectsCopy = locality.animalsObjects.ToList();
            plantsObjectsCopy = locality.plantsObjects.ToList();
            foreach (Animal animal in animalsObjectsCopy)
            {
                if (season == "summer")
                {
                    animal.AnimalStandartAction(5);
                }
                else
                {
                    animal.AnimalWinterAction();
                }
               
            }

            foreach (Plants plant in plantsObjectsCopy)
            {
                if (season == "summer")
                {
                    plant.PlantLifeStep();
                }
                else
                {
                    plant.PlantWinterLifeStep();
                }
            }

            locality.GenerateStart(locality);
            
        }
    }
}
