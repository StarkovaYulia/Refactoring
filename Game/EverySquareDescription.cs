using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Plant;
using Animals;
using Humans;
using Omnivores;
using EverySquareDescription;
using Resources;
using Buildings;
using Fields;
using Homes;

namespace EverySquareDescription
{
    public class SquareDescription
    {
        public bool isEmpty;
        public List<Plants> plant = new List<Plants>();   

        public bool IsTherePlant = false;
        public bool IsThereHerbivor = false;
        public bool IsThereOmnivor = false;
        public bool IsTherePredator = false;
        public bool IsThereHuman = false;

        public Home home = null;
        public bool IsItVillage = false;

        public List<Animal> herbivor = new List<Animal>();
        public List<Animal> omnivor = new List<Animal>();
        public List<Animal> predator = new List<Animal>();
        public List<Omnivor> human = new List<Omnivor>();
        public List<Field> field = new List<Field>();
        public List<Building> building = new List<Building>();

        private int x;
        private int y;
        public SquareDescription(int x, int y, Color color, bool isEmpty) 
        {
            this.x = x;
            this.y = y;
            this.isEmpty = isEmpty;
        }
    }
}
