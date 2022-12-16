using System;
using System.Collections.Generic;
using ClassMap;
using GeneratingAndDrawing;
using System.Drawing;
using Animals;
using Omnivores;
using Predators;
using Cows;
using Gophers;
using Goats;

namespace Herbivores
{
    public class Herbivor : Animal
    {
        public Herbivor(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public Herbivor(Map map, GenerateAndDraw locality, int coordAnimalX, int coordAnimalY, string typeAnimal, int genderAnimal)
            : base(map, locality, coordAnimalX, coordAnimalY, typeAnimal, genderAnimal)
        {

        }
        public override void SetAnimal()
        {
            typeAnimal = "herbivor";
            int[] coordinations = new int[2] { -1, -1 };
            coordinations = locality.GenerateRandomNumbers();
            coordAnimalX = coordinations[0];
            coordAnimalY = coordinations[1];
            CreateKindOfAnimal();
            if (coordAnimalX != -1 && coordAnimalY != -1)
            {
                if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereHerbivor == false)
                {
                    locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereHerbivor = true;
                }
                if (locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty == true)
                {
                    locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
                }
            }
        }
        private void CreateKindOfAnimal()
        {
            int kind = rnd.Next(0, 3);
            timeAfterReproduction = 5;
            CreateKindOfAnimalSecond(kind);
        }

        private void CreateKindOfAnimalSecond(int kind)
        {
            switch (kind)
            {
                case 0:
                    Cow cow = new Cow(map, locality, kind);
                    cow.CreateKind();
                    break;
                case 1:
                    Gopher gopher = new Gopher(map, locality, kind);
                    gopher.CreateKind();
                    break;
                case 2:
                    Goat goat = new Goat(map, locality, kind);
                    goat.CreateKind();
                    break;
            }
        }
        public override void CheckForTamingAnimals() { }
        public override void KillAnimal() { }
        public override void EatFromInventory() { }
        public override string GoodJobFromAnimal() { return "none"; }
        public override void PutGoodThingIntoInventory(string goodthing) { }
        public override void CheckForFieldAndUseIt() { }
        public override void CheckForBuildingAndUseIt() { }
        public override void CheckForHomeAndUseIt() { }
        public override void TryToBuildBuilding() { }
        public override void HumanCheckPartner() { }

        public override void MoveAnimalFromLastSquare(int x, int y)
        {
            locality.descriptionSquares[x, y].herbivor.Remove(this);
            if (locality.descriptionSquares[x, y].herbivor.Count == 0)
            {
                locality.descriptionSquares[x, y].IsThereHerbivor = false;
                if (locality.descriptionSquares[x, y].IsTherePredator != true && locality.descriptionSquares[x, y].IsThereOmnivor != true && locality.descriptionSquares[x, y].IsTherePlant != true)
                {
                    locality.descriptionSquares[x, y].isEmpty = true;
                }
            }
        }

        public override void Die() // смерть
        {
            MoveAnimalFromLastSquare(coordAnimalX, coordAnimalY);
            locality.animalsObjects.Remove(this);
        }

        public override void Eat(int x, int y)
        {
            if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].IsTherePlant == true)
            {
                if (locality.descriptionSquares[x, y].plant[0].isEatable == true)
                {
                    if (locality.descriptionSquares[x, y].plant[0].isPoisoned == true)
                    {
                        Die();
                    }
                    else
                    {
                        satietyLevel += 5;
                    }
                    locality.descriptionSquares[x, y].plant = null;
                    locality.descriptionSquares[x, y].IsTherePlant = false;
                }
            }
        }

        protected override void CheckPartner(int x, int y)
        {
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereHerbivor == true)
            {

                if (timeAfterReproduction == 5)
                {
                    foreach (Herbivor herbivorAnimal in locality.descriptionSquares[coordAnimalX, coordAnimalY].herbivor)
                    {
                        if (herbivorAnimal.timeAfterReproduction == 5 && herbivorAnimal != this && herbivorAnimal.typeAnimal != "human" && herbivorAnimal.gender != gender)
                        {
                            CreatingChild();
                            herbivorAnimal.timeAfterReproduction = 0;
                        }
                    }
                }
                else
                {
                    timeAfterReproduction += 1;
                }

            }
        }

        protected override void GoNextSquare(int x0, int y0, int x, int y)
        {
            MoveAnimalFromLastSquare(x0, y0);
            if (locality.descriptionSquares[x, y].herbivor.Count == 0)
            {
                locality.descriptionSquares[x, y].IsThereHerbivor = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }
            }
            coordAnimalX = x;
            coordAnimalY = y;
            locality.descriptionSquares[x, y].herbivor.Add(this);
        }

        protected internal void CreateChild(int x, int y, string nameOfAnimal)
        {
            int kindOfAnimal = -1;
            switch (nameOfAnimal)
            {
                case "cow":
                    kindOfAnimal = 0;
                    break;
                case "gopher":
                    kindOfAnimal = 1;
                    break;
                case "goat":
                    kindOfAnimal = 2;
                    break;
                default:
                    break;
            }
            CreateKindOfAnimalSecond(kindOfAnimal);
            if (locality.descriptionSquares[x, y].IsThereHerbivor == false)
            {
                locality.descriptionSquares[x, y].IsThereHerbivor = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }
            }
        }
    }
}







