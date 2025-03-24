using Student_Information_System;

namespace AdvancedProgramming_Vize
{
    public partial class Form1 : Form
    {
        List<string> Ankara = new List<string> { "Beypazar�", "�ankaya", "G�lba��", "Mamak", "Merkez", "Polatl�", "Sincan" };

        List<string> Eski�ehir = new List<string> { "Alpu", "�ifteler", "Merkez", "Odunpazar�", "Sar�cakaya", "Seyitgazi", "Sivrihisar" };

        List<string> �stanbul = new List<string> { "Bak�rk�y", "Be�ikta�", "Beylikd�z�", "Beyo�lu", "Ey�p", "Kad�k�y", "�i�li", "�sk�dar", "Zeytinburnu" };

        List<string> �zmir = new List<string> { "Bornova", "�e�me", "Dikili", "Fo�a", "Kar��yaka", "Konak", "Torbal�", "Urla" };

        ImageList smallImageList = new ImageList();
        ImageList largeImageList = new ImageList();

        private void LoadIcons()
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string iconFolderPath = Path.Combine(projectDirectory, "Icons");

            if (!Directory.Exists(iconFolderPath))
            {
                MessageBox.Show("Icons klas�r� bulunamad�!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            smallImageList.ImageSize = new Size(16, 16);
            largeImageList.ImageSize = new Size(32, 32);

            smallImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person1-small.jpg")));
            smallImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person2-small.jpg")));
            smallImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person3-small.jpg")));
            smallImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person4-small.png")));

            largeImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person1-large.jpg")));
            largeImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person2-large.jpg")));
            largeImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person3-large.jpg")));
            largeImageList.Images.Add(Image.FromFile(Path.Combine(iconFolderPath, "person4-large.png")));

            listViewStudentList.SmallImageList = smallImageList;
            listViewStudentList.LargeImageList = largeImageList;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeListView();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;
            LoadIcons();
        }

        private void InitializeListView()
        {
            listViewStudentList.View = View.Details;
            listViewStudentList.Columns.Add("Ad", 100);
            listViewStudentList.Columns.Add("Soyad", 100);
            listViewStudentList.Columns.Add("�ehir", 100);
            listViewStudentList.Columns.Add("�l�e", 100);
            listViewStudentList.Columns.Add("Cinsiyet", 100);
            listViewStudentList.Columns.Add("Sinema", 70);
            listViewStudentList.Columns.Add("Kitap", 70);
            listViewStudentList.Columns.Add("M�zik", 70);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxDistrict.Items.Clear();
            string selectedCity = comboBoxCity.SelectedItem?.ToString().ToLower();

            if (string.IsNullOrEmpty(selectedCity)) return;

            if (selectedCity == "ankara")
            {
                listBoxDistrict.Items.AddRange(Ankara.ToArray());
            }
            else if (selectedCity == "istanbul")
            {
                listBoxDistrict.Items.AddRange(�stanbul.ToArray());
            }
            else if (selectedCity == "eski�ehir")
            {
                listBoxDistrict.Items.AddRange(Eski�ehir.ToArray());
            }
            else if (selectedCity == "izmir")
            {
                listBoxDistrict.Items.AddRange(�zmir.ToArray());
            }
        }

        private void comboBoxListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxListView.SelectedItem.ToString() == "Large Icon")
            {
                listViewStudentList.View = View.LargeIcon;
            }
            else if (comboBoxListView.SelectedItem.ToString() == "Small Icon")
            {
                listViewStudentList.View = View.SmallIcon;
            }

            switch (comboBoxListView.SelectedItem.ToString())
            {
                case "Details":
                    listViewStudentList.View = View.Details;
                    break;
                case "Large Icon":
                    listViewStudentList.View = View.LargeIcon;
                    break;
                case "Small Icon":
                    listViewStudentList.View = View.SmallIcon;
                    break;
                case "List":
                    listViewStudentList.View = View.List;
                    break;
                case "Tile":
                    listViewStudentList.View = View.Tile;
                    break;
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxListView.SelectedItem.ToString() == "Large Icon")
            {
                listViewStudentList.View = View.LargeIcon;
            }
            else if (comboBoxListView.SelectedItem.ToString() == "Small Icon")
            {
                listViewStudentList.View = View.SmallIcon;
            }

            switch (comboBoxListView.SelectedItem.ToString())
            {
                case "Details":
                    listViewStudentList.View = View.Details;
                    break;
                case "Large Icon":
                    listViewStudentList.View = View.LargeIcon;
                    break;
                case "Small Icon":
                    listViewStudentList.View = View.SmallIcon;
                    break;
                case "List":
                    listViewStudentList.View = View.List;
                    break;
                case "Tile":
                    listViewStudentList.View = View.Tile;
                    break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBoxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text) || string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                comboBoxCity.SelectedIndex == -1 || listBoxDistrict.SelectedIndex == -1)
            {
                MessageBox.Show("L�tfen t�m bilgileri doldurunuz.", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gender = radioButton1.Checked ? "Erkek" : "Kad�n";
            int selectedIconIndex = radioButton3.Checked ? 0 : radioButton4.Checked ? 1 : radioButton5.Checked ? 2 : 3;
            string hobbyCinema = checkBoxCinema.Checked ? "Evet" : "Hay�r";
            string hobbyBook = checkBoxBook.Checked ? "Evet" : "Hay�r";
            string hobbyMusic = checkBoxMusic.Checked ? "Evet" : "Hay�r";

            ListViewItem item = new ListViewItem(textBoxName.Text);
            item.ImageIndex = selectedIconIndex;

            item.SubItems.Add(textBoxSurname.Text);
            item.SubItems.Add(comboBoxCity.SelectedItem.ToString());
            item.SubItems.Add(listBoxDistrict.SelectedItem.ToString());
            item.SubItems.Add(gender);
            item.SubItems.Add(hobbyCinema);
            item.SubItems.Add(hobbyBook);
            item.SubItems.Add(hobbyMusic);

            listViewStudentList.Items.Add(item);

            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBoxCity.SelectedIndex = -1;
            listBoxDistrict.Items.Clear();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void bilgileriTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBoxCity.SelectedIndex = -1;
            listBoxDistrict.Items.Clear();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;
        }

        private void listeyiTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewStudentList.Items.Clear();
        }

        private void ��k��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hakk�ndaCtrlHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            listViewStudentList.Items.Clear();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }
    }
}
