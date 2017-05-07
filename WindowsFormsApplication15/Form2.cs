using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication15
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string file_name;
        int flag1 = 0;
        int flag2 = 0;
        int sizew = 220;
        int sizeh = 260;
        int width = 0;
        int hight = 0;
        int i = 0;
        int j = 1;
        private void makePicBigger()
        {
            pictureBox1.Image = imageList1.Images[j];

            pictureBox2.Image = imageList1.Images[i];

            pictureBox3.Image = imageList1.Images[j + 1];
            if (width <= 160 || sizeh >= 0)
            {

                pictureBox1.Size = new Size(width, hight);
                pictureBox3.Size = new Size(width, hight);
                pictureBox2.Size = new Size(sizew, sizeh);
                sizeh--;
                sizew--;
                width++;
                hight++;
            }
            else
            {
                flag1 = 1;

                i = j + 2;
                if (i == imageList1.Images.Count - 1) { timer1.Stop(); }
            }
        }

        public void makePicSmaller()
        {
            hight--;
            width--;

            sizeh++;
            sizew++;
            if (flag1 == 1)
            {
                if (width != 0)
                {
                    pictureBox1.Size = new Size(width, hight);
                    pictureBox3.Size = new Size(width, hight);
                    pictureBox2.Size = new Size(sizew, sizeh);
                    pictureBox2.Image = imageList1.Images[i];

                }
                else
                {
                    flag1 = 0;

                    j = i + 1;
                    if (j == imageList1.Images.Count - 1) { timer1.Stop(); }

                }
            }




        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag1 == 0 && flag2 == 0)
            {
                makePicBigger();
            }
            else { makePicSmaller(); }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            string file = string.Format(@"D:\{0}",file_name);
            try
            {
                string[] GalleryArray = System.IO.Directory.GetFiles(file);
                for (int i = 0; i < GalleryArray.Length; i++)
                {
                    if (GalleryArray[i].Contains(".jpg") || GalleryArray[i].Contains(".JPG"))   //test if the file is an image
                    {
                        var tempImage = Image.FromFile(GalleryArray[i]); //Load the image from directory location
                        Bitmap pic = new Bitmap(200, 200);
                        using (Graphics g = Graphics.FromImage(pic))
                        {
                            g.DrawImage(tempImage, new Rectangle(0, 0, pic.Width, pic.Height)); //redraw smaller image
                        }
                        imageList1.Images.Add(pic);    //add new image to imageList
                        tempImage.Dispose();    //after adding to the list, dispose image out of memory
                    }
                }
            }
            catch
            {
                return;
            }
           
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
