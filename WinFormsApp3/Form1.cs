using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        string fileName;
        
        public Form1()
        {
            InitializeComponent();
        }
        Image img;
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                img = Image.FromFile(pic);
                pictureBox1.Image = img;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog () { Filter = "JPEG|*.jpg",ValidateNames = true, Multiselect = false})
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    pictureBox1.Image = Image.FromFile(fileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileName) && textBox1.Text !="")
            {
                if (File.Exists(Path.Combine(Application.StartupPath, @"Images\", textBox1.Text)))
                {
                    DialogResult dr = MessageBox.Show("Rewrite?", "File exists", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        File.Copy(fileName, Path.Combine(Application.StartupPath, @"Images\", textBox1.Text), true);
                        label1.Text = "Image saved successfully";
                        pictureBox1.Image = null;
                    }
                    else if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    File.Copy(fileName, Path.Combine(Application.StartupPath, @"Images\", textBox1.Text), true);
                    label1.Text = "Image saved successfully";
                    pictureBox1.Image = null;
                }
            }
            else
            {
                label1.Text = "Fill all the data";
                pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"oops.png"));
            }
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, @"Images\", textBox2.Text)))
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\", textBox2.Text));
                label1.Text = "Image found";
            }
            else
            {
                label1.Text = "Image not exists";
                pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"oops.png"));
            }
            textBox2.Text = "";
        }
    }
}
