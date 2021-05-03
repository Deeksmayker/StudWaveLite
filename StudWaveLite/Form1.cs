using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StudWaveLite
{
    public partial class Form1 : Form
    {
        public Panel GamePanel;

        public Label TextLabel;
        public Button FirstChoiceButton;
        public Button SecondChoiceButton;
        public Button ThirdChoiceButton;
        public Label DateInfoLabel;

        public Form1()
        {
            InitializeComponent();

            GamePanel = GetGamePanel();
            Controls.Add(GamePanel);

            TextLabel = GetTextLabel();
            GamePanel.Controls.Add(TextLabel);

            FirstChoiceButton = GetChoiceButton(1);
            SecondChoiceButton = GetChoiceButton(1.5);
            ThirdChoiceButton = GetChoiceButton(2);
            GamePanel.Controls.Add(FirstChoiceButton);
            GamePanel.Controls.Add(SecondChoiceButton);
            GamePanel.Controls.Add(ThirdChoiceButton);

            DateInfoLabel = GetDateInfoLabel();
            GamePanel.Controls.Add(DateInfoLabel);

        }

        public Panel GetGamePanel()
        {
            var panel = new Panel();
            panel.Size = new Size(ClientSize.Width, ClientSize.Height);

            SizeChanged += (sender, args) =>
            {
                panel.Size = new Size(ClientSize.Width, ClientSize.Height);
            };

            return panel;
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

        public Button GetChoiceButton(double yLocation)
        {
            var button = new Button();
            button.Location = new Point(ClientSize.Width / 5, (int)(ClientSize.Height / 3 * yLocation));
            button.Size = new Size(width: ClientSize.Width - button.Location.X * 2, height: ClientSize.Height / 10);
            button.Text = "Arevuar maman";
            button.Font = new Font(button.Font.FontFamily, ClientSize.Width / 70);
            button.TextAlign = (ContentAlignment)HorizontalAlignment.Center;

            SizeChanged += (sender, args) =>
            {
                button.Location = new Point(ClientSize.Width / 5, (int)(ClientSize.Height / 3 * yLocation));
                button.Size = new Size(width: ClientSize.Width - button.Location.X * 2, height: ClientSize.Height / 10);
                button.Font = new Font(button.Font.FontFamily, ClientSize.Width / 70);
            };

            return button;
        }

        public Label GetDateInfoLabel()
        {
            var label = new Label();

            label.Location = new Point(20, 20);
            label.Size = new Size(ClientSize.Width / 10, ClientSize.Height / 10);
            label.BackColor = Color.Beige;
            label.Text = "SFD";
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 96);
            label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;

            SizeChanged += (sender, args) =>
            {
                label.Size = new Size(ClientSize.Width / 10, ClientSize.Height / 10);
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 96);
                label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            };

            return label;
        }
    }
}
