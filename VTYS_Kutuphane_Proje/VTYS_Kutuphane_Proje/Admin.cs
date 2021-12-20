using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriTabani_Proje
{
    public partial class Admin : Form
    {
        private NpgsqlConnection baglanti;
        private DataSet dataset;
        private DataTable dataTb;
        private NpgsqlCommand cmd;
        private NpgsqlDataAdapter add;
        private NpgsqlDataReader dr;
        private string SqlIfade;
        private int SonrakiUrunID = 0;
        public Admin()
        {

            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
          
            baglanti = new NpgsqlConnection("Server=localhost; Port = 5432; UserID=postgres; password=14551455; Database=dbkutuphane");
            dataset = new DataSet();

            try
            {
                //Ürünleri Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\" ";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Elektronik Kitapları Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"ElektronikKitap\" INNER JOIN \"Urun\" ON \"ElektronikKitap\".\"UrunID\" = \"Urun\".\"UrunID\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                EKitap_dataGridView.AutoGenerateColumns = false;
                EKitap_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Kitapları Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kitap\" INNER JOIN \"Urun\" ON \"Kitap\".\"UrunID\" = \"Urun\".\"UrunID\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kitap_dataGridView.AutoGenerateColumns = false;
                Kitap_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Yazarları Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM public.\"Yazar\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Yazar_dataGridView.AutoGenerateColumns = false;
                Yazar_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Cevirmenleri Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM public.\"Cevirmen\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Cevirmen_dataGridView.AutoGenerateColumns = false;
                Cevirmen_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Kategorileri Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM public.\"Kategori\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kategori_dataGridView.AutoGenerateColumns = false;
                Kategori_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Dilleri Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM public.\"Dil\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Dil_dataGridView.AutoGenerateColumns = false;
                Dil_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Kitap Türlerini Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM public.\"KitapTuru\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                KitapTuru_dataGridView.AutoGenerateColumns = false;
                KitapTuru_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------


                //Yayin Evlerini Ekliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"YayinEvi\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                YayinEvi_dataGridView.AutoGenerateColumns = false;
                YayinEvi_dataGridView.DataSource = dataTb;
                baglanti.Close();
                //-------------------------------------------------

                //Sonraki Ürün Hesaplanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if(SonrakiUrunID < Convert.ToInt32(dr["UrunID"]))
                    {
                        SonrakiUrunID = Convert.ToInt32(dr["UrunID"]);
                    }
                }
                baglanti.Close();
                SonrakiUrunID++;
                //-------------------------------------------------

                //Combo Boxlar------------------------------------------------------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"YayinEvi\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    YayinEviCb.Items.Add(dr["Adi"]);
                    EKitapYayinEviCb.Items.Add(dr["Adi"]);
                    KitapYayinEviCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kategori\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    KategoriCb.Items.Add(dr["Adi"]);
                    EKitapKategoriCb.Items.Add(dr["Adi"]);
                    KitapKategoriCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"KitapTuru\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    KitapTuruCb.Items.Add(dr["Adi"]);
                    EKitapTuruCb.Items.Add(dr["Adi"]);
                    Kitap_TuruCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"KapakTipi\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    KapakTipiCb.Items.Add(dr["Adi"]);
                    KitapKapakTipiCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Dil\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DilCb.Items.Add(dr["Adi"]);
                    KitapDilCb.Items.Add(dr["Adi"]);
                    EKitapDilCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Yazar\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    YazarAdiCb.Items.Add(dr["Isim"] + " " + dr["Soyisim"]);
                    KitapYazarAdiCb.Items.Add(dr["Isim"] + " " + dr["Soyisim"]);
                    EKitapYazarAdiCb.Items.Add(dr["Isim"] + " " + dr["Soyisim"]);
                }
                baglanti.Close();
                //-------------------------------------------------

                //Combo Boxlar Ayarlanıyor

                YazarCinsiyetiCb.Items.Add("Erkek");
                YazarCinsiyetiCb.Items.Add("Kadın");
                YazarCinsiyet_GuncelleCb.Items.Add("Erkek");
                YazarCinsiyet_GuncelleCb.Items.Add("Kadın");
                CevirmenCinsiyetCb.Items.Add("Erkek");
                CevirmenCinsiyetCb.Items.Add("Kadın");
                CevirmenCinsiyet_GuncelleCb.Items.Add("Erkek");
                CevirmenCinsiyet_GuncelleCb.Items.Add("Kadın");

                //Combo Boxlar Ayarlanıyor
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Ulke\"";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    YazarUlkesiCb.Items.Add(dr["Adi"]);
                    YazarUlke_GuncelleCb.Items.Add(dr["Adi"]);
                }
                baglanti.Close();
                //-------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenilmeye Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Urun_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Urun\" (\"UrunID\",\"Adi\",\"DilID\",\"KapakFotografi\",\"KategoriID\",\"KitapTuruID\",\"Puan\",\"SayfaSayisi\",\"Tanitim\",\"Ucret\",\"YayinEviID\") VALUES(" +
                    "\'" + SonrakiUrunID.ToString() + "\'," + 
                    "\'" + KitapAdiTb.Text + "\'," +
                    "\'" + (DilCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + "Boş" + "\'," +
                    "\'" + (KategoriCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + (KitapTuruCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + PuanTb.Text + "\'," +
                    "\'" + SayfaSayisiNumericUpDown.Value.ToString() + "\'," +
                    "\'" + TanitimTb.Text + "\'," +
                    "\'" + UcretTb.Text + "\'," +
                    "\'" + (YayinEviCb.SelectedIndex + 1).ToString() +
                    "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
                SonrakiUrunID++;
                YayinEviAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                KitapAdiTb.Clear();
                PuanTb.Clear();
                TanitimTb.Clear();
                UcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }
        private void Urun_GuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"Urun\" SET \"Adi\" = \'" + KitapAdiTb.Text +
                    "\', \"DilID\" = \'" + (DilCb.SelectedIndex + 1).ToString() +
                    "\', \"KategoriID\" = \'" + (KategoriCb.SelectedIndex + 1).ToString() +
                    "\', \"KitapTuruID\" = \'" + (KitapTuruCb.SelectedIndex + 1).ToString() +
                    "\', \"Puan\" = \'" + PuanTb.Text +
                    "\', \"SayfaSayisi\" = \'" + SayfaSayisiNumericUpDown.Value.ToString() +
                    "\', \"Tanitim\" = \'" + TanitimTb.Text +
                    "\', \"Ucret\" = \'" + UcretTb.Text +
                    "\', \"YayinEviID\" = \'" + (YayinEviCb.SelectedIndex + 1).ToString() +
                    "\' WHERE \"UrunID\" = \'" + UrunID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\" order by \"UrunID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                KitapAdiTb.Clear();
                PuanTb.Clear();
                TanitimTb.Clear();
                UcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }
        private void YayinEvi_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"YayinEvi\" (\"Adi\") VALUES(\'" + YayinEviAdiTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

                YayinEviAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yayin Evlerini Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"YayinEvi\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                YayinEvi_dataGridView.AutoGenerateColumns = false;
                YayinEvi_dataGridView.DataSource = dataTb;

                YayinEviAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }


        }

        private void YayinEvi_GuncellerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"YayinEvi\" SET \"Adi\" = \'" + YayinEviAdi_GuncelleTb.Text +
                    "\' WHERE \"YayinEviID\" = \'" + YayinEviID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yayin Evlerini Güncelliyoruz
                baglanti.Open();
                SqlIfade = " SELECT * FROM \"YayinEvi\" order by \"YayinEviID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                YayinEvi_dataGridView.AutoGenerateColumns = false;
                YayinEvi_dataGridView.DataSource = dataTb;

                YayinEviID_GuncelleNumericUpDown.Value = 0;
                YayinEviAdi_GuncelleTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Yazar_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Yazar\" (\"Cinsiyet\",\"DogumTarihi\",\"Hayati\",\"Isim\",\"Soyisim\",\"UlkeID\") VALUES(\'" +
                    (YazarCinsiyetiCb.SelectedIndex + 1).ToString() + "\'," + "\'" + YazarDogumTarihi_dateTimePicker.Value.ToString() + "\'," + "\'" +
                    YazarHayatiTb.Text + "\'," + "\'" + YazarIsmiTb.Text + "\'," + "\'" + YazarSoyismiTb.Text + "\'," + "\'" +
                    (YazarUlkesiCb.SelectedIndex + 1).ToString() + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Yazar\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Yazar_dataGridView.AutoGenerateColumns = false;
                Yazar_dataGridView.DataSource = dataTb;

                YazarHayatiTb.Clear();
                YazarIsmiTb.Clear();
                YazarSoyismiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

        }

        private void Yazar_GuncelleBtn_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"Yazar\" SET \"Isim\" = \'" + YazarIsmi_GuncelleTb.Text + 
                    "\', \"Soyisim\" = \'" + YazarSoyismi_GuncelleTb.Text +
                    "\', \"Hayati\" = \'" + YazarHayati_GuncelleTb.Text + 
                    "\', \"DogumTarihi\" = \'" + YazarDogumTarihi_GuncelledateTimePicker.Value.ToString() +
                    "\', \"Cinsiyet\" = \'" + (YazarCinsiyet_GuncelleCb.SelectedIndex + 1).ToString() +
                    "\', \"UlkeID\" = \'" + (YazarUlke_GuncelleCb.SelectedIndex + 1).ToString() +
                    "\' WHERE \"YazarID\" = \'" + YazarID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Yazar\" order by \"YazarID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Yazar_dataGridView.AutoGenerateColumns = false;
                Yazar_dataGridView.DataSource = dataTb;

                YazarSoyismi_GuncelleTb.Clear();
                YazarIsmi_GuncelleTb.Clear();
                YazarHayati_GuncelleTb.Clear();
                YazarID_GuncelleNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Cevirmen_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Cevirmen\" (\"Isim\",\"Soyisim\",\"Cinsiyet\") VALUES(\'" + 
                    CevirmenAdiTb.Text + "\'," + "\'" + CevirmenSoyadiTb.Text + "\'," + "\'" + 
                    (CevirmenCinsiyetCb.SelectedIndex + 1).ToString() + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Çevirmenleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Cevirmen\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Cevirmen_dataGridView.AutoGenerateColumns = false;
                Cevirmen_dataGridView.DataSource = dataTb;

                CevirmenAdiTb.Clear();
                CevirmenSoyadiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Cevirmen_GuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"Cevirmen\" SET \"Isim\" = \'" + CevirmenIsim_GuncelleTb.Text +
                    "\', \"Soyisim\" = \'" + CevirmenSoyisim_GuncelleTb.Text +
                    "\', \"Cinsiyet\" = \'" + (CevirmenCinsiyet_GuncelleCb.SelectedIndex + 1).ToString() +
                    "\' WHERE \"CevirmenID\" = \'" + CevirmenID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Çevirmenleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Cevirmen\" order by \"CevirmenID\" ASC" ;
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Cevirmen_dataGridView.AutoGenerateColumns = false;
                Cevirmen_dataGridView.DataSource = dataTb;

                CevirmenIsim_GuncelleTb.Clear();
                CevirmenSoyisim_GuncelleTb.Clear();
                CevirmenID_GuncelleNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Kategori_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Kategori\" (\"Adi\") VALUES(\'" + KategoriAdiTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Kategorileri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kategori\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kategori_dataGridView.AutoGenerateColumns = false;
                Kategori_dataGridView.DataSource = dataTb;

                KategoriAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Kategori_GuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"Kategori\" SET \"Adi\" = \'" + KategoriAdiGuncelleTb.Text + "\' WHERE \"KategoriID\" = \'" + KategoriGuncelle_numericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Kategorileri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kategori\" order by \"KategoriID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kategori_dataGridView.AutoGenerateColumns = false;
                Kategori_dataGridView.DataSource = dataTb;

                KategoriAdiTb.Clear();
                KategoriGuncelle_numericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

        }

        private void Dil_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Dil\" (\"Adi\") VALUES(\'" + DilAdiTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Dilleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Dil\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Dil_dataGridView.AutoGenerateColumns = false;
                Dil_dataGridView.DataSource = dataTb;

                DilAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Dil_GuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"Dil\" SET \"Adi\" = \'" + DilAdiGuncelleTb.Text + "\' WHERE \"DilID\" = \'" + DilAdiGuncelle_numericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Dilleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Dil\" order by \"DilID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Dil_dataGridView.AutoGenerateColumns = false;
                Dil_dataGridView.DataSource = dataTb;

                DilAdiTb.Clear();
                DilAdiGuncelleTb.Clear();
                DilAdiGuncelle_numericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapTuru_EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"KitapTuru\" (\"Adi\") VALUES(\'" + KitapTuruTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = " SELECT * FROM \"KitapTuru\" order by \"KitapTuruID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                KitapTuru_dataGridView.AutoGenerateColumns = false;
                KitapTuru_dataGridView.DataSource = dataTb;

                KitapTuruTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapTuru_GuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"KitapTuru\" SET \"Adi\" = \'" + KitapTuruID_GuncelleTb.Text +
                    "\' WHERE \"KitapTuruID\" = \'" + KitapTuruID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Kitap türlerini Güncelliyoruz
                baglanti.Open();
                SqlIfade = " SELECT * FROM \"KitapTuru\" order by \"KitapTuruID\" ASC";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                KitapTuru_dataGridView.AutoGenerateColumns = false;
                KitapTuru_dataGridView.DataSource = dataTb;

                KitapTuruID_GuncelleTb.Clear();
                KitapTuruID_GuncelleNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Kategori_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Kategori\"WHERE \"KategoriID\" = \'" + KategoriSil_numericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kategori\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kategori_dataGridView.AutoGenerateColumns = false;
                Kategori_dataGridView.DataSource = dataTb;

                KategoriSil_numericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Urun_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Urun\"WHERE \"UrunID\" = \'" + UrunID_SilNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                KitapAdiTb.Clear();
                PuanTb.Clear();
                TanitimTb.Clear();
                UcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Yazar_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Yazar\"WHERE \"YazarID\" = \'" + YazarID_SilnumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Yazar\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Yazar_dataGridView.AutoGenerateColumns = false;
                Yazar_dataGridView.DataSource = dataTb;

                YazarID_SilnumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Cevirmen_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Cevirmen\"WHERE \"CevirmenID\" = \'" + CevirmenID_SilNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Çevirmenleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Cevirmen\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Cevirmen_dataGridView.AutoGenerateColumns = false;
                Cevirmen_dataGridView.DataSource = dataTb;

                CevirmenID_SilNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Dil_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Dil\"WHERE \"DilID\" = \'" + DilID_SilNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
            try
            {
                //Dilleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Dil\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Dil_dataGridView.AutoGenerateColumns = false;
                Dil_dataGridView.DataSource = dataTb;

                DilID_SilNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapTuru_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"KitapTuru\"WHERE \"KitapTuruID\" = \'" + KitapTuruID_SilNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"KitapTuru\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                KitapTuru_dataGridView.AutoGenerateColumns = false;
                KitapTuru_dataGridView.DataSource = dataTb;

                KitapTuruID_SilNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Kullanici_SilBtn_Click(object sender, EventArgs e)
        {

        }

        private void YayinEvi_SilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"YayinEvi\"WHERE \"KitapTuruID\" = \'" + YayinEviID_SilNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Yazarları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"YayinEvi\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                YayinEvi_dataGridView.AutoGenerateColumns = false;
                YayinEvi_dataGridView.DataSource = dataTb;

                YayinEviID_SilNumericUpDown.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void EKitapEkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Urun\" (\"UrunID\",\"Adi\",\"DilID\",\"KapakFotografi\",\"KategoriID\",\"KitapTuruID\",\"Puan\",\"SayfaSayisi\",\"Tanitim\",\"Ucret\",\"YayinEviID\") VALUES(" +
                    "\'" + SonrakiUrunID.ToString() + "\'," +
                    "\'" + EKitapAdiTb.Text + "\'," +
                    "\'" + (EKitapDilCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + "Boş" + "\'," +
                    "\'" + (EKitapKategoriCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + (EKitapTuruCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + EKitapPuanTb.Text + "\'," +
                    "\'" + EKitapSayfaSayisi_NumericUpDown.Value.ToString() + "\'," +
                    "\'" + EKitapTanitimTb.Text + "\'," +
                    "\'" + EKitapUcretTb.Text + "\'," +
                    "\'" + (EKitapYayinEviCb.SelectedIndex + 1).ToString() +
                    "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
                SonrakiUrunID++;
                YayinEviAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"ElektronikKitap\" (\"UrunID\",\"Boyut\",\"IndirmeSayisi\") VALUES(" +
                    "\'" + SonrakiUrunID.ToString() + "\'," +
                    "\'" + EKitapBoyutTb.Text+ "\'," +
                    "\'" + EKitapIndirmeSayisiTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
                YayinEviAdiTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                KitapAdiTb.Clear();
                PuanTb.Clear();
                TanitimTb.Clear();
                UcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Elektronik Kitapları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"ElektronikKitap\" INNER JOIN \"Urun\" ON \"ElektronikKitap\".\"UrunID\" = \"Urun\".\"UrunID\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                EKitap_dataGridView.AutoGenerateColumns = false;
                EKitap_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void EKitapSilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"ElektronikKitap\"WHERE \"YazarID\" = \'" + EKitapIDSil_NumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
                KitapAdiTb.Clear();
                PuanTb.Clear();
                TanitimTb.Clear();
                UcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"ElektronikKitap\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                EKitap_dataGridView.AutoGenerateColumns = false;
                EKitap_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void EKitapGuncelleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "UPDATE \"ElektronikKitap\" SET \"Boyut\" = \'" + CevirmenIsim_GuncelleTb.Text +
                    "\', \"IndirmeSayisi\" = \'" + CevirmenSoyisim_GuncelleTb.Text +
                    "\' WHERE \"UrunID\" = \'" + CevirmenID_GuncelleNumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapEkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Urun\" (\"UrunID\",\"Adi\",\"DilID\",\"KapakFotografi\",\"KategoriID\",\"KitapTuruID\",\"Puan\",\"SayfaSayisi\",\"Tanitim\",\"Ucret\",\"YayinEviID\") VALUES(" +
                    "\'" + SonrakiUrunID.ToString() + "\'," +
                    "\'" + Kitap_AdiTb.Text + "\'," +
                    "\'" + (KitapDilCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + "Boş" + "\'," +
                    "\'" + (KitapKategoriCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + (Kitap_TuruCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + KitapPuanTb.Text + "\'," +
                    "\'" + KitapSayfaSayisi_NumericUpDown.Value.ToString() + "\'," +
                    "\'" + KitapTanitimTb.Text + "\'," +
                    "\'" + KitapUcretTb.Text + "\'," +
                    "\'" + (KitapYayinEviCb.SelectedIndex + 1).ToString() +
                    "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
                Kitap_AdiTb.Clear();
                KitapPuanTb.Clear();
                KitapTanitimTb.Clear();
                KitapUcretTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                baglanti.Open();
                SqlIfade = "INSERT INTO \"Kitap\" (\"UrunID\",\"BasimTarihi\",\"BaskiSayisi\",\"KapakTipiID\",\"StokMiktari\") VALUES(" +
                    "\'" + SonrakiUrunID.ToString() + "\'," +
                    "\'" + KitapBasimTarihi_dateTimePicker.Value.ToString() + "\'," +
                    "\'" + KitapBaskiSayisiTb.Text + "\'," +
                    "\'" + (KitapKapakTipiCb.SelectedIndex + 1).ToString() + "\'," +
                    "\'" + KitapStokMiktariTb.Text + "\')";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
                SonrakiUrunID++;
                KitapBaskiSayisiTb.Clear();
                KitapStokMiktariTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Elektronik Kitapları Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kitap\" INNER JOIN \"Urun\" ON \"Kitap\".\"UrunID\" = \"Urun\".\"UrunID\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kitap_dataGridView.AutoGenerateColumns = false;
                Kitap_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapSilBtn_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlIfade = "DELETE FROM \"Kitap\"WHERE \"UrunID\" = \'" + KitapIDSil_NumericUpDown.Value.ToString() + "\'";
                cmd = new NpgsqlCommand(SqlIfade, baglanti);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Urun\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
            
            try
            {
                //Ürünleri Güncelliyoruz
                baglanti.Open();
                SqlIfade = "SELECT * FROM \"Kitap\"";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                EKitap_dataGridView.AutoGenerateColumns = false;
                EKitap_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void KitapGuncelleBtn_Click(object sender, EventArgs e)
        {

        }

        private void UrunTabPage_Click(object sender, EventArgs e)
        {

        }

        private void Kategori_AraBtn_Click(object sender, EventArgs e)
        {

            try
            {
                //Kategorileri Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"Kategori\"WHERE \"KategoriID\" = \'" + numericUpDown4.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Kategori_dataGridView.AutoGenerateColumns = false;
                Kategori_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Ürünleri Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"Urun\"WHERE \"UrunID\" = \'" + numericUpDown2.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Urun_dataGridView.AutoGenerateColumns = false;
                Urun_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Yazarlari Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"Yazar\"WHERE \"YazarID\" = \'" + numericUpDown1.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Yazar_dataGridView.AutoGenerateColumns = false;
                Yazar_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //cevirmen Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"Cevirmen\"WHERE \"CevirmenID\" = \'" + numericUpDown3.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Cevirmen_dataGridView.AutoGenerateColumns = false;
                Cevirmen_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Dil Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"Dil\"WHERE \"DilID\" = \'" + numericUpDown5.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                Dil_dataGridView.AutoGenerateColumns = false;
                Dil_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //KitapTuru Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"KitapTuru\"WHERE \"KitapTuruID\" = \'" + numericUpDown6.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                KitapTuru_dataGridView.AutoGenerateColumns = false;
                KitapTuru_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //YayinEvi Arıyoruz                
                baglanti.Open();
                SqlIfade = "SELECT * FROM  \"YayinEvi\"WHERE \"YayinEviID\" = \'" + numericUpDown7.Value.ToString() + "\'";
                add = new NpgsqlDataAdapter(SqlIfade, baglanti);
                dataTb = new DataTable();
                add.Fill(dataTb);
                YayinEvi_dataGridView.AutoGenerateColumns = false;
                YayinEvi_dataGridView.DataSource = dataTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Beklenmedik Bir Durum Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }


    }

    }


