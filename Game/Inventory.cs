using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humans;
using Plant;
using Animals;
using Resources;
using Golds;
using Irons;
using Stones;
using Woods;

namespace Inventories
{
    public class Inventory
    {
        public Inventory()
        {

        }
        public int size = 100;
        private Random rnd;
        public Dictionary<string, int> thingsInventory = new Dictionary<string, int>();
        public List<Resource> resources = new List<Resource>();
        public void FillStartDictionary()
        {
            thingsInventory.Add("cow meat", 0);
            thingsInventory.Add("goat meat", 0);
            thingsInventory.Add("gopher meat", 0);
            thingsInventory.Add("bear meat", 0);
            thingsInventory.Add("dog meat", 0);
            thingsInventory.Add("raven meat", 0);
            thingsInventory.Add("panther meat", 0);
            thingsInventory.Add("lion meat", 0);
            thingsInventory.Add("tiger meat", 0);

            thingsInventory.Add("eatable_fruit_poisoned", 0);
            thingsInventory.Add("eatable_fruit_healthy", 0);
            thingsInventory.Add("eatable_nofruit_poisoned", 0);
            thingsInventory.Add("eatable_nofruit_healthy", 0);
            thingsInventory.Add("noeatable_fruit", 0);
            thingsInventory.Add("noeatable_nofruit", 0);
        }
        public int HasTameThingInInventory(string thingForTaming)
        {
            if (thingsInventory.ContainsKey(thingForTaming) == true && thingsInventory[thingForTaming] != 0)
                return 1;
            else
                return 0;
        }

        public int CheckEmptySpaceInventory()
        {
            int sum = 0;
            foreach (int number in thingsInventory.Values)
            {
                sum += number;
            }
            sum += resources.Count;
            if (sum < size) return 1;
            else return 0;
        }
        public void PutIntoInventoryEatableObject(Animal meatForInventory)
        {
            int HasEmptySpace = CheckEmptySpaceInventory();
            if (HasEmptySpace == 1)
            {
                switch (meatForInventory.typeAnimal)
                {
                    case "cow":
                        thingsInventory["cow meat"] += 1;
                        break;
                    case "goat":
                        thingsInventory["goat meat"] += 1;
                        break;
                    case "gopher":
                        thingsInventory["gopher meat"] += 1;
                        break;
                    case "dog":
                        thingsInventory["dog meat"] += 1;
                        break;
                    case "bear":
                        thingsInventory["bear meat"] += 1;
                        break;
                    case "raven":
                        thingsInventory["raven meat"] += 1;
                        break;
                    case "lion":
                        thingsInventory["lion meat"] += 1;
                        break;
                    case "panther":
                        thingsInventory["panther meat"] += 1;
                        break;
                    case "tiger":
                        thingsInventory["tiger meat"] += 1;
                        break;
                }
            }
        }

        public void PutIntoInventoryUnEatableObject<T>(T resource) where T : Resource
        {
            int HasEmptySpace = CheckEmptySpaceInventory();
            if (HasEmptySpace == 1)
            {
                resources.Add(resource);
            }
        }
        public void PutIntoInventoryGoodThing(string goodthing)
        {
            int HasEmptySpace = CheckEmptySpaceInventory();
            if (HasEmptySpace == 1)
                thingsInventory[goodthing] += 1;
        }

        public void AddOrTakeFromInventaryHome(Inventory inventoryHome)  
        {
            rnd = new Random();
            int x = rnd.Next(0, 9);
            switch (x)
            {
                case 1:
                    thingsInventory["cow meat"] -= 1;
                    inventoryHome.thingsInventory["cow meat"] += 1;
                    break;
                case 2:
                    thingsInventory["goat meat"] -= 1;
                    inventoryHome.thingsInventory["goat meat"] += 1;
                    break;
                case 3:
                    thingsInventory["gopher meat"] -= 1;
                    inventoryHome.thingsInventory["gopher meat"] += 1;
                    break;
                case 4:
                    thingsInventory["dog meat"] -= 1;
                    inventoryHome.thingsInventory["dog meat"] += 1;
                    break;
                case 5:
                    thingsInventory["bear meat"] -= 1;
                    inventoryHome.thingsInventory["bear meat"] += 1;
                    break;
                case 6:
                    thingsInventory["raven meat"] -= 1;
                    inventoryHome.thingsInventory["raven meat"] += 1;
                    break;
                case 7:
                    thingsInventory["lion meat"] -= 1;
                    inventoryHome.thingsInventory["lion meat"] += 1;
                    break;
                case 8:
                    thingsInventory["panther meat"] -= 1;
                    inventoryHome.thingsInventory["panther meat"] += 1;
                    break;
                case 9:
                    thingsInventory["tiger meat"] -= 1;
                    inventoryHome.thingsInventory["tiger meat"] += 1;
                    break;
            }

        }
    }
}


