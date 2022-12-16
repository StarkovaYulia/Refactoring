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

namespace Fields
{
    public class Field
    {
        public int reserve = 10;
        private int coordX;
        private int coordY;
        private GenerateAndDraw locality;
        public Resource resourceInField;
        private Random rnd = new Random();
        public Field(int coordX, int coordY, GenerateAndDraw locality)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            this.locality = locality;
        }

        public void CreateField()
        {
            int randomInt = rnd.Next(0, 4);
            if (randomInt == 0)
            {
                resourceInField = new Gold();
            }
            else if (randomInt == 1)
            {
                resourceInField = new Iron();
            }
            else if (randomInt == 2)
            {
                resourceInField = new Wood();
            }
            else if (randomInt == 3)
            {
                resourceInField = new Stone();
            }
        }
        private void ClearMapFromField()
        {
            locality.descriptionSquares[coordX, coordY].field.Remove(this);
        }

        public void GiveResource<T>(T resource, Inventory inventory) where T : Resource, new()
        {
            var newResource = new T();
            int IsEmptyPlaceInInventory = inventory.CheckEmptySpaceInventory();
            if (IsEmptyPlaceInInventory == 1)
            {
                inventory.PutIntoInventoryUnEatableObject(newResource);
                reserve -= 1;
                if (reserve == 0)
                {
                    ClearMapFromField();
                }
            }   
         }

    }
}
