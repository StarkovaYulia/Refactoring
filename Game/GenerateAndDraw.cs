using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using ClassMap;
using Plant;
using Animals;
using Predators;
using Herbivores;
using Omnivores;
using Humans;
using Inventories;
using EverySquareDescription;
using Fields;
using Resources;
using Homes;

namespace GeneratingAndDrawing
{
    public class GenerateAndDraw
    {
        public List<Animal> animalsObjects = new List<Animal>();
        public List<Plants> plantsObjects = new List<Plants>();
        public SquareDescription[,] descriptionSquares;
        private Random rnd;
        private Map map;
        private int numberAnimals;
        private int numberPlants;
        private int coordX;
        private int coordY;
        private int typeAnimal;
        private int genderAnimal;
        public GenerateAndDraw(int animals, int plants, Map map)
        {
            rnd = new Random();
            numberAnimals = animals;
            numberPlants = plants;
            this.map = map;
        }

        public int[] GenerateRandomNumbers()
        {
            coordX = rnd.Next(0, 1000);
            coordY = rnd.Next(0, 1000);
            if (descriptionSquares[coordX, coordY].isEmpty == true)
            {
                descriptionSquares[coordX, coordY].isEmpty = false;
            }
            int[] arr = new int[] { coordX, coordY };
            return arr;
        }

        public int[] GenerateRandomNumbersForPlants()
        {
            coordX = rnd.Next(0, 1000);
            coordY = rnd.Next(0, 1000);
            while (descriptionSquares[coordX, coordY].IsTherePlant == true)
            {
                coordX = rnd.Next(0, 1000);
                coordY = rnd.Next(0, 1000);
            }
            descriptionSquares[coordX, coordY].IsTherePlant = true;
            if (descriptionSquares[coordX, coordY].isEmpty == true)
            {
                descriptionSquares[coordX, coordY].isEmpty = false;
            }
            int[] arr = new int[] { coordX, coordY };
            return arr;
        }
        private int GenerateTypeAnimal()
        {
            return rnd.Next(0, 3);
        }
        private void GenerateAnimals(int anim, GenerateAndDraw locality)
        {
            for (int i = 0; i < anim; i++)
            {
                typeAnimal = GenerateTypeAnimal();
                genderAnimal = rnd.Next(0, 2);
                switch (typeAnimal)
                {
                    case 0:
                        Herbivor animalHerbivor = new Herbivor(map, locality, genderAnimal);
                        animalHerbivor.SetAnimal();
                        this.DrawAnimal(animalHerbivor.coordAnimalX, animalHerbivor.coordAnimalY, animalHerbivor.color);
                        break;
                    case 1:
                        Omnivor animalOmnivor = new Omnivor(map, locality, genderAnimal);
                        this.DrawAnimal(animalOmnivor.coordAnimalX, animalOmnivor.coordAnimalY, animalOmnivor.color);
                        animalOmnivor.SetAnimal();
                        break;
                    case 2:
                        Predator animalPredator = new Predator(map, locality, genderAnimal);
                        this.DrawAnimal(animalPredator.coordAnimalX, animalPredator.coordAnimalY, animalPredator.color);
                        animalPredator.SetAnimal();
                        break;
                }
            }
        }

        public void DrawAnimal(int x, int y, Color color)
        {
            int colorAnimal = rnd.Next(0, 8);
            Color colorOfSquare = new Color();

            switch (colorAnimal)
            {
                case 0:
                    colorOfSquare = Color.FromArgb(28, 28, 28);
                    break;

                case 1:
                    colorOfSquare = Color.FromArgb(250, 208, 218);
                    break;
                case 2:
                    colorOfSquare = Color.FromArgb(15, 190, 98);
                    break;
                case 3:
                    colorOfSquare = Color.FromArgb(34, 98, 45);
                    break;
                case 4:
                    colorOfSquare = Color.FromArgb(8, 2, 243);
                    break;
                case 5:
                    colorOfSquare = Color.FromArgb(218, 250, 78);
                    break;
                case 6:
                    colorOfSquare = Color.FromArgb(48, 128, 178);
                    break;
                case 7:
                    colorOfSquare = Color.FromArgb(175, 57, 65);
                    break;
                case 8:
                    colorOfSquare = Color.FromArgb(25, 127, 43);
                    break;
            }

            map.Location.SetPixel(x, y, colorOfSquare);
            map.SetMap();

        }

        public void DrawPixel(int x, int y)
        {

            Color colorOfSquare = new Color();
            if (descriptionSquares[x, y].predator.Count != 0)
            {
                colorOfSquare = Color.FromArgb(20, 25, 10);
            }
            else if (descriptionSquares[x, y].omnivor.Count != 0)
            {
                colorOfSquare = Color.FromArgb(225, 155, 200);
            }
            else if (descriptionSquares[x, y].herbivor.Count != 0)
            {
                colorOfSquare = Color.FromArgb(145, 45, 40);
            }
            else if (descriptionSquares[x, y].plant.Count != 0 != false)
            {
                colorOfSquare = descriptionSquares[x, y].plant[0].color;
            }
            else
            {
                colorOfSquare = Color.FromArgb(255, 255, 0);

            }

            map.Location.SetPixel(x, y, colorOfSquare);
            map.SetMap();
        }
        private void GeneratePlants(int plants, GenerateAndDraw locality)
        {
            Plants plant;
            for (int i = 0; i < plants; i++)
            {
                plant = new Plants(locality, map);
                plant.CreatePlant();
                plantsObjects.Add(plant);
                descriptionSquares[plant.coordPlantX, plant.coordPlantY].plant.Add(plant);
            }
        }

        public void DrawArea(int x, int y)
        {
            Color colorOfSquare = new Color();
            colorOfSquare = Color.FromArgb(255, 255, 0);

            map.Location.SetPixel(x, y, colorOfSquare);
            map.SetMap();

        }

        private void GenerateHuman(int humans, GenerateAndDraw locality, Inventory inventory)
        {
            Human human;
            genderAnimal = rnd.Next(0, 2);
            for (int i = 0; i < humans; i++)
            {
                human = new Human(map, locality, inventory, genderAnimal);
                human.CreateHuman();
            }
        }
        private Inventory GenerateInventory()
        {
            Inventory inventory = new Inventory();
            return inventory;
        }

        private void GenerateFields(int number, GenerateAndDraw locality)
        {
            Field newfield;
            int[] coordinations = new int[2] { -1, -1 };
            for (int i = 0; i < number; i++)
            {
                coordinations = GenerateRandomNumbers();
                newfield = new Field(coordinations[0], coordinations[1], locality);
                newfield.CreateField();
                descriptionSquares[coordinations[0], coordinations[1]].field.Add(newfield);
            }
        }

        private void DeleteAnimals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Color colorOfSquare = new Color();

                colorOfSquare = Color.FromArgb(255, 255, 0);

                coordX = rnd.Next(0, 1000);
                coordY = rnd.Next(0, 1000);

                map.Location.SetPixel(coordX, coordY, colorOfSquare);
            }
            map.SetMap();
        }

        public void GenerateStart(GenerateAndDraw locality) // старт генерации
        {
            descriptionSquares = new SquareDescription[1000, 1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    SquareDescription oneSquare = new SquareDescription(j, i, Color.FromArgb(255, 255, 0), true);
                    descriptionSquares[j, i] = oneSquare;
                }
            }
            GeneratePlants(numberPlants, locality);
            GenerateAnimals(numberAnimals, locality);
            Inventory inventory = GenerateInventory();
            DeleteAnimals(400000);
            inventory.FillStartDictionary();
            GenerateHuman(100, locality, inventory);
            GenerateFields(50, locality);
        }

        private void DrawWinterPixel(int x, int y)
        {
            Color colorOfSquare = Color.FromArgb(220, 220, 220);
            map.Location.SetPixel(x, y, colorOfSquare);
            map.SetMap();
        }
        public void MoveOnWinterLocation()
        {
            for (int i = 0; i <= 999; i++)
            {
                for (int j = 0; j <= 999; j++)
                {
                    if (descriptionSquares[j, i].isEmpty == true)
                    {
                        DrawWinterPixel(j, i);
                    }

                }
            }
        }
        public void MoveOnLocation()
        {
            for (int i = 0; i <= 999; i++)
            {
                for (int j = 0; j <= 999; j++)
                {
                    DrawPixel(j, i);
                }
            }
            map.SetMap();
        }

        public List<int> FindSquare(int i, int j)
        {
            string[] temporaryArray;
            string[] directionsArray = new string[] { "up", "right", "down", "left" };
            if (i == 999 && j == 0)
            {
                temporaryArray = new string[] { directionsArray[2], directionsArray[3] };
            }
            else if (i == 0 && j == 0)
            {
                temporaryArray = new string[] { directionsArray[1], directionsArray[2] };
            }
            else if (i == 0 && j == 999)
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[1] };
            }
            else if (i == 999 && j == 999)
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[3] };
            }
            else if (i == 0)
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[1], directionsArray[2] };
            }
            else if (j == 0)
            {
                temporaryArray = new string[] { directionsArray[1], directionsArray[2], directionsArray[3] };
            }
            else if (i == 999)
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[2], directionsArray[3] };
            }
            else if (j == 999)
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[1], directionsArray[3] };
            }
            else
            {
                temporaryArray = new string[] { directionsArray[0], directionsArray[1], directionsArray[2], directionsArray[3] };
            }
            List<int> coordinates = new List<int> { -1, -1 };
            string direction = temporaryArray[rnd.Next(0, 4)];

            if (direction == "up")
            {
                coordinates[0] = coordX;
                coordinates[1] = coordY - 1;
            }
            else if (direction == "right")
            {
                coordinates[0] = coordX + 1;
                coordinates[1] = coordY;
            }
            else if (direction == "down")
            {
                coordinates[0] = coordX;
                coordinates[1] = coordY + 1;
            }
            else
            {
                coordinates[0] = coordX - 1;
                coordinates[1] = coordY;
            }
            return coordinates;
        }
    }
}
