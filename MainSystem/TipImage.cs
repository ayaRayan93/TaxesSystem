﻿using MySql.Data.MySqlClient;
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
    public partial class TipImage : Form
    {
        string Data_ID ;
        string type = "";
        MySqlConnection dbconnection;
        byte[] bin;
        int bufferSize = 1024;
        MemoryStream ms = null;
        Image image = null;
        public TipImage(string Data_ID)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            bin = new byte[bufferSize];
            image = null;
            this.Data_ID = Data_ID;
        }
        public TipImage(string Data_ID, String Type)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            bin = new byte[bufferSize];
            image = null;
            this.Data_ID = Data_ID;
            type = Type;
        }
        private void TipImage_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Properties.Resources.Back_32;
                if (type != "")
                {
                    Thread t1 = new Thread(displayImageType);
                    t1.Start();
                }
                else
                {
                    Thread t1 = new Thread(displayImage);
                    t1.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        public void displayImageType()
        {
            try
            {
                dbconnection.Open();
                string query = "";
                if (type == "بند")
                {
                    query = "select Photo from data_photo where Data_ID=" + Data_ID;
                }
                else if (type == "طقم")
                {
                    query = "select Photo from set_photo where Set_ID=" + Data_ID;
                }
                else if (type == "عرض")
                {
                    query = "select Photo from offer_photo where Offer_ID=" + Data_ID;
                }
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
    }
}
