using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudWaveLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var label = new Label();
            label.Location = new Point(0, 0);
            label.Size = new Size(ClientSize.Width, 30);
            label.Text = "Давай давай давай давай";
            Controls.Add(label);
        }
    }
}
