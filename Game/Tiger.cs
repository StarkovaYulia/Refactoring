using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Predators;
using System.Drawing;
using Animals;
using GeneratingAndDrawing;
using ClassMap;

namespace Tigers
{
    class Tiger : Predator
    {
        public Tiger(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = true;
            IsTamed = false;
            CanBeTamed = false;
            color = Color.FromArgb(33, 33, 33);
            health = 35;
            constHealth = health;
            satietyLevel = 25;
            age = 5;
            animalName = "tiger";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].predator.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }

        public override string GoodJobFromAnimal() { return "none"; }
    }
}
