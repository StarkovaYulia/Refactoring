using System;
using System.Collections.Generic;
using ClassMap;
using GeneratingAndDrawing;
using System.Drawing;
using EverySquareDescription;

namespace Plant
{
    public class Plants
    {
        enum TypesPlants
        {
            eatable_fruit_poisoned = 2,
            eatable_fruit_healthy,
            eatable_nofruit_poisoned,
            eatable_nofruit_healthy,
            noeatable_fruit,
            noeatable_nofruit
        }

        public int coordPlantX;
        public int coordPlantY;
        public Color color;
        public bool isEatable = false;
        public bool isPoisoned;

        private Random rnd;
        private GenerateAndDraw locality;
        private Map map;
        private string stepOfPlantrowth;
        private int typePlant;
        private string[] namesOfPlantGrowth = new string[] { "seeds", "sprout", "plant", "diedPlant" };
        private Color colorPlods = Color.FromArgb(252, 10, 10);
        private Color[] colorsPlants = new Color[] { Color.FromArgb(0, 191, 255), Color.FromArgb(0, 128, 0), Color.FromArgb(202, 44, 146),
                                               Color.FromArgb(65, 105, 225), Color.FromArgb(65, 74, 76), Color.FromArgb(117, 51, 19)};

        private int[] temporaryArray = new int[] { 2, 3, 4, 5, 6, 7 };

        public Plants() { }

        public Plants(bool isEatable, int coordPlantX, int coordPlantY, string stepOfPlantrowth,
                      int typePlant, GenerateAndDraw locality, Map map, Color color, bool isPoisoned)
        {
            this.isEatable = false;
            this.coordPlantX = coordPlantX;
            this.coordPlantY = coordPlantY;
            this.stepOfPlantrowth = stepOfPlantrowth;
            this.typePlant = typePlant;
            this.map = map;
            this.locality = locality;
            this.color = color;
            this.isPoisoned = isPoisoned;
        }
        public Plants(GenerateAndDraw locality, Map map)
        {
            this.locality = locality;
            this.map = map;
        }

        public void CreatePlant()
        {
            typePlant = GenerateTypePlant();
            int[] array = locality.GenerateRandomNumbers();
            coordPlantX = array[0];
            coordPlantY = array[1];
            isEatable = false;
            color = colorsPlants[typePlant - 2];
            if (typePlant == 2 || typePlant == 4)
            {
                isPoisoned = true;
            }
            else
            {
                isPoisoned = false;
            }
            locality.descriptionSquares[coordPlantX, coordPlantY].isEmpty = false;
            locality.descriptionSquares[coordPlantX, coordPlantY].IsTherePlant = true;
        }

        public void PlantWinterLifeStep()
        {
            if (stepOfPlantrowth != "seeds" && (typePlant == 2 || typePlant == 5 || typePlant == 7))
            {
                ClearFromPlant();
            }
            else
            {
                if (typePlant != 3)   // 3 тип не плодоносит зимой
                {// росток зимой должен стать взрослым
                    PlantLifeStep();
                }
            }
        }
        private int GenerateTypePlant() // cоздать новый тип растения
        {
            rnd = new Random();
            return temporaryArray[rnd.Next(2, temporaryArray.Length + 2) - 2];
        }
        private void GiveSeedsOrPlods() // выбор между плодами и семенами
        {
            List<int> ChildSquare = new List<int> { -1, -1 };
            ChildSquare = locality.FindSquare(coordPlantX, coordPlantY);
            Plants onePlant;
            if (ChildSquare[0] != -1 && locality.descriptionSquares[ChildSquare[0], ChildSquare[1]].IsTherePlant == false)
            {
                Color colorChild = Color.Yellow;
                locality.descriptionSquares[ChildSquare[0], ChildSquare[1]].isEmpty = false;
                locality.descriptionSquares[ChildSquare[0], ChildSquare[1]].IsTherePlant = true;
                if (typePlant == 4 || typePlant == 5 || typePlant == 7)
                {
                    onePlant = new Plants(false, ChildSquare[0], ChildSquare[1], "seeds", typePlant, locality, map, color, isPoisoned);
                }
                else
                {
                    string[] typesOfChildren = new string[] { "seeds", "young plod" };
                    string type = typesOfChildren[rnd.Next(0, 2)];
                    if (type == "seeds")
                    {
                        colorChild = color;
                    }
                    else
                    {
                        colorChild = colorPlods;
                    }
                    onePlant = new Plants(false, ChildSquare[0], ChildSquare[1], type, typePlant, locality, map, colorChild, isPoisoned);
                }
                locality.plantsObjects.Add(onePlant);
                locality.descriptionSquares[ChildSquare[0], ChildSquare[1]].plant.Add(onePlant);
            }
        }

        public void PlantLifeStep()
        {
            if (stepOfPlantrowth == "plant")
            {
                GiveSeedsOrPlods();
                stepOfPlantrowth = "diedPlant";
            }

            else if (stepOfPlantrowth == "seeds")
            {
                stepOfPlantrowth = "sprout";
                if (typePlant == 6 || typePlant == 7)
                {
                    isEatable = false;
                }
                else isEatable = true;
            }

            else if (stepOfPlantrowth == "sprout")
            {
                stepOfPlantrowth = "plant";
            }

            else if (stepOfPlantrowth == "young plod")
            {
                isEatable = true;
                stepOfPlantrowth = "pine plod";
            }

            else if (stepOfPlantrowth == "diedPlant")
            {
                ClearFromPlant();
            }
        }

        private void ClearFromPlant()
        {
            if (locality.descriptionSquares[coordPlantX, coordPlantY].IsThereHerbivor != true && locality.descriptionSquares[coordPlantX, coordPlantY].IsThereOmnivor != true
                && locality.descriptionSquares[coordPlantX, coordPlantY].IsTherePredator != true)
            {
                locality.descriptionSquares[coordPlantX, coordPlantY].isEmpty = true;
            }
            locality.descriptionSquares[coordPlantX, coordPlantY].IsTherePlant = false;
            locality.descriptionSquares[coordPlantX, coordPlantY].plant.Remove(this);
        }
    }
}
