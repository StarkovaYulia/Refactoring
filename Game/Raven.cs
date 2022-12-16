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

namespace Ravens
{
    class Raven : Omnivor
    {
        public Raven(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = false;
            IsTamed = false;
            CanBeTamed = false;
            color = Color.FromArgb(18, 25, 16);
            health = 35;
            constHealth = health;
            satietyLevel = 20;
            age = 4;
            animalName = "raven";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].omnivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }
    }
}
