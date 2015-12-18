using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PineApple
{
    public partial class Form2 : Form
    {
        Form1 MainForm;
        List<PictureBox> Pb;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            MainForm = form1;
            Pb = new List<PictureBox>(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PictNanediVallis_MouseClick(object sender, MouseEventArgs e)
        {
            double xRatio = (double)PictNanediVallis.Width / PictNanediVallis.Image.Width;
            double yRatio = (double)PictNanediVallis.Height / PictNanediVallis.Image.Height;
            Point basePixel = new Point(e.X, e.Y);

            // Application des taux d'étirement/compression du ratio
            basePixel.X = (int)(basePixel.X / xRatio);
            basePixel.Y = (int)(basePixel.Y / yRatio);
            /* Le clic sur l'image redimensionnée renvoie désormais les coordonnées (arrondies)
             * de l'image en taille réelle. Les axes partent du coin haut gauche du picture box :
             * Pour les abcisses : gauche -> droite
             * Pour les ordonnées : haut -> bas
             */

            /* L'origine du repère est fixé au pixel (700,1000).
             * 1 pixel correspond à 5 mètres.
             * => Il faut décaler l'origine, inverser le sens de l'axe des ordonnées et mettre à l'échelle
             */
            int metersX = (basePixel.X - 700) * 5;
            int metersY = (basePixel.Y - 1000) * (-5);
            // Insertion dans les TextBoxes correspondantes
            textBox1.Text = metersX.ToString();
            textBox2.Text = metersY.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            List<Location> a = new List<Location>(0);

            PictNanediVallis.Controls.Clear();
            if(checkBox2.Checked)
            {
                a = MainForm.getExploLocations(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), checkBox2.Checked,checkBox3.Checked,checkBox1.Checked);

                showVehicule(a);
            }
            if(checkBox3.Checked)
            {
                a = MainForm.getExploLocations(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), checkBox2.Checked,checkBox3.Checked,checkBox1.Checked);

                showScaf(a);
            }
            if(checkBox1.Checked)
            {
                a = MainForm.getExploLocations(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), checkBox2.Checked,checkBox3.Checked,checkBox1.Checked);

                showExtExp(a);
            }
     
            
        }
        private void showScaf(List<Location> l)
        {
            foreach(Location L in l)
            {
                PictureBox p = new PictureBox();
                
                p.Image = new Bitmap(Image.FromFile("astronauts.png"), new Size(20, 20));
                p.Location = new Point(L.getLocation()[0],L.getLocation()[1]); 
                PictNanediVallis.Controls.Add(p);
            }
        }
        private void showExtExp(List<Location> l)
        {
            foreach(Location L in l)
            {
                PictureBox p = new PictureBox();
                p.Image = new Bitmap(Image.FromFile("breaker5.png"), new Size(20, 20));

                p.Location = new Point(L.getLocation()[0], L.getLocation()[1]);
                PictNanediVallis.Controls.Add(p);

            }
        }
        private void showVehicule(List<Location> l)
        {
            foreach (Location L in l)
            {
                PictureBox p = new PictureBox();
                p.Image = new Bitmap(Image.FromFile("car3.png"), new Size(20, 20));
                p.Location = new Point(L.getLocation()[0], L.getLocation()[1]);
                PictNanediVallis.Controls.Add(p);
            }
        }
    }
}
