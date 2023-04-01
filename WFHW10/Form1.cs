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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FHW10
{
    public partial class Form1 : Form
    {
        string path = string.Empty;
        List<RichTextBox> tmp = new List<RichTextBox>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Text editor";
        }

        void SaveFileAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files(*.txt) | *.txt";
            sfd.DefaultExt = "txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName;
                SaveFile(sfd.FileName);
            }
        }
        private void SaveFile(string path)
        {
            StreamWriter write = new StreamWriter(path);
            write.Write(richTextBox1.Text);
            write.Close();
        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
            open.FilterIndex = 2;
            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileName;
                StreamReader reader = new StreamReader(open.FileName);
                richTextBox1.Text = reader.ReadToEnd(); 
                reader.Close();
            }
            this.Text = path;
        }

        private void newFileBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            path = string.Empty;
            this.Text = "Text editor";
        }

        private void saveFileBtn_Click(object sender, EventArgs e)
        {
            if (path == "")
            {
                SaveFileAs();
                this.Text = path;
            }
            else
            {
                SaveFile(path);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult message = MessageBox.Show("Close file and save changes?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (path != "" && richTextBox1.Text != "")
            {
                if (message == DialogResult.Yes)
                {
                    SaveFile(path);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else if (path == "" && richTextBox1.Text != "")
            {
                if (message == DialogResult.Yes)
                {
                    SaveFileAs();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void cutBtn_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            if (richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
            string cut_tmp = richTextBox1.Text;
            richTextBox1.Text = cut_tmp.Remove(cut_tmp.IndexOf(Clipboard.GetText()), Clipboard.GetText().Length);
        }

        private void pasteBtn_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            richTextBox1.Text += Clipboard.GetText();
            richTextBox1.Select(richTextBox1.Text.Length, 0);
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            if (tmp.Count != 0) 
            {
                richTextBox1 = tmp.Last();
            }
            
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK )
            {
                if (richTextBox1.SelectedText == "")
                {
                    richTextBox1.ForeColor = color.Color;
                }
                else
                {
                    richTextBox1.SelectionColor = color.Color;
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Select(0, richTextBox1.Text.Length);
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = color.Color;   
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text == "" && path == "")
            {
               e.Cancel = false;
            }
            else
            {
                DialogResult close_dialog = MessageBox.Show("Close file and save changes?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (close_dialog == DialogResult.Yes)
                {
                    if (path != "" && richTextBox1.Text != "")
                    {
                        if (close_dialog == DialogResult.Yes)
                        {
                            SaveFile(path);
                            e.Cancel = false;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else if (path == "" && richTextBox1.Text != "")
                    {
                        if (close_dialog == DialogResult.Yes)
                        {
                            SaveFileAs();
                            e.Cancel = false;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
            }
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Underline);
            }
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Underline);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Italic);
            }
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Italic);
        }

        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmp.Add(richTextBox1);
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
            }
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
        }
    }
}