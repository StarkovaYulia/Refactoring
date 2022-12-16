using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class SimulationMenu : Form                             
    {
        public SimulationMenu()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var animalsText = textBox1.Text;
            var plantsText = textBox2.Text;

            BeforeStart x = new BeforeStart(animalsText, plantsText);
            x.Show(this);
            this.Hide();
        }
        private void SimulationMenu_Load(object sender, EventArgs e)  { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
       
    }
}
