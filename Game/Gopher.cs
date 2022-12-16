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

namespace Gophers //surok
{
    class Gopher : Herbivor
    {
        public Gopher(Map map, GenerateAndDraw locality, int genderAnimal)
            : base(map, locality, genderAnimal)
        {

        }
        public void CreateKind()
        {
            IsHibernation = true;
            IsTamed = false;
            CanBeTamed = false;
            color = Color.FromArgb(0, 0, 0);
            health = 25;
            constHealth = health;
            satietyLevel = 25;
            age = 6;
            animalName = "gopher";
            locality.descriptionSquares[coordAnimalX, coordAnimalY].herbivor.Add(this);
            locality.descriptionSquares[coordAnimalX, coordAnimalY].isEmpty = false;
            locality.animalsObjects.Add(this);
        }
        public override string GoodJobFromAnimal() { return "none"; }
    }
}
