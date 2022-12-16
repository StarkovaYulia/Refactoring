using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;
using EverySquareDescription;
using GeneratingAndDrawing;
using ClassMap;
using Golds;
using Irons;
using Stones;
using Woods;
using Inventories;

namespace Buildings
{
    public class Building
    {
        public int capacity = 0;
        private GenerateAndDraw locality;
        public Resource keepedResource;
        public Building(GenerateAndDraw locality)
        {
            this.locality = locality;
        }

        public void TryToCreate<T>(Inventory inventory, int coordX, int coordY) where T:  Resource, new()
        {
            var resources = (List<T>)inventory.resources.OfType<T>().ToList();
            if (resources.Count == 2)
            {
                keepedResource = new T();
                for (int i = 0; i < 2; i++)
                {
                    var x = inventory.resources.OfType<T>().FirstOrDefault();
                    inventory.resources.Remove(x);
                    locality.descriptionSquares[coordX, coordY].building.Add(this);
                } 
            }
        }

        public void GiveResource<T>(T resource, Inventory inventory) where T : Resource, new()
        {
            if (capacity != 0)
            {
                var newResource = new T();
                int IsEmptyPlaceInInventory = inventory.CheckEmptySpaceInventory();
                if (IsEmptyPlaceInInventory == 1)
                {
                    inventory.PutIntoInventoryUnEatableObject(newResource);
                    capacity -= 1;
                }
            }
        }

        public void PutIntoBuilding<T>(T resource, Inventory inventory) where T: Resource
        {
            if (capacity < 10)
            {
                capacity += 1;
                var x = inventory.resources.OfType<T>().FirstOrDefault();
                inventory.resources.Remove(x);
            }
        }
    }
}
