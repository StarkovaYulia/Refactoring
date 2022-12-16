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

namespace Goats
{
    class Goat : Herbivor
    {
        public Goat(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = false;
            IsTamed = false;
            CanBeTamed = false;
            color = Color.FromArgb(30, 17, 18);
            health = 25;
            constHealth = health;
            satietyLevel = 20;
            age = 5;
            animalName = "goat";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].herbivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }
        public override string GoodJobFromAnimal() { return "none"; }
    }
}
