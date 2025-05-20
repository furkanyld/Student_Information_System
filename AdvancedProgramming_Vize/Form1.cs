using Student_Information_System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AdvancedProgramming_Vize
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=FURKAN;Database=VT_OGRENCILER;Trusted_Connection=True;TrustServerCertificate=True;";

        List<string> Ankara = new List<string> { "Beypazarý", "Çankaya", "Gölbaþý", "Mamak", "Merkez", "Polatlý", "Sincan" };

        List<string> Eskiþehir = new List<string> { "Alpu", "Çifteler", "Merkez", "Odunpazarý", "Sarýcakaya", "Seyitgazi", "Sivrihisar" };

        List<string> Ýstanbul = new List<string> { "Bakýrköy", "Beþiktaþ", "Beylikdüzü", "Beyoðlu", "Eyüp", "Kadýköy", "Þiþli", "Üsküdar", "Zeytinburnu" };

        List<string> Ýzmir = new List<string> { "Bornova", "Çeþme", "Dikili", "Foça", "Karþýyaka", "Konak", "Torbalý", "Urla" };

        ImageList smallImageList = new ImageList();
        ImageList largeImageList = new ImageList();

        private void LoadIcons()
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string iconFolderPath = Path.Combine(projectDirectory, "Icons");

            if (!Directory.Exists(iconFolderPath))
            {
                MessageBox.Show("Icons klasörü bulunamadý!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadDataFromDatabase()
        {
            listViewStudentList.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ogrenci";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string tc = reader["tc"].ToString();
                        string ad = reader["adi"].ToString();
                        string soyad = reader["soyadi"].ToString();
                        string il = reader["ili"].ToString();
                        string ilce = reader["ilcesi"].ToString();
                        string cinsiyet = reader["cinsiyet"].ToString();
                        bool sinema = (bool)reader["sinema"];
                        bool kitap = (bool)reader["kitap"];
                        bool muzik = (bool)reader["muzik"];
                        int ikon = Convert.ToInt32(reader["ikon"]);

                        ListViewItem item = new ListViewItem(tc);
                        item.ImageIndex = Math.Max(0, Math.Min(3, ikon - 1));
                        item.SubItems.Add(ad);
                        item.SubItems.Add(soyad);
                        item.SubItems.Add(il);
                        item.SubItems.Add(ilce);
                        item.SubItems.Add(cinsiyet);
                        item.SubItems.Add(muzik ? "Evet" : "Hayýr");
                        item.SubItems.Add(kitap ? "Evet" : "Hayýr");
                        item.SubItems.Add(sinema ? "Evet" : "Hayýr");

                        listViewStudentList.Items.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yükleme hatasý: " + ex.Message);
                }
            }
        }

        private void listViewStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudentList.SelectedItems.Count == 0)
                return;

            var item = listViewStudentList.SelectedItems[0];

            textBoxTC.Text = item.SubItems[0].Text;
            textBoxName.Text = item.SubItems[1].Text;
            textBoxSurname.Text = item.SubItems[2].Text;
            comboBoxCity.SelectedItem = item.SubItems[3].Text;

            comboBoxCity_SelectedIndexChanged(null, null);
            listBoxDistrict.SelectedItem = item.SubItems[4].Text;

            string gender = item.SubItems[5].Text;
            radioButton1.Checked = (gender == "Erkek");
            radioButton2.Checked = (gender == "Kadýn");

            checkBoxMusic.Checked = (item.SubItems[8].Text == "Evet");
            checkBoxBook.Checked = (item.SubItems[7].Text == "Evet");
            checkBoxCinema.Checked = (item.SubItems[6].Text == "Evet");

            switch (item.ImageIndex)
            {
                case 0: radioButton3.Checked = true; break;
                case 1: radioButton4.Checked = true; break;
                case 2: radioButton5.Checked = true; break;
                case 3: radioButton6.Checked = true; break;
                default: radioButton3.Checked = true; break;
            }

            textBoxTC.ReadOnly = true; // TC güncellenmiyor
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
            LoadDataFromDatabase();
        }

        private void InitializeListView()
        {
            listViewStudentList.View = View.Details;
            listViewStudentList.Columns.Add("TC", 120);
            listViewStudentList.Columns.Add("Ad", 100);
            listViewStudentList.Columns.Add("Soyad", 100);
            listViewStudentList.Columns.Add("Þehir", 100);
            listViewStudentList.Columns.Add("Ýlçe", 100);
            listViewStudentList.Columns.Add("Cinsiyet", 100);
            listViewStudentList.Columns.Add("Müzik", 70);
            listViewStudentList.Columns.Add("Kitap", 70);
            listViewStudentList.Columns.Add("Sinema", 70);
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
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
                listBoxDistrict.Items.AddRange(Ýstanbul.ToArray());
            }
            else if (selectedCity == "eskiþehir")
            {
                listBoxDistrict.Items.AddRange(Eskiþehir.ToArray());
            }
            else if (selectedCity == "izmir")
            {
                listBoxDistrict.Items.AddRange(Ýzmir.ToArray());
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        private void comboBoxListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxListView.SelectedItem.ToString();

            switch (selected)
            {
                case "Detay (Details)":
                    listViewStudentList.View = View.Details;
                    break;
                case "Büyük Ýkon (Large Icon)":
                    listViewStudentList.View = View.LargeIcon;
                    listViewStudentList.LargeImageList = largeImageList; 
                    break;
                case "Küçük Ýkon (Small Icon)":
                    listViewStudentList.View = View.SmallIcon;
                    listViewStudentList.SmallImageList = smallImageList;
                    break;
                case "Liste (List)":
                    listViewStudentList.View = View.List;
                    break;
                case "Döþeme (Tile)":
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
                MessageBox.Show("Lütfen tüm bilgileri doldurunuz.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gender = radioButton1.Checked ? "Erkek" : "Kadýn";
            int selectedIconIndex = radioButton3.Checked ? 1 : radioButton4.Checked ? 2 : radioButton5.Checked ? 3 : 4;
            bool hobbyCinema = checkBoxCinema.Checked;
            bool hobbyBook = checkBoxBook.Checked;
            bool hobbyMusic = checkBoxMusic.Checked;

            string tc = textBoxTC.Text.Trim();

            ListViewItem item = new ListViewItem(tc);
            item.ImageIndex = selectedIconIndex - 1;
            item.SubItems.Add(textBoxName.Text);
            item.SubItems.Add(textBoxSurname.Text);
            item.SubItems.Add(comboBoxCity.SelectedItem.ToString());
            item.SubItems.Add(listBoxDistrict.SelectedItem.ToString());
            item.SubItems.Add(gender);
            item.SubItems.Add(hobbyMusic ? "Evet" : "Hayýr");
            item.SubItems.Add(hobbyBook ? "Evet" : "Hayýr");
            item.SubItems.Add(hobbyCinema ? "Evet" : "Hayýr");

            listViewStudentList.Items.Add(item);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ogrenci (tc, adi, soyadi, ili, ilcesi, cinsiyet, ikon, sinema, kitap, muzik) " +
                               "VALUES (@tc, @adi, @soyadi, @ili, @ilcesi, @cinsiyet, @ikon, @sinema, @kitap, @muzik)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tc", tc);
                    cmd.Parameters.AddWithValue("@adi", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@soyadi", textBoxSurname.Text);
                    cmd.Parameters.AddWithValue("@ili", comboBoxCity.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ilcesi", listBoxDistrict.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@cinsiyet", gender);
                    cmd.Parameters.AddWithValue("@ikon", selectedIconIndex);
                    cmd.Parameters.AddWithValue("@muzik", hobbyMusic);
                    cmd.Parameters.AddWithValue("@kitap", hobbyBook);
                    cmd.Parameters.AddWithValue("@sinema", hobbyCinema);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Öðrenci baþarýyla eklendi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }

            textBoxTC.Clear();
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBoxTC.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBoxCity.SelectedIndex = -1;
            listBoxDistrict.Items.Clear();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;

            textBoxTC.ReadOnly = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void bilgileriTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxTC.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBoxCity.SelectedIndex = -1;
            listBoxDistrict.Items.Clear();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;

            textBoxTC.ReadOnly = false;
        }

        private void listeyiTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewStudentList.Items.Clear();
        }

        private void çýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hakkýndaCtrlHToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewStudentList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir öðrenci seçiniz.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listViewStudentList.SelectedItems[0];
            string tc = selectedItem.Text;

            var result = MessageBox.Show($"Seçilen öðrenciyi silmek istediðinize emin misiniz?\nTC: {tc}", "Silme Onayý",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ogrenci WHERE tc = @tc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tc", tc);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Öðrenci baþarýyla silindi.");
                        LoadDataFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Silme hatasý: " + ex.Message);
                    }
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listViewStudentList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen güncellemek için bir öðrenci seçiniz.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tc = textBoxTC.Text.Trim();
            string ad = textBoxName.Text.Trim();
            string soyad = textBoxSurname.Text.Trim();
            string sehir = comboBoxCity.SelectedItem?.ToString() ?? "";
            string ilce = listBoxDistrict.SelectedItem?.ToString() ?? "";
            string cinsiyet = radioButton1.Checked ? "Erkek" : "Kadýn";
            int ikon = radioButton3.Checked ? 1 : radioButton4.Checked ? 2 : radioButton5.Checked ? 3 : 4;
            bool sinema = checkBoxCinema.Checked;
            bool kitap = checkBoxBook.Checked;
            bool muzik = checkBoxMusic.Checked;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE ogrenci SET adi = @adi, soyadi = @soyadi, ili = @ili, ilcesi = @ilcesi, " +
                               "cinsiyet = @cinsiyet, ikon = @ikon, sinema = @sinema, kitap = @kitap, muzik = @muzik " +
                               "WHERE tc = @tc";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tc", tc);
                cmd.Parameters.AddWithValue("@adi", ad);
                cmd.Parameters.AddWithValue("@soyadi", soyad);
                cmd.Parameters.AddWithValue("@ili", sehir);
                cmd.Parameters.AddWithValue("@ilcesi", ilce);
                cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                cmd.Parameters.AddWithValue("@ikon", ikon);
                cmd.Parameters.AddWithValue("@sinema", sinema);
                cmd.Parameters.AddWithValue("@kitap", kitap);
                cmd.Parameters.AddWithValue("@muzik", muzik);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Güncelleme baþarýlý.");
                    LoadDataFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme hatasý: " + ex.Message);
                }
            }
            textBoxTC.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBoxCity.SelectedIndex = -1;
            listBoxDistrict.Items.Clear();
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            checkBoxCinema.Checked = false;
            checkBoxBook.Checked = false;
            checkBoxMusic.Checked = false;
            textBoxTC.ReadOnly = false;
        }

        private void buttonListAll_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }
    }
}
