using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class DisplayImage : Form
    {
        private MySqlConnection dbconnection, dbconnection1;
        int count = 0;
        struct ImageLoaded
        {
          public  int id { get; set; }
          public byte[] data { get; set; }
        }
        List<int> imagesID;
        List<ImageLoaded> myImages;
        int bufferSize = 1024;
        byte[] bin;
        long retval = 0;
        long startIndex = 0;
        bool flag = false;
        MemoryStream ms = null;
        Image image = null;
        private int oldX;
        private int oldY;
        private int defX;
        private int defY;
        private PictureBox thisPB;
        string Data_ID;
        public DisplayImage()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                imagesID = new List<int>();
                myImages = new List<ImageLoaded>();
                bin = new byte[bufferSize];
                Cursor cur = new Cursor(Properties.Resources.img_431660.Handle);
                panel1.Cursor = cur;
                defX = pictureBox1.Location.X;
                defY = pictureBox1.Location.Y;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public DisplayImage(string Data_ID)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            bin = new byte[bufferSize];
            image = null;
            this.Data_ID = Data_ID;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select DataDetails_ID from data_details";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    imagesID.Add(Convert.ToInt16(dr[0].ToString()));
                }
                dr.Close();
                
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Properties.Resources.animated_gif_loading;
                Thread t1 = new Thread(displayImage);
                t1.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        public void displayImage()
        {
            try
            {
                dbconnection.Open();

                string query = "select Photo from data_photo where Data_ID=" + Data_ID + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);

                if (com.ExecuteScalar() != null)
                {
                    bin = (byte[])com.ExecuteScalar();

                    ms = new MemoryStream(bin);
                    image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Properties.Resources.Delete_52px;
                }

            }
            catch
            {
                // MessageBox.Show(ex.Message);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Properties.Resources.Delete_52px;

            }
            dbconnection.Close();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (count >= imagesID.Count-1)
                    count = 0;
                else
                    count++;

                trackBar1.Value = 0;
                pictureBox1.Location = new Point(defX, defY);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Properties.Resources.animated_gif_loading;
                Thread t1 = new Thread(displayImage);
                t1.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnPreviews_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (count <= 0)
                    count = imagesID.Count-1;
                else
                    count--;

                trackBar1.Value = 0;
                pictureBox1.Location = new Point(defX, defY);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Properties.Resources.animated_gif_loading;
                Thread t1 = new Thread(displayImage);
                t1.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (trackBar1.Value > 0)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.Image = zoom(image, new Size(trackBar1.Value, trackBar1.Value));
                }
                else if (trackBar1.Value == 0)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Image zoom(Image img,Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
           
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Cursor cur = new Cursor(Properties.Resources.img_520108.Handle);
                pictureBox1.Cursor = cur;
                flag = true;
                oldX = e.X;
                oldY = e.Y;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Cursor cur = new Cursor(Properties.Resources.img_431660.Handle);
                pictureBox1.Cursor = cur;
                flag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (flag)
                {
                    thisPB = (PictureBox)sender;
                    thisPB.Location = new Point(thisPB.Location.X - (oldX - e.X), thisPB.Location.Y - (oldY - e.Y));

                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor cur = new Cursor(Properties.Resources.img_431660.Handle);
                pictureBox1.Cursor = cur;
                flag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void displayImage2()
        {
            try
            {          
                dbconnection1.Open();
                if (Exist(imagesID[count]) == null)
                {
                    string query = "select Photo from data_details where DataDetails_ID=" + imagesID[count];
                    MySqlCommand com = new MySqlCommand(query, dbconnection1);

                    bin = (byte[])com.ExecuteScalar();

                    ms = new MemoryStream(bin);
                    image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                    ImageLoaded image1 = new ImageLoaded() { id = imagesID[count], data = bin };
                    myImages.Add(image1);
                }
                else
                {
                    ms = new MemoryStream(Exist(imagesID[count]));
                    image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection1.Close();
        }
        public byte[] Exist(int Id)
        {
            foreach (ImageLoaded item in myImages)
            {
                if (item.id == Id)
                    return item.data;
            }
            return null;
        }
       
    }
  
}
