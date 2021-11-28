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

namespace WindowsFormsAppEasyCs103
{
    public partial class Form1 : Form
    {
        private TextBox[] tb = new TextBox[5];
        private Button bt1, bt2;
        private FlowLayoutPanel flp;
        
        // [STAThread]
        
        public Form1()
        {
            InitializeComponent();
            this.Text = "Save Binary";
            this.Width = 250;
            this.Height = 200;
            for (int i = 0; i < tb.Length; i++)
            {
                tb[i] = new TextBox();
                tb[i].Width = 30;
                tb[i].Height = 30;
                tb[i].Top = 0;
                tb[i].Left = i * tb[i].Width;
                tb[i].Text = Convert.ToString(i);
            }

            bt1 = new Button();
            bt2 = new Button();
            bt1.Text = "Read";
            bt2.Text = "Save";

            flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Bottom;

            bt1.Parent = flp;
            bt2.Parent = flp;
            flp.Parent = this;
            for (int i = 0; i < tb.Length; i++)
            {
                tb[i].Parent = this;
            }

            bt1.Click += new EventHandler(BtClick);
            bt2.Click += new EventHandler(BtClick);
        }

        public void BtClick(Object sender, EventArgs e)
        {
            if (sender == bt1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Binary File | *.bin";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    BinaryReader br = new BinaryReader(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read));
                    for (int i = 0; i < tb.Length; i++)
                    {
                        int num = br.ReadInt32();
                        tb[i].Text = Convert.ToString(num);
                    }
                    br.Close();
                }
            }
            else if (sender == bt2)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Binary File | *.bin";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    BinaryWriter bw =
                        new BinaryWriter(new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write));
                    for (int i = 0; i < tb.Length; i++)
                    {
                        bw.Write(Convert.ToInt32(tb[i].Text));
                    }
                    bw.Close();
                }
            }
        }
    }
}