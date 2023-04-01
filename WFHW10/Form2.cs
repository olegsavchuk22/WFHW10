using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FHW10
{
    public partial class Form2 : Form
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        List<string> fields = new List<string>();
        List<string> bwdPath = new List<string>();
        public Form2()
        {
            InitializeComponent();
            foreach (var drive in drives)
            {
                fields.Add(drive.Name);
                listBox1.Items.Add(drive.Name);
            }
            backwardToolStripMenuItem.Enabled = false;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(fields[listBox1.SelectedIndex]))
                {
                    try
                    {
                        bwdPath.Add(textBox1.Text);
                        textBox1.Text = fields[listBox1.SelectedIndex];
                        fields.Clear();
                        listBox1.Items.Clear();
                        foreach (string item in Directory.GetDirectories(textBox1.Text))
                        {
                            DirectoryInfo file = new DirectoryInfo(item);
                            fields.Add(file.FullName);
                            listBox1.Items.Add(file.Name);
                        }
                        listBox2.Items.Clear();
                        foreach (string item in Directory.GetFileSystemEntries(textBox1.Text))
                        {
                            FileInfo file = new FileInfo(item);
                            listBox2.Items.Add(file.Name);
                        }
                        backwardToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception exept)
                    {
                        MessageBox.Show(exept.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Невірний шлях", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exeption)
            {

                MessageBox.Show(exeption.ToString());
            }
        }

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bwdPath.Count == 1 || bwdPath.Count == 0)
            {
                fields.Clear();
                listBox1.Items.Clear();
                textBox1.Text = "";
                backwardToolStripMenuItem.Enabled = false;
                foreach (var drive in drives)
                {
                    fields.Add(drive.Name);
                    listBox1.Items.Add(drive.Name);
                }
                bwdPath.Clear();
                return;
            }
            textBox1.Text = bwdPath.Last();
            fields.Clear();
            listBox1.Items.Clear();
            foreach (string item in Directory.GetDirectories(textBox1.Text))
            {
                DirectoryInfo file = new DirectoryInfo(item);
                fields.Add(file.FullName);
                listBox1.Items.Add(file.Name);
                bwdPath.Remove(textBox1.Text);
            }
            listBox2.Items.Clear();
            foreach (string item in Directory.GetFileSystemEntries(textBox1.Text))
            {
                FileInfo file = new FileInfo(item);
                listBox2.Items.Add(file.Name);
            }
        }
    }
}