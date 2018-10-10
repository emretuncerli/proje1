using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    class Class1
    {
        MySqlConnection baglanti;

        public bool baglanti_kontrol()
        {
            try
            {
                baglanti = new MySqlConnection("Server=localhost;Database=kayıtlar;Uid=root;Pwd='123456et';Encrypt=false;AllowUserVariables=True;UseCompression=True");
                MySqlCommand command = baglanti.CreateCommand();
                command.CommandText = "INSERT INTO okul (isim,soyad,sınıf,yas,dersler) VALUES ('emre','tuncerli','3','22','Fizik')";
                //command.CommandText = "update okul set isim='emree' where isim='emre'";
                //command.CommandText = "delete from okul where isim='emre'";
                //command.CommandText = "select * from okul";
                baglanti.Open();
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < 100000; i++)
                {
                    command.ExecuteNonQuery();
                }
                sw.Stop();
                MessageBox.Show("100000 kayıt için geçen Veri Ekleme süresi: "+sw.ElapsedMilliseconds.ToString()+ " milisaniyedir.");
                return true;
            }

            catch (Exception)
            {
                return false;
                //Veritabanına bağlanamazsa "false" değeri dönecek
            }
        }
    }
}
