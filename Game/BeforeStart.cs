using System;
using ClassMap;
using GeneratingAndDrawing;
using StartSimulation;
using System.Windows.Forms;

namespace Game
{
    public partial class BeforeStart : Form
    {
        private Map map;
        private GenerateAndDraw locality;
        private StartSimulationStep starter;
        private int numberAnimals;
        private int numberPlants;
        public BeforeStart(string animalsText, string plantsText)
        {
            InitializeComponent();
            timer1.Start();
            numberAnimals = int.Parse(animalsText);
            numberPlants = int.Parse(plantsText);
            map = new Map(pictureBox1);
            locality = new GenerateAndDraw(numberAnimals, numberPlants, map);
            map.GenerateSquares();

            locality.GenerateStart(locality);

            starter = new StartSimulationStep(locality, map);
            
        }
        private void DrawMap_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void timer1_Tick(object sender, EventArgs e)
        {
            starter.Tm_Tick();
        }
    }
}
