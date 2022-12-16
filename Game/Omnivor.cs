using System;
using System.Collections.Generic;
using ClassMap;
using System.Linq;
using GeneratingAndDrawing;
using System.Drawing;
using Animals;
using Herbivores;
using Predators;
using Ravens;
using Dogs;
using Bears;
using Humans;
using Homes;

namespace Omnivores
{
    public class Omnivor : Animal
    {
        public Home home = null;
        public Human pairHuman = null;
        public Omnivor(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public Omnivor(Map map, GenerateAndDraw locality, int coordAnimalX, int coordAnimalY, string typeAnimal, int genderAnimal)
            : base(map, locality, coordAnimalX, coordAnimalY, typeAnimal, genderAnimal)
        {

        }
        public override void SetAnimal()
        {
            typeAnimal = "omnivor";
            int[] coordinations = new int[2] { -1, -1 };
            coordinations = locality.GenerateRandomNumbers();
            coordAnimalX = coordinations[0];
            coordAnimalY = coordinations[1];
            CreateKindOfAnimal();
            if (coordAnimalX != - 1 && coordAnimalY != -1)
            {
                if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereOmnivor == false)
                {
                    locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereOmnivor = true;

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
                    Raven raven = new Raven(map, locality, kind);
                    raven.CreateKind();
                    break;
                case 1:
                    Dog dog = new Dog(map, locality, kind);
                    dog.CreateKind();
                    break;
                case 2:
                    Bear bear = new Bear(map, locality, kind);
                    bear.CreateKind();
                    break;
            }
        }
        public override void MoveAnimalFromLastSquare(int x, int y)
        {
            locality.descriptionSquares[x, y].omnivor.Remove(this);
            if (locality.descriptionSquares[x, y].omnivor.Count == 0)
            {
                locality.descriptionSquares[x, y].IsThereOmnivor = false;
                if (locality.descriptionSquares[x, y].IsTherePredator != true && locality.descriptionSquares[x, y].IsThereHerbivor != true && locality.descriptionSquares[x, y].IsTherePlant != true)
                {
                    locality.descriptionSquares[x, y].isEmpty = true;
                }
            }
            if (typeAnimal == "human")
            {
                locality.descriptionSquares[x, y].human.Remove(this);
                if (locality.descriptionSquares[x, y].human.Count == 0)
                {
                    locality.descriptionSquares[x, y].IsThereHuman = false;
                }
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
        public override void HumanCheckPartner() { }
        public override void TryToBuildBuilding() { }
        public override void Die() // смерть
        {
            MoveAnimalFromLastSquare(coordAnimalX, coordAnimalY);
            locality.animalsObjects.Remove(this);

        }

        private void AskOwnerEat()
        {
           int x = owner.inventory.HasTameThingInInventory(this.thingForTaming);
            if (x == 1)
            {
                satietyLevel += 10;
                owner.inventory.thingsInventory[thingForTaming] -= 1;
            }
            else
            {
                EatByThemselves(coordAnimalX, coordAnimalY);
            }
        }

        private void EatByThemselves(int x, int y)
        {
            if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].IsTherePlant == true)
            {
                if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].plant[0].isEatable == true)
                {
                    if (locality.descriptionSquares[x, y].plant[0].isPoisoned == true)
                    {
                        Die();
                    }
                    else
                    {
                        satietyLevel += 1;
                    }
                    locality.descriptionSquares[x, y].plant = null;
                    locality.descriptionSquares[x, y].IsTherePlant = false;
                }
            }
            else if (x >= 0 && y >= 0 &&  locality.descriptionSquares[x, y].IsThereHerbivor == true)
            {
                satietyLevel += 2;
                // locality.descriptionSquares[x, y].herbivor[0].Die();
            }
            else if (x >= 0 && y >= 0 && locality.descriptionSquares[x, y].IsTherePredator == true)
            {
                satietyLevel += 2;
                // locality.descriptionSquares[x, y].predator[0].Die();
            }
            if (typeAnimal == "human")
            {
                CheckForHomeAndUseIt();
            }
        }
        public override void Eat(int x, int y)
        {
            if (IsTamed == true && typeAnimal != "human")
            {
                AskOwnerEat();
            }
            else
            {
                EatByThemselves(x, y);
            }

        }
        protected override void CheckPartner(int x, int y)
        {
            if (typeAnimal == "human" && locality.descriptionSquares[coordAnimalX, coordAnimalY].human.Count != 1)
            {
                HumanCheckPartner();
            }
            else if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereOmnivor == true)
            {
                if (timeAfterReproduction == 5)
                {
                    foreach (Omnivor omnivorAnimal in locality.descriptionSquares[coordAnimalX, coordAnimalY].omnivor)
                    {
                        if (omnivorAnimal.timeAfterReproduction == 5 && omnivorAnimal != this && omnivorAnimal.typeAnimal != "human" && omnivorAnimal.gender != gender)
                        {
                            CreatingChild();
                            omnivorAnimal.timeAfterReproduction = 0;
                        }
                    }
                }
                else
                {
                    timeAfterReproduction += 1;
                }
            }
        }

        private void MoveTamedAnimalsWithOwner(int x0, int y0, int x, int y)
        {
            foreach (Animal tamedAnimal in tamedAnimals)
            {
                tamedAnimal.MoveAnimalFromLastSquare(x0, y0);
                if (tamedAnimal.typeAnimal == "predator")
                {
                    if (locality.descriptionSquares[x, y].predator.Count == 0)
                    {
                        locality.descriptionSquares[x, y].IsTherePredator = true;
                    }
                    locality.descriptionSquares[x, y].predator.Add(tamedAnimal);
                }
                else if (tamedAnimal.typeAnimal == "omnivor")
                {
                    if (locality.descriptionSquares[x, y].omnivor.Count == 0)
                    {
                        locality.descriptionSquares[x, y].IsThereOmnivor = true;
                    }
                    locality.descriptionSquares[x, y].omnivor.Add(tamedAnimal);
                }
                else if (tamedAnimal.typeAnimal == "herbivor")
                {
                    if (locality.descriptionSquares[x, y].herbivor.Count == 0)
                    {
                        locality.descriptionSquares[x, y].IsThereHerbivor = true;
                    }
                    locality.descriptionSquares[x, y].herbivor.Add(tamedAnimal);
                }
            }

        }
        protected override void GoNextSquare(int x0, int y0, int x, int y)
        {
            MoveAnimalFromLastSquare(x0, y0);
            if (locality.descriptionSquares[x, y].omnivor.Count == 0)
            {
                locality.descriptionSquares[x, y].IsThereOmnivor = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }
            }
            coordAnimalX = x;
            coordAnimalY = y;
            locality.descriptionSquares[x, y].omnivor.Add(this);
            MoveTamedAnimalsWithOwner(x0, y0, x, y);

        }

        protected internal void CreateChild(int x, int y, string nameOfAnimal)
        {
            int kindOfAnimal = -1;
            switch (nameOfAnimal)
            {
                case "raven":
                    kindOfAnimal = 0;
                    break;
                case "dog":
                    kindOfAnimal = 1;
                    break;
                case "bear":
                    kindOfAnimal = 2;
                    break;
                default:
                    break;
            }
            CreateKindOfAnimalSecond(kindOfAnimal);
            if (locality.descriptionSquares[x, y].IsThereOmnivor == false)
            {
                locality.descriptionSquares[x, y].IsThereOmnivor = true;
                if (locality.descriptionSquares[x, y].isEmpty == true)
                {
                    locality.descriptionSquares[x, y].isEmpty = false;
                }
            }
        }
    }
}







