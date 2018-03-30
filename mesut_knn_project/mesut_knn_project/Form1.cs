using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace mesut_knn_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<knn> listem = new List<knn>();
        List<knn> listehesapli = new List<knn>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\veri\\veri.xlsx; Extended Properties='Excel 12.0 Xml;HDR=YES'"); 
                DataTable tablo = new DataTable();
                xlsxbaglanti.Open(); 
                tablo.Clear(); 
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [veri$]", xlsxbaglanti);
                da.Fill(tablo);
                dataGridView1.DataSource = tablo;  
                xlsxbaglanti.Close();
              
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("okuma işlemi yapılamadı."+ex.ToString());
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    listBox1.Items.Add("x:" + row.Cells[0].Value + " y:" + row.Cells[1].Value);
                    knn k = new knn();
                    k.x = Convert.ToInt32(row.Cells[0].Value.ToString());
                    k.y = Convert.ToInt32(row.Cells[1].Value.ToString());
                    k.sinif = row.Cells[2].Value.ToString();
                    listem.Add(k);
                }
                /* hesaplama yapılıyor */
                listBox1.Items.Add("---------------");
                foreach (var item in listem)
                {
                    double xtoplam = Convert.ToInt32(textBox1.Text) - item.x;
                    double ytoplam = Convert.ToInt32(textBox2.Text) - item.y;
                    double mesafe = Math.Sqrt(Math.Pow(xtoplam, 2) + Math.Pow(ytoplam, 2));
                    listBox1.Items.Add("x:" + item.x + " y:" + item.y + " sınıf:" + item.sinif + " mesafe " + mesafe);
                    knn lis = new knn();
                    lis.x = item.x;
                    lis.y = item.y;
                    lis.sinif = item.sinif;
                    lis.mesafe = mesafe;
                    listehesapli.Add(lis);
                }

                listBox1.Items.Add("----------------------- ");
                listBox1.Items.Add(" K Komşulara bakılıyor ");
                /* komşulara bakılıyor */

                var sonuc = (from t in listehesapli
                             orderby t.mesafe
                             select t).Take(Convert.ToInt32(numericUpDown1.Value.ToString()));

                foreach (var item in sonuc)
                {
                    listBox1.Items.Add("x:" + item.x + " y:" + item.y + " Sınıf:" + item.sinif + " mesafe:" + item.mesafe);
                }
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void temizle_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listehesapli.Clear();
            listem.Clear();
        }
    }
}
