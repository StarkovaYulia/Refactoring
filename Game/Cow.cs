using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Herbivores;
using System.Drawing;
using Animals;
using GeneratingAndDrawing;
using ClassMap;

namespace Cows
{
    class Cow: Herbivor
    { 
        public Cow(Map map, GenerateAndDraw locality, int genderAnimal) 
            :base(map, locality, genderAnimal)
        {
            
        }
        public void CreateKind()
        {
            thingForTaming = "eatable_fruit_healthy";
            IsHibernation = false;
            IsTamed = false;
            CanBeTamed = true;
            color = Color.FromArgb(28, 28, 28);
            health = 20;
            constHealth = health;
            satietyLevel = 20;
            age = 5;
            animalName = "cow";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].herbivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }

        public override string GoodJobFromAnimal()
        {
            return "cow meat";
        }
    }
}
