using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MonitorEstadoDB
{
    public partial class Form1 : Form
    {
        private DBConect dbc;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string name = null;
            string server = null;
            string id = null;
            string psw = null;
            string engine = null;
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                XmlTextReader xml = new XmlTextReader(openFileDialog1.FileName);
                while(xml.Read())
                {
                    if (xml.Name == "server" && xml.HasAttributes)
                    {
                        name = xml.GetAttribute("id");
                        engine = xml.GetAttribute("engine");
                    }
                    
                    if (xml.Name == "server" && !xml.HasAttributes)
                    {
                        dbc = new DBConect(server,id,psw,engine);
                        dataGridView1.Rows.Add(name, engine, dbc.validate());
                        server = null;
                        id = null;
                        psw = null;
                    }

                    switch (xml.GetAttribute("id"))
                    {
                        case "Host":
                            server = xml.GetAttribute("value");
                            break;
                        case "UserName":
                            id = xml.GetAttribute("value");
                            break;
                        case "Password":
                            psw = xml.GetAttribute("value");
                            break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumns();
        }

        private void button1_Focus(object sender, EventArgs e)
        {
            button1.Left = 380;
            pictureBox1.BringToFront();
        }

        private void button2_Focus(object sender, EventArgs e)
        {
            button2.Left = 380;
            pictureBox3.BringToFront();
        }

        private void button3_Focus(object sender, EventArgs e)
        {
            button3.Left = 380;
            pictureBox2.BringToFront();
        }

        private void button1_LFocus(object sender, EventArgs e)
        {
            button1.Left = 400;
            pictureBox1.SendToBack();
        }

        private void button2_LFocus(object sender, EventArgs e)
        {
            button2.Left = 400;
            pictureBox3.SendToBack();
        }

        private void button3_LFocus(object sender, EventArgs e)
        {
            button3.Left = 400;
            pictureBox2.SendToBack();
        }
    }
}
