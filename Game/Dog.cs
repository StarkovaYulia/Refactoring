using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omnivores;
using System.Drawing;
using Animals;
using GeneratingAndDrawing;
using ClassMap;

namespace Dogs
{ 
    class Dog : Omnivor
    {
        public Dog (Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = false;
            IsTamed = false;
            CanBeTamed = true;
            thingForTaming = "cow meat";
            color = Color.FromArgb(2, 32, 39);
            health = 20;
            constHealth = health;
            satietyLevel = 20;
            age = 5;
            animalName = "dog";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].omnivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }

        public override string GoodJobFromAnimal()
        {
            return "dog meat";
        }
    }
}
