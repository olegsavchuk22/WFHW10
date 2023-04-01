using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FHW10
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(textBox1.Text);
                menuStrip1.Items.Add(menuItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (menuStrip1.Items.Count > 0) 
            {
                for (int i = 0; i < menuStrip1.Items.Count; i++)
                {
                    if (menuStrip1.Items[i].Text == textBox1.Text)
                    {
                        ((ToolStripMenuItem)menuStrip1.Items[i]).DropDownItems.Add(textBox2.Text);
                        return;
                    }
                }
            }
            MessageBox.Show("Виберіть один із пунктів меню", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}