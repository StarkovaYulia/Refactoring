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

namespace Panthers
{
    class Panther : Predator
    {
        public Panther(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = false;
            IsTamed = false;
            CanBeTamed = true;
            thingForTaming = "bear meat";
            color = Color.FromArgb(31, 14, 17);
            health = 40;
            constHealth = health;
            satietyLevel = 15;
            age = 5;
            animalName = "panther";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].predator.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }

        public override string GoodJobFromAnimal()
        {
            return "panther meat";
        }
    }
}
