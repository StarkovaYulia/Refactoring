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

namespace Bears
{
    class Bear : Omnivor
    {
        public Bear(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = true;
            IsTamed = false;
            CanBeTamed = false;
            color = Color.FromArgb(40, 40, 40);
            health = 35;
            constHealth = health;
            satietyLevel = 25;
            age = 5;
            animalName = "bear";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].omnivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }
        public override string GoodJobFromAnimal() { return "none"; }
    }
}
