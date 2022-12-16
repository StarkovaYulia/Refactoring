using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Animals;
using GeneratingAndDrawing;
using ClassMap;
using Herbivores;
using Predators;
using Omnivores;
using Inventories;
using Homes;
using Golds;
using Stones;
using Woods;
using Irons;
using Buildings;
using Resources;
using Fields;


namespace Humans  
{
    public class Human: Omnivor   
    {  
        public Inventory inventory;
        public bool IsVillager = false;

        public Human(Map map, GenerateAndDraw locality, Inventory inventory, int genderAnimal)
            : base(map, locality, genderAnimal)
        {
            this.inventory = inventory;
        }
        
        public Human (Map map, GenerateAndDraw locality, int coordAnimalX, int coordAnimalY, string typeAnimal, Inventory inventory, int genderAnimal)
            : base(map, locality, coordAnimalX, coordAnimalY, typeAnimal, genderAnimal)
        {
            this.coordAnimalX = coordAnimalX;
            this.coordAnimalX = coordAnimalY;
            this.typeAnimal = typeAnimal;
            this.inventory = inventory;
        }

        public override void EatFromInventory() 
        {
            bool HaveEaten = false;
            int indexation = 0;
            string meatName;
            foreach (KeyValuePair<string, int> keyValue in inventory.thingsInventory)
            {
                indexation += 1;
                meatName = keyValue.Key;
                if (keyValue.Value != 0)
                {
                    inventory.thingsInventory[meatName] -= 1;
                    HaveEaten = true;
                    break;
                }
            }
            if (HaveEaten == false)
            {
                SearchFood();
            } 
        }

        public void CreateHuman()
        { 
            age = 15;
            satietyLevel = 40;
            health = 30;
            color = Color.FromArgb(255, 0, 255);
            typeAnimal = "human";
            HasHumanPair = false;
            IsHibernation = false;
            IsTamed = false;
            int[] coordinations = new int[2];
            coordinations = locality.GenerateRandomNumbers();
            coordAnimalX = coordinations[0];
            coordAnimalY = coordinations[1];
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].human.Count == 0)
            {
                locality.descriptionSquares[coordAnimalX, coordAnimalY].IsThereHuman = true;
                if (locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty == true)
                {
                    locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
                }
            }
            locality.descriptionSquares[coordAnimalX, coordAnimalY].human.Add(this);
            locality.animalsObjects.Add(this); 


            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].IsItVillage == true)
            {
                IsVillager = true;
            }
        }

        public void FeedTamedAnimal(Animal tamedAnimalWhichWantsEat)  
        {
            tamedAnimalWhichWantsEat.Eat(coordAnimalX, coordAnimalY);
        }

        public override void HumanCheckPartner()
        {
            foreach (Human humanObject in locality.descriptionSquares[coordAnimalX, coordAnimalY].human)
            {
                if (humanObject.HasHumanPair == false && humanObject != this && humanObject.gender != gender)
                {
                    humanObject.HasHumanPair = true;
                    humanObject.pairHuman = this;
                    HasHumanPair = true;
                    pairHuman = humanObject;
                    home = new Home(locality, coordAnimalX, coordAnimalY);
                    if (locality.descriptionSquares[coordAnimalX, coordAnimalY].predator.Count == 0 &&
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].herbivor.Count == 0 &&
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].omnivor.Count == 0 &&
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].plant.Count == 0 &&
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].field.Count == 0 &&
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].building.Count == 0)
                    {
                        home.CreateHome();
                        humanObject.home = home;
                        home.SetOwners(this, humanObject);
                        CreatingChild();
                        CheckForVillage();
                    }
                }
            }
        }
        private void AddHomeIntoNeighbours(int x, int y)
        {
            home.NeighbourHomes.Add(locality.descriptionSquares[x, y].home);
            locality.descriptionSquares[x, y].home.NeighbourHomes.Add(home);   

            foreach (Home neighbour in locality.descriptionSquares[x, y].home.NeighbourHomes)
            {
                neighbour.NeighbourHomes.Add(home);
                home.NeighbourHomes.Add(neighbour); 
            }
            Home HomeIntoVillage = null;
            HomeIntoVillage = locality.descriptionSquares[coordAnimalX, coordAnimalY].home.NeighbourHomes.FirstOrDefault(homeSecond => homeSecond.NeighbourHomes.Count == 2);
            if (HomeIntoVillage != null)
            {
                home.PutHomeIntoVillage(coordAnimalX, coordAnimalY);
                foreach (Home neighbour in locality.descriptionSquares[x, y].home.NeighbourHomes)
                {
                    neighbour.PutHomeIntoVillage(x, y);
                }
            }
        }

        private void CheckForVillageSecond(int x, int y)
        {
            if (locality.descriptionSquares[x, y].IsItVillage == false)
            {
                AddHomeIntoNeighbours(x, y);
            }
            else
            {
                home.PutHomeIntoVillage(coordAnimalX, coordAnimalY);
                foreach (Home neighbour in locality.descriptionSquares[x, y].home.NeighbourHomes)
                {
                    neighbour.NeighbourHomes.Add(home);
                    home.NeighbourHomes.Add(neighbour);
                }
            }
        }

        public void CheckForVillage()
        {
            if (coordAnimalX != 0 && coordAnimalY != 0 && locality.descriptionSquares[coordAnimalX - 1, coordAnimalY - 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX - 1, coordAnimalY - 1);
            }
            else if (coordAnimalY != 0 && locality.descriptionSquares[coordAnimalX, coordAnimalY - 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX, coordAnimalY - 1);
            }
            else if (coordAnimalX != 0 && locality.descriptionSquares[coordAnimalX - 1, coordAnimalY].home != null)
            {
                CheckForVillageSecond(coordAnimalX - 1, coordAnimalY);
            }
            else if (coordAnimalY != 999 && locality.descriptionSquares[coordAnimalX, coordAnimalY + 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX, coordAnimalY + 1);
            }
                
            else if (coordAnimalX != 999 && locality.descriptionSquares[coordAnimalX + 1, coordAnimalY].home != null)
            {
                CheckForVillageSecond(coordAnimalX + 1, coordAnimalY);
            }

            else if (coordAnimalX != 999 && coordAnimalY != 0 && locality.descriptionSquares[coordAnimalX + 1, coordAnimalY - 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX + 1, coordAnimalY - 1);
            }

            else if (coordAnimalX != 999 && coordAnimalY != 999 && locality.descriptionSquares[coordAnimalX + 1, coordAnimalY + 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX + 1, coordAnimalY + 1);
            }

            else if (coordAnimalX != 0 && coordAnimalY != 999 && locality.descriptionSquares[coordAnimalX - 1, coordAnimalY + 1].home != null)
            {
                CheckForVillageSecond(coordAnimalX - 1, coordAnimalY + 1);
            }
        }

        public override void KillAnimal()
        {
            base.KillAnimal();
            Animal animalForKillng = null;
            animalForKillng = locality.animalsObjects.OfType<Animal>().FirstOrDefault(x => x.coordAnimalX == this.coordAnimalX &&
                                                                    x.coordAnimalY == this.coordAnimalY && x.IsTamed == false);
            if (animalForKillng != null)
            {
                inventory.PutIntoInventoryEatableObject(animalForKillng);
                animalForKillng.Die();
            }    
        }
        private void TameAnimal(Animal animalForTaming)  
        {
            int answer = inventory.HasTameThingInInventory(animalForTaming.thingForTaming);
            tamedAnimals.Add(animalForTaming);   
            animalForTaming.IsTamed = true;
            animalForTaming.owner = this;
        }

        public override void CheckForTamingAnimals()
        {
            base.CheckForTamingAnimals();
            Animal animalForTaming = null;
            animalForTaming = locality.animalsObjects.OfType<Animal>().FirstOrDefault(x => x.coordAnimalX == this.coordAnimalX &&
                                                                    x.coordAnimalY == this.coordAnimalY && x.CanBeTamed == true && x.IsTamed == false);
            if (animalForTaming != null)
            {
                TameAnimal(animalForTaming);
            }
        }

        public override void PutGoodThingIntoInventory(string goodthing) 
        {
            inventory.PutIntoInventoryGoodThing(goodthing);
        }

        public override void CheckForFieldAndUseIt() 
        { 
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].field.Count != 0 && ((IsVillager == true && gender == 1) || (IsVillager == false)))
            {
                int maximum = locality.descriptionSquares[coordAnimalX, coordAnimalY].field.Max(a => a.reserve);
                var fieldForUse = locality.descriptionSquares[coordAnimalX, coordAnimalY].field.FirstOrDefault(a => a.reserve == maximum);
                fieldForUse.GiveResource(fieldForUse.resourceInField, inventory);
            }
        }
        public override void CheckForBuildingAndUseIt() 
        { 
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].building.Count != 0 && ((IsVillager == true && gender == 1) || (IsVillager == false)))
            {
                int minimum = locality.descriptionSquares[coordAnimalX, coordAnimalY].building.Min(a => a.capacity);
                var buildingForUse = locality.descriptionSquares[coordAnimalX, coordAnimalY].building.FirstOrDefault(a => a.capacity == minimum);
                if (inventory.CheckEmptySpaceInventory() == 1)
                {
                    buildingForUse.GiveResource(buildingForUse.keepedResource, inventory);
                }
                else
                {
                    buildingForUse.PutIntoBuilding(buildingForUse.keepedResource, inventory);
                }      
            }
        }
        public override void CheckForHomeAndUseIt() 
        {
            if (locality.descriptionSquares[coordAnimalX, coordAnimalY].home != null)
            {
                bool IsHomeFromOurVillage = false;
                foreach (Home homeSecond in home.NeighbourHomes)
                {
                    if (homeSecond == locality.descriptionSquares[coordAnimalX, coordAnimalY].home)
                    {
                        IsHomeFromOurVillage = true;
                    }
                }

                if (IsHomeFromOurVillage == false)
                {
                    if (inventory.CheckEmptySpaceInventory() == 0)
                    {
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].home.PutIntoHome(inventory);
                    }
                    else if (locality.descriptionSquares[coordAnimalX, coordAnimalY].home != null)
                    {
                        locality.descriptionSquares[coordAnimalX, coordAnimalY].home.GetFromHome(inventory);
                    }
                }  
            }  
        }

        public override void TryToBuildBuilding() 
        {
            if (health > 10 && ((IsVillager == true && gender == 1) || (IsVillager == false)))   
            { 
                int KindOfBuildingHumanWantsBuild = rnd.Next(0, 4);
                Building newBuilding = new Building(locality);
                switch (KindOfBuildingHumanWantsBuild)
                {
                    case 0:
                        newBuilding.TryToCreate<Gold>(inventory, coordAnimalX, coordAnimalY);
                        break;
                    case 1:
                        newBuilding.TryToCreate<Iron>(inventory, coordAnimalX, coordAnimalY);
                        break;
                    case 2:
                        newBuilding.TryToCreate<Wood>(inventory, coordAnimalX, coordAnimalY);
                        break;
                    case 3:
                        newBuilding.TryToCreate<Stone>(inventory, coordAnimalX, coordAnimalY);
                        break;
                }
                
            }
        }
    }
}
