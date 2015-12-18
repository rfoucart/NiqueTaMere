﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PineApple
{
    public partial class Form1 : Form
    {
        const int numberOfDays = 500;
        private Mission mission;
        private List<Button> listButtonPanel;
        private int daySheet;
        private bool creating = true;
        public Form1()
        {
            InitializeComponent();
            listButtonPanel = new List<Button>(0);
            //mission = new Mission("wasabi",numberOfDays);


            this.ReadMissionXML();
            mission.ReadActivityXML();
            //mission.defaultDay(1);
            //mission.WriteActivityXML();

            globalPanel.Controls.Remove(globalPanel.GetControlFromPosition(0, 0));
            daySheet = 4;//getDaySheetNumberFromDay(mission.getCurrentDay().getDay());
            globalPanelInit();
            panelActu(daySheet);

            //mission.newAstronaute("jean-pierre");
            //mission.newAstronaute("jean-guy");
            //mission.newAstronaute("pierre-jean");

            //mission.newLocation("petaouchnok",150,200);
            //mission.newLocation("Base", 700, 1000);
            //this.WriteMissionXML();



            

            showDay(mission.getCurrentDay().getDay());
            searchInit();
            updateDateNow();
            updateSelectedTime(mission.getCurrentDay().getDay());

            dayHeaderInit();
        }
        //Initialisation/remplissage du groupbox de recherche
        private void searchInit()
        {
            List<Type> listeGenericType = mission.getActivityTypes();

            int i = 0;
            Dictionary<string, string> GT = new Dictionary<string, string>();
            Dictionary<string, string> T = new Dictionary<string, string>();
            foreach (Type t in listeGenericType)
            {
                GT.Add(i.ToString(), t.getGenericType());
                i++;
            }
            searchGTypeCombo.DataSource = new BindingSource(GT, null);
            searchGTypeCombo.DisplayMember = "Value";
            searchGTypeCombo.ValueMember = "Key";

            comboBoxGenericType.DataSource = new BindingSource(GT, null);
            comboBoxGenericType.DisplayMember = "Value";
            comboBoxGenericType.ValueMember = "Key";
            //string value = ((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Value;

        }

        private void globalPanelInit()
        {
            // Création du paneau de boutons
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Button cmd = new Button();
                    cmd.Text = string.Format("{0}", (i - 1) * 10 + j);
                    cmd.Image = new Bitmap(Image.FromFile("mol.png"), new Size(20, 20));
                    cmd.Bounds = button3.Bounds;
                    cmd.Click += new EventHandler(button3_Click);
                    cmd.ImageAlign = ContentAlignment.MiddleRight;
                    cmd.TextAlign = ContentAlignment.MiddleLeft;
                    cmd.FlatStyle = FlatStyle.Flat;
                    cmd.BackColor = Color.LightBlue;
                    cmd.Margin = new Padding(0, 0, 0, 0);
                    globalPanel.Controls.Add(cmd, i - 1, j - 1);
                    listButtonPanel.Add(cmd);
                }
            }
        }
        //Affichage de la Bande des heures dans la partie jour
        private void dayHeaderInit()
        {

            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = 150; // <<<-------
            //tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//TableLayoutPanelGrowStyle.FixedSize;//.AddColumns;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel1.ColumnCount);
                tableLayoutPanel1.ColumnStyles.Add(cs);
            }
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31));
            for (int x = 0; x < 25; x++)
            {
                Label Text2 = new Label();

                Text2.Text = x + "H";
                Text2.Font = new Font("Helvetica", 7);
                Text2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                Text2.BackColor = Color.LightCyan;
                Text2.Margin = new Padding(0, 0, 0, 0);
                Text2.Height = 30;
                Text2.AutoSize = false;
                tableLayoutPanel1.Controls.Add(Text2, x * 6, 0);
                tableLayoutPanel1.SetColumnSpan(Text2, 6);
                if (x == 24)
                {
                    tableLayoutPanel1.SetColumnSpan(Text2, 4);
                }

            }
            tableLayoutPanel1.ResumeLayout();

            astroNames.SuspendLayout();
            astroNames.Controls.Clear();
            astroNames.ColumnCount = 1; // <<<-------
            astroNames.RowCount = mission.getAstronautes().Count;
            astroNames.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//TableLayoutPanelGrowStyle.FixedSize;//.AddColumns;

            astroNames.RowStyles.Clear();
            for (int i = 0; i < astroNames.RowCount; i++)
            {
                astroNames.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / astroNames.RowCount));

            }
            for (int i = 0; i < astroNames.RowCount; i++)
            {
                Label Text2 = new Label();
                Text2.Text = mission.getAstronautes()[i].getName();
                Text2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                Text2.BackColor = Color.LightCyan;
                Text2.Margin = new Padding(0, 0, 0, 0);
                Text2.Height = 30;
                Text2.AutoSize = false;
                Text2.Dock = DockStyle.Fill;
                astroNames.Controls.Add(Text2, 0, i);
            }
            astroNames.ResumeLayout();

        }
        //renomme les boutons (de 50 en 50 ) 
        //probléme de vitesse pour l'instant
        public void panelActu(int i)
        {

            if (i >= 1 && i <= 10)
            {
                for (int j = 0; j < 50; j++)
                {
                    int jour = (i - 1) * 50 + (j + 1);
                    if (mission.outThisDay(jour) == false)
                    {
                        listButtonPanel[j].Image = null;
                    }
                    else
                    {
                        listButtonPanel[j].Image = new Bitmap(Image.FromFile("mol.png"), new Size(20, 20));
                    }

                    listButtonPanel[j].Text = string.Format("{0}", jour);
                    if (jour == mission.getCurrentDay().getDay())
                    {
                        listButtonPanel[j].BackColor = Color.LightGreen;
                    }
                    else if (jour > mission.getCurrentDay().getDay())
                    {
                        listButtonPanel[j].BackColor = Color.LightBlue;
                    }
                    else if (jour < mission.getCurrentDay().getDay())
                    {
                        listButtonPanel[j].BackColor = Color.LightGray;
                    }

                }
            }
        }
        //Actualise la partie jour en fonction du jour selectionné sur la partie calendrier. 
        public void showDay(int day)
        {
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.ColumnCount = 150; // <<<-------
            tableLayoutPanel2.RowCount = 0;
            tableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//.AddColumns;
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.AutoScrollMargin = new Size(0, 0);
            for (int i = 0; i < tableLayoutPanel2.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel2.ColumnCount);
                tableLayoutPanel2.ColumnStyles.Add(cs);
            }
            List<Activity> ListOfActivities = mission.selectActivitiesByDay(day);
            tableLayoutPanel2.RowCount = mission.getAstronautes().Count;
            for (int i = 0; i < tableLayoutPanel2.RowCount; i++)
            {
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanel2.RowCount));
            }

            if (ListOfActivities.Count() != 0)
            {
                foreach (Activity a in ListOfActivities)
                {
                    List<Astronaute> astronautes = mission.getAstronautes();
                    for (int j = 0; j < mission.getAstronautes().Count; j++)
                    {
                        foreach (int astro in a.getAstronautes())
                        {
                            if (astro == mission.getAstronautes()[j].getNumber())
                            {
                                Button cmd2 = new Button();
                                cmd2.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                                cmd2.Click += new System.EventHandler(cmd2_Click);
                                cmd2.Tag = a;
                                cmd2.MaximumSize = new Size(500, 200);
                                cmd2.Dock = DockStyle.Fill;
                                tableLayoutPanel2.Controls.Add(cmd2, hoursToColumn(a.getStartDate().getHours(), a.getStartDate().getMinutes()), j);
                                int length = lengthToColumn(a.getStartDate(), a.getEndDate());
                                tableLayoutPanel2.SetColumnSpan(cmd2, length);
                            }
                        }
                    }
                }
            }
            tableLayoutPanel2.ResumeLayout();
        }
        //Fait correspondre temps et colonnes sur le tableau d'affichage des jours
        private int hoursToColumn(int hours, int minutes)
        {
            return 6 * hours + minutes / 10;
        }
        private int lengthToColumn(MDate d, MDate f)
        {
            double D = d.getHours() * 60 + d.getMinutes();
            double F = f.getHours() * 60 + f.getMinutes();
            double DF = F - D;
            int df = (int)(DF / 10);
            return df;
        }
        //Ecrit la mission en xml
        private void WriteMissionXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Missions");
            xmlDoc.AppendChild(rootNode);

            mission.WriteXML(xmlDoc, rootNode);

            xmlDoc.Save("./mission.xml");
        }

        //Charge le fichier mission.xml
        private void ReadMissionXML()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("mission.xml");
            var name = doc.SelectSingleNode("/Missions/Mission/Name");
            var astronautes = doc.SelectNodes("/Missions/Mission/Astronautes/Astronaute");
            var refastro = doc.SelectSingleNode("/Missions/Mission/refNumberAstronaute");
            var locations = doc.SelectNodes("/Missions/Mission/Locations/Location");
            var reflocation = doc.SelectSingleNode("/Missions/Mission/refNumberLoc");

            //Create the mission
            mission = new Mission(name.InnerText, numberOfDays);

            //Add the astronautes
            foreach (XmlNode astronaute in astronautes)
            {
                mission.newAstronaute(astronaute["Name"].InnerText, int.Parse(astronaute["Number"].InnerText));
            }

            //Add the ref number to the class
            Astronaute.setRefNumber(int.Parse(refastro.InnerText));

            foreach (XmlNode location in locations)
            {
                mission.newLocation(location["Name"].InnerText, int.Parse(location["POSX"].InnerText), int.Parse(location["POSY"].InnerText), int.Parse(location["Number"].InnerText));
            }

            //Add the ref number to the class
            PineApple.Location.setRefNumber(int.Parse(reflocation.InnerText));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Astronaute astro in mission.getAstronautes())
                checkedListBox1.Items.Add(astro.getName());
            ResetActivityButton.Tag = new Activity();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Previous/Next
        private void button1_Click(object sender, EventArgs e)
        {
            Button a = sender as Button;
            if (a.Text == "Previous")
            {
                if (daySheet > 1)
                {
                    daySheet--;
                    panelActu(daySheet);
                }
            }
            else if (a.Text == "Next")
            {
                if (daySheet < 10)
                {
                    daySheet++;
                    panelActu(daySheet);
                }
            }
        }

        //Affichage du jour aprés click sur le calendrier
        private void button3_Click(object sender, EventArgs e)
        {
            Button A = sender as Button;
            showDay(int.Parse(A.Text));
            updateSelectedTime(int.Parse(A.Text));
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

        //Remplissage des champs lors du clic sur une activité
        private void cmd2_Click(object sender, EventArgs e)
        {
            // Récupération de l'activité liée au bouton
            Activity a = (Activity)(sender as Button).Tag;
            ResetActivityButton.Tag = a;
            //Appel de la fonction de remplissage
            fillActivityPanel(a);
            groupBox1.Text = "Activity";
            SaveActivityButton.Text = "Save Mods";
            creating = false;
        }
        //rempli les champs juste grace au numero de l'activité  tout les cas 
        private void fillActivityPanel(Activity a)
        {
            //Remplissage des champs de l'activité
            numericUpDown1.Value = a.getDay();
            comboBoxStartHour.SelectedIndex = a.getStartDate().getHours();
            comboBoxStartMinutes.SelectedIndex = a.getStartDate().getMinutes() / 10;
            comboBoxEndHour.SelectedIndex = a.getEndDate().getHours();
            comboBoxEndMinutes.SelectedIndex = a.getEndDate().getMinutes() / 10;
            richTextBox1.Text = a.getDescription();
            comboBoxGenericType.SelectedIndex = a.getIndexOfGenericType();
            comboBoxType.SelectedIndex = a.getIndexOfType();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)//On déselectionne tous les astronautes
                checkedListBox1.SetItemChecked(i, false);
            foreach (int numAstro in a.getAstronautes()) //Pour resélectionner les bons
                checkedListBox1.SetItemChecked(numAstro, true);
        }

        private void NewActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "New Activity"; // Changement du nom pour montrer qu'on crée une activité
            comboBoxStartHour.Text = string.Empty;
            comboBoxStartMinutes.Text = string.Empty;
            comboBoxEndHour.Text = string.Empty;
            comboBoxEndMinutes.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            comboBoxGenericType.Text = string.Empty;
            comboBoxType.Text = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)//On déselectionne tous les astronautes
                checkedListBox1.SetItemChecked(i, false);
            ResetActivityButton.Tag = new Activity();
            SaveActivityButton.Text = "Save Activity";
            creating = true;
        }

        private void SaveActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Activity";

            //On crée les dates de début et de fin à partir des combo boxes
            MDate dateDeb = new MDate((int)numericUpDown1.Value, comboBoxStartHour.SelectedIndex, comboBoxStartMinutes.SelectedIndex * 10);
            MDate dateFin = new MDate((int)numericUpDown1.Value, comboBoxEndHour.SelectedIndex, comboBoxEndMinutes.SelectedIndex * 10);

            //On crée la liste des astronautes sélectionnés
            List<int> lastro = new List<int>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    lastro.Add(i);
            }
            if (creating) // Si on est en mode création
            {//Puis on crée l'activité pour l'ajouter à la mission
                bool b = mission.checkAvailability(dateDeb, dateFin, lastro);

                bool explo = false;
                if (comboBoxGenericType.SelectedIndex==1 && comboBoxType.SelectedIndex==4)
                    {
                        explo = true;
                    }
                if (b)
                {  
                    mission.newActivity(richTextBox1.Text,
                                        comboBoxGenericType.SelectedIndex,
                                        comboBoxType.SelectedIndex,
                                        0, // A changer !! Il faut le numéro de la location choisie
                                        lastro,
                                        explo, // extern mission ?
                                        checkBox4.Checked, //spaceVehicle ?
                                        checkBox5.Checked,
                                        dateDeb,
                                        dateFin);
                    mission.WriteActivityXML(); // On inscrit l'activité dans le .XML
                    showDay(dateDeb.getDay());//On rafraichit le tableau journalier
                    label32.Text = dateDeb.getDay().ToString();//On indique la journée affichée en haut du calendrier quotidien
                    SaveActivityButton.Text = "Save Mods";
                }
            }
            else
            {
                bool b = mission.checkAvailabilityForUpdate(dateDeb, dateFin, lastro, ((Activity)ResetActivityButton.Tag).getNumber());
                bool explo = false;
                if (comboBoxGenericType.SelectedIndex == 1 && comboBoxType.SelectedIndex == 4)
                {
                    explo = true;
                }
                if (b)
                {
                    Activity a = new Activity(richTextBox1.Text,
                                              comboBoxGenericType.SelectedIndex,
                                              comboBoxType.SelectedIndex,
                                              0, // A changer !! Il faut le numéro de la location choisie
                                              lastro,
                                              explo, // extern mission ?
                                              checkBox4.Checked, //spaceVehicle ?
                                        checkBox5.Checked,
                                              dateDeb,
                                              dateFin);
                    mission.updateActivity(((Activity)ResetActivityButton.Tag).getNumber(), a);
                    mission.WriteActivityXML(); // On inscrit l'activité dans le .XML
                    showDay(dateDeb.getDay());//On rafraichit le tableau journalier
                    label32.Text = dateDeb.getDay().ToString();//On indique la journée affichée en haut du calendrier quotidien
                    SaveActivityButton.Text = "Save Mods";
                }
            }
        }
        private void ResetActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Activity";
            fillActivityPanel((Activity)ResetActivityButton.Tag);
        }

        //Fonction recherche aprés click sur le bouton search du panel search.
        private void search(object sender, EventArgs e)
        {
            searchPanel.SuspendLayout();
            List<Mission.searchResult> results = new List<Mission.searchResult>(0);
            if (searchGTypeCombo.Enabled == true)
            {
                results = mission.searchByType(int.Parse(((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Key), int.Parse(((KeyValuePair<string, string>)searchTypeCombo.SelectedItem).Key), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
            }
            else
            {
                results = mission.searchByKeyword(textBox4.Text, Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
            }
            searchPanel.Controls.Clear();
            searchPanel.RowCount = results.Count; // <<<-------
            searchPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            searchPanel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//.AddColumns;
            searchPanel.RowStyles.Clear();
            for (int i = 0; i < searchPanel.RowCount; i++)
            {
                RowStyle cs = new RowStyle(SizeType.Percent, 100f / (float)(searchPanel.RowCount));
                searchPanel.RowStyles.Add(cs);
            }
            if (results.Count() != 0)
            {
                int j = 0;
                foreach (Mission.searchResult a in results)
                {
                    Label lb = new Label();
                    lb.Text = a.types;
                    lb.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb.Click += new System.EventHandler(clicOnSearchResult);
                    lb.Tag = a.a;
                    lb.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb, 0, j);
                    Label lb1 = new Label();
                    lb1.Text = a.startDate;
                    lb1.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb1.Click += new System.EventHandler(clicOnSearchResult);
                    lb1.Tag = a.a;
                    lb1.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb1, 1, j);
                    Label lb2 = new Label();
                    lb2.Text = a.endDate;
                    lb2.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb2.Click += new System.EventHandler(clicOnSearchResult);
                    lb2.Tag = a.a;
                    lb2.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb2, 2, j);
                    j++;
                }
            }
            searchPanel.ResumeLayout();

        }
        private void clicOnSearchResult(object sender, EventArgs e)
        {
            fillActivityPanel((sender as Label).Tag as Activity);
        }
        //Lorsque un type generique est selectionné on bloque le champ keyword
        // et on affiche la liste cohérente dans type
        private void searchGTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox a = sender as ComboBox;

            int key = int.Parse(((KeyValuePair<string, string>)a.SelectedItem).Key);

            List<Type> listeGenericType = mission.getActivityTypes();

            int i = 0;
            Dictionary<string, string> T = new Dictionary<string, string>();
            foreach (string t in listeGenericType[key].getTypes())
            {
                T.Add(i.ToString(), t);
                i++;
            }
            searchTypeCombo.DataSource = new BindingSource(T, null);
            searchTypeCombo.DisplayMember = "Value";
            searchTypeCombo.ValueMember = "Key";
            //string value = ((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Value;

        }
        //Lorsque un keyword est tapé, on bloque les champs type et generic type.
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;

            if (t.Text.Length != 0)
            {
                searchGTypeCombo.Enabled = false;
                searchTypeCombo.Enabled = false;
            }
            else
            {
                searchGTypeCombo.Enabled = true;
                searchTypeCombo.Enabled = true;
            }
        }
        //Lorsque un premier sol de la période est selectionné, on débloque la selection du 2eme jour de la période.
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Minimum = (sender as NumericUpDown).Value;
        }

        private void updateDateNow()
        {
            DateTime now = DateTime.Now;
            label29.Text = now.ToString();
            label30.Text = mission.earthToMarsDate(now).getDay().ToString();
        }
        private void updateSelectedTime(int i)
        {
            mission.updateSelectedDay(i);
            label31.Text = mission.marsToEarthDate(mission.getSelectedDay());
            label32.Text = mission.getSelectedDay().ToString();
        }
        private int getDaySheetNumberFromDay(int i)
        {
            int sheet = 0;
            if (i <= 500 && i > 0)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i >= j * 50)
                    {
                        sheet = j++;
                    }
                }
            }
            return sheet;
        }



        private void comboBoxGenericType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox a = sender as ComboBox;

            int key = int.Parse(((KeyValuePair<string, string>)a.SelectedItem).Key);

            List<Type> listeGenericType = mission.getActivityTypes();

            int i = 0;
            Dictionary<string, string> T = new Dictionary<string, string>();
            foreach (string t in listeGenericType[key].getTypes())
            {
                T.Add(i.ToString(), t);
                i++;
            }
            comboBoxType.DataSource = new BindingSource(T, null);
            comboBoxType.DisplayMember = "Value";
            comboBoxType.ValueMember = "Key";
            //string value = ((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Value;

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Activity a = (Activity)ResetActivityButton.Tag;
            mission.deleteActivity(a.getNumber());
            showDay(a.getDay()); // Rafraichit le panel des activités de la journée
        }
        // Add location
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void addLocation(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 formMap = new Form2(this);
            formMap.Show();
        }
        public List<Location> getExploLocations(int periodeStart, int periodEnd,bool v,bool s, bool oe )
        {
            //manque la gestion de la période 
            List<Location> l = new List<Location>(0);

            if (v == true)
            {
                l=mission.getVehiculeLocations(periodeStart, periodEnd);
            }
            if (s == true )
            {
                l=mission.getScaphLocations(periodeStart, periodEnd);
            }
            if(oe == true)
            {
                l=mission.getExternExpLocations(periodeStart, periodEnd);
            }
            return l;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((sender as ComboBox).SelectedIndex);
        }
        
    }   
}
