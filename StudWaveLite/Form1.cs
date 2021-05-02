using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StudWaveLite
{
    public partial class Form1 : Form
    {
        public Label TextLabel;

        public Form1()
        {
            InitializeComponent();

            TextLabel = GetTextLabel();

            Controls.Add(TextLabel);

        }

        public Label GetTextLabel()
        {
            var label = new Label();
            label.Location = new Point(ClientSize.Width / 6, 20);
            label.BackColor = Color.AliceBlue;
            label.Text = "YOS 123 готов мальчик? Но все таки что тебе можно сказать так это то что все таки в этом мире сказать иногда не достаточно, поэтому стоит думать своей головой прежде чем идти на упреждение, понимаешь о чем я, надедюсь, тут не сложно понять, в принципе.";
            label.Size = new Size(width: ClientSize.Width - label.Location.X * 2, height: ClientSize.Height / 4);
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 96);
            label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            SizeChanged += (sender, args) =>
            {
                label.Location = new Point(ClientSize.Width / 6, 20);
                label.Size = new Size(width: ClientSize.Width - label.Location.X * 2, height: ClientSize.Height / 4);
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 96);
            };

            return label;
        }
    }
}
