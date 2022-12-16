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
using Humans;
using Omnivores;

namespace Homes
{
    public class Home : Inventory 
    {
        private int capacity = 0;
        private GenerateAndDraw locality;
        private int coordX;
        private int coordY;
        private Inventory inventory;
        public List<Home> NeighbourHomes = new List<Home>();
        public Human FirstOwner = null;
        public Human SecondOwner = null;

        public Home (GenerateAndDraw locality, int coordX, int coordY)
        {
            this.locality = locality;
            this.coordX = coordX;
            this.coordY = coordY;
        }

        public void CreateHome()
        {
            locality.descriptionSquares[coordX, coordY].home = this;
            inventory = new Inventory();
        }

        public void SetOwners(Human first, Human second)
        {
            FirstOwner = first;
            SecondOwner = second;
        }

        public void PutIntoHome(Inventory inventorySecond)
        {
            if (capacity < size)
            {
                inventorySecond.AddOrTakeFromInventaryHome(inventory);
            }
        }

        public void GetFromHome(Inventory inventorySecond)
        {
            inventory.AddOrTakeFromInventaryHome(inventorySecond);
        }

        public void PutHomeIntoVillage(int x, int y)
        {
            locality.descriptionSquares[x, y].IsItVillage = true;
            FirstOwner.IsVillager = true;
            SecondOwner.IsVillager = true;
        }
    }
}
