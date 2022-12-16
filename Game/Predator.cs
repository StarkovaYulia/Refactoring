using System;
using System.Collections.Generic;
using ClassMap;
using GeneratingAndDrawing;
using System.Drawing;
using Animals;
using Herbivores;
using Omnivores;
using Lions;
using Tigers;
using Panthers;

namespace Predators
{
    public class Predator : Animal
    {
        public Predator(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public Predator(Map map, GenerateAndDraw locality, int coordAnimalX, int coordAnimalY, string typeAnimal, int genderAnimal)
            : base(map, locality, coordAnimalX, coordAnimalY, typeAnimal, genderAnimal)
        {

        }
        public override void SetAnimal()
        {
            typeAnimal = "predator";
            int[] coordinations = new int[2] { -1, -1 };
            coordinations = locality.GenerateRandomNumbers();
            coordAnimalX = coordinations[0];
            coordAnimalY = coordinations[1];
            CreateKindOfAnimal();
            if (coordAnimalX != -1 && coordAnimalY != -1)
            {
                if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsTherePredator == false)
                {
                    locality.descriptionSquares[coordAnimalX, coordAnimalY].IsTherePredator = true;
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
                    Lion lion = new Lion(map, locality, kind);
                    lion.CreateKind();
                    break;
                case 1:
                    Tiger tiger = new Tiger(map, locality, kind);
                    tiger.CreateKind();
                    break;
                case 2:
                    Panther panther = new Panther(map, locality, kind);
                    panther.CreateKind();
                    break;
            }
        }
        public override void MoveAnimalFromLastSquare(int x, int y)
        {
            locality.descriptionSquares[x, y].predator.Remove(this);
            if (locality.descriptionSquares[x, y].predator.Count == 0)
            {
                locality.descriptionSquares[x, y].IsTherePredator = false;
                if (locality.descriptionSquares[x, y].IsThereOmnivor != true && locality.descriptionSquares[x, y].IsThereHerbivor != true && locality.descriptionSquares[x, y].IsTherePlant != true)
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
            if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].IsThereHerbivor == true)
            {
                satietyLevel += 10;
               // locality.descriptionSquares[x, y].herbivor[0].Die();
            }
            else if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].IsThereOmnivor == true)
            {
                satietyLevel += 10;
               // locality.descriptionSquares[x, y].omnivor[0].Die();
            }
        }

        protected override void CheckPartner(int x, int y)
        {
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsTherePredator == true)
            {

                if (timeAfterReproduction == 5)
                {
                    foreach (Predator predatorAnimal in locality.descriptionSquares[coordAnimalX, coordAnimalY].predator)
                    {
                        if (predatorAnimal.timeAfterReproduction == 5 && predatorAnimal != this && predatorAnimal.typeAnimal != "human" && predatorAnimal.gender != gender)
                        {
                            CreatingChild();
                            predatorAnimal.timeAfterReproduction = 0;
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
            if (locality.descriptionSquares[x, y].predator.Count == 0)
            {
                locality.descriptionSquares[x, y].IsTherePredator = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }
            }
            coordAnimalX = x;
            coordAnimalY = y;
            locality.descriptionSquares[x, y].predator.Add(this);

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
        protected internal void CreateChild(int x, int y, string nameOfAnimal)
        {
            int kindOfAnimal = -1;
            switch (nameOfAnimal)
            {
                case "lion":
                    kindOfAnimal = 0;
                    break;
                case "tiger":
                    kindOfAnimal = 1;
                    break;
                case "panther":
                    kindOfAnimal = 2;
                    break;
                default:
                    break;
            }
            CreateKindOfAnimalSecond(kindOfAnimal);
            if (locality.descriptionSquares[x, y].IsTherePredator == false)
            {
                locality.descriptionSquares[x, y].IsTherePredator = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }

            }
        }
    }
}







