using System;
using System.Collections.Generic;
using ClassMap;
using GeneratingAndDrawing;
using System.Drawing;
using Predators;
using Omnivores;
using Herbivores;
using StartSimulation;
using Humans;
using Inventories;
using Homes;

namespace Animals
{
    public abstract class Animal
    {
        protected Random rnd = new Random();
        protected GenerateAndDraw locality;
        protected Map map;
        protected int age;
        public int coordAnimalX;
        public int coordAnimalY;
        protected int numberOfAnimalName;
        protected int timeAfterReproduction;
        protected int health;
        protected int constHealth;
        public int satietyLevel;
        public List<Animal> tamedAnimals = new List<Animal>();
        public int lostSatietyLevel = 5;
        public bool IsTamed;
        public string thingForTaming= "";
        public bool CanBeTamed;
        public bool IsHibernation;
        public Color color;
        public string animalName;
        public string typeAnimal;
        public bool HasHumanPair;
        public int gender;          // 1 - мужчина, 0 - женщина

        public Human owner = null;
        public Animal(Map map, GenerateAndDraw locality, int genderAnimal)
        {
            this.map = map;
            this.locality = locality;
            gender = genderAnimal;
        }
        public Animal(Map map, GenerateAndDraw locality, int coordAnimalX, int coordAnimalY,
                       string typeAnimal, int genderAnimal)
        {
            this.map = map;
            this.locality = locality;
            this.coordAnimalX = coordAnimalX;
            this.coordAnimalY = coordAnimalY;
            this.typeAnimal = typeAnimal;
            gender = genderAnimal;
        }
        public abstract void Eat(int x, int y);
        protected abstract void CheckPartner(int x, int y);
        public abstract void Die();
        protected abstract void GoNextSquare(int x0, int y0, int x, int y);
        public abstract void SetAnimal();
        public abstract void CheckForTamingAnimals();
        public abstract void KillAnimal();
        public abstract void EatFromInventory();
        public abstract string GoodJobFromAnimal();
        public abstract void PutGoodThingIntoInventory(string goodthing);
        public abstract void MoveAnimalFromLastSquare(int x0, int y0);
        public abstract void CheckForFieldAndUseIt();
        public abstract void CheckForBuildingAndUseIt();
        public abstract void CheckForHomeAndUseIt();
        public abstract void TryToBuildBuilding();
        public abstract void HumanCheckPartner();


        public void AnimalStandartAction(int lostSatietyLevelThere)   // стандартные действия животного
        {
            age -= 1;
            if (age > 0)
            {
                if (satietyLevel > 15 && health > 0)
                {
                    satietyLevel -= lostSatietyLevel;
                    if (typeAnimal != "human" && IsTamed == false)
                    {
                        WalkOrSearchPartner();
                    }
                    else if (typeAnimal == "human")
                    {
                        WalkOrSearchPartner();
                        CheckForTamingAnimals();
                        KillAnimal();
                        CheckForFieldAndUseIt();
                        CheckForBuildingAndUseIt();
                        CheckForHomeAndUseIt();
                        TryToBuildBuilding();
                    }
                }

                else if (health > 0 && satietyLevel <= 15)
                {
                    if (satietyLevel <= 0)
                    {
                        health -= 5;
                    }
                    else if (satietyLevel > 0)
                    {
                        satietyLevel -= lostSatietyLevel;
                    }

                    if (typeAnimal == "human")
                    {
                        foreach(Animal tamedAnimal in tamedAnimals)
                        {
                            string goodJobFromAnimal = tamedAnimal.GoodJobFromAnimal();
                            PutGoodThingIntoInventory(goodJobFromAnimal);
                        }
                        EatFromInventory();
                    }
                    else if (IsTamed == false)
                    {
                        SearchFood();
                    }
                }
                else if (health <= 0)
                {
                    Die();
                }
            }
            else Die();
        }

        public void AnimalWinterAction()
        {
            lostSatietyLevel = 7;
            if (IsHibernation == true)
            {
                health = constHealth;
            }
            else
            {
                AnimalStandartAction(lostSatietyLevel);
            }
        }

        protected void WalkOrSearchPartner()
        {
            int[] directions = new int[] { 0, 1, 2, 3 };
            int direction = directions[rnd.Next(0, 4)];
            switch (direction)
            {
                case 0:
                    if (coordAnimalY != 0 && coordAnimalX >= 0 && coordAnimalY > 0)
                    {
                        GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX, coordAnimalY - 1);
                        if (typeAnimal != "human" || (typeAnimal == "human" && HasHumanPair == false))
                        {
                            CheckPartner(coordAnimalX, coordAnimalY - 1);
                        }
                    }

                    break;

                case 1:
                    if (coordAnimalX != 999 && coordAnimalX >= 0 && coordAnimalY >= 0)
                    {
                        GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX + 1, coordAnimalY);
                        if (typeAnimal != "human" || (typeAnimal == "human" && HasHumanPair == false))
                        {
                            CheckPartner(coordAnimalX + 1, coordAnimalY);
                        }
                    }

                    break;

                case 2:
                    if (coordAnimalY != 999 && coordAnimalX >= 0 && coordAnimalY >= 0)
                    {
                        GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX, coordAnimalY + 1);
                        if (typeAnimal != "human" || (typeAnimal == "human" && HasHumanPair == false))
                        {
                            CheckPartner(coordAnimalX, coordAnimalY + 1);
                        }
                    }

                    break;

                case 3:
                    if (coordAnimalX != 0 && coordAnimalX > 0 && coordAnimalY >= 0)
                    {
                        GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX - 1, coordAnimalY);
                        if (typeAnimal != "human" || (typeAnimal == "human" && HasHumanPair == false))
                        {
                            CheckPartner(coordAnimalX - 1, coordAnimalY);
                        }
                    }

                    break;
            }
        }

        public void SearchFood()
        {
            string[] directions = new string[] { "up", "right", "down", "left" };
            string direction = directions[rnd.Next(0, 4)];
            switch (direction)
            {
                case "up":
                    if (coordAnimalY != 0 && coordAnimalX >= 0 && coordAnimalY > 0)
                    {
                        GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX, coordAnimalY - 1);
                        Eat(coordAnimalX, coordAnimalY - 1);
                    }

                    break;

                case "right":
                    if (coordAnimalX != 999 && coordAnimalX >= 0 && coordAnimalY >= 0)
                    {
                         GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX + 1, coordAnimalY);
                         Eat(coordAnimalX + 1, coordAnimalY);
                    }

                    break;

                case "down":
                    if (coordAnimalY != 999  && coordAnimalX >= 0 && coordAnimalY >= 0)
                    {
                          GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX, coordAnimalY + 1);
                          Eat(coordAnimalX, coordAnimalY + 1);
                    }

                    break;

                case "left":
                    if (coordAnimalX != 0 && coordAnimalX > 0 && coordAnimalY >= 0)
                    {
                          GoNextSquare(coordAnimalX, coordAnimalY, coordAnimalX - 1, coordAnimalY);
                          Eat(coordAnimalX - 1, coordAnimalY);
                    }

                    break;
            }
        }

        protected void CreatingChild()
        {
            List<int> EmptySquare = new List<int> { -1, -1 };
            EmptySquare = locality.FindSquare(coordAnimalX, coordAnimalY);
            if (EmptySquare[0] != -1 && EmptySquare[1] != -1)
            {
                if (typeAnimal == "human")
                {
                    Inventory inventory = new Inventory();
                    inventory.FillStartDictionary();
                    Human humanObject = new Human(map, locality, EmptySquare[0], EmptySquare[1], typeAnimal, inventory, gender);
                    humanObject.CreateHuman();
                }
                else if (typeAnimal == "predator")
                {
                    Predator oneAnimal = new Predator(map, locality, EmptySquare[0], EmptySquare[1],
                                          typeAnimal, gender);
                    oneAnimal.CreateChild(EmptySquare[0], EmptySquare[1], animalName);
                    oneAnimal.timeAfterReproduction = 3;

                }
                else if (typeAnimal == "omnivor")
                {
                    Omnivor oneAnimal = new Omnivor(map, locality, EmptySquare[0], EmptySquare[1],
                                         typeAnimal, gender);
                    oneAnimal.CreateChild(EmptySquare[0], EmptySquare[1], animalName);
                    oneAnimal.timeAfterReproduction = 3;
                }
                else if (typeAnimal == "herbivor")
                {
                    Herbivor oneAnimal = new Herbivor(map, locality, EmptySquare[0], EmptySquare[1],
                                         typeAnimal, gender);
                    oneAnimal.CreateChild(EmptySquare[0], EmptySquare[1], animalName);
                    oneAnimal.timeAfterReproduction = 3;
                }
                timeAfterReproduction = 0;
            }
        }
    }
}