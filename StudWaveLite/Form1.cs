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
using StudWaveLite.Model;


namespace StudWaveLite
{
    public partial class Form1 : Form
    {
        private const int FontSizeSeparator = 96;

        public Panel GamePanel;

        public Label TextLabel;
        public Button FirstChoiceButton;
        public Button SecondChoiceButton;
        public Button ThirdChoiceButton;
        public Label DateInfoLabel;
        public Tuple<ProgressBar, Label> HealthBar;
        public Tuple<ProgressBar, Label> MoodBar;
        public Tuple<ProgressBar, Label> StudyBar;

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

            HealthBar = GetStatBar("Здоровье", 1);
            GamePanel.Controls.Add(HealthBar.Item1);
            GamePanel.Controls.Add(HealthBar.Item2);

            MoodBar = GetStatBar("Настроение", 6);
            GamePanel.Controls.Add(MoodBar.Item1);
            GamePanel.Controls.Add(MoodBar.Item2);

            StudyBar = GetStatBar("Учеба", 11);
            GamePanel.Controls.Add(StudyBar.Item1);
            GamePanel.Controls.Add(StudyBar.Item2);
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
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
            label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            SizeChanged += (sender, args) =>
            {
                label.Location = new Point(ClientSize.Width / 6, 20);
                label.Size = new Size(width: ClientSize.Width - label.Location.X * 2, height: ClientSize.Height / 4);
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
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
            var date = new DateInfo();

            label.Location = new Point(40, 40);
            label.Size = new Size(ClientSize.Width / 14, ClientSize.Height / 10);
            label.BackColor = Color.Beige;
            label.Text = date.GetDateAndCourse();
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
            label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;

            SizeChanged += (sender, args) =>
            {
                label.Size = new Size(ClientSize.Width / 14, ClientSize.Height / 10);
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
                label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            };

            return label;
        }

        public Tuple<ProgressBar, Label> GetStatBar(string statName, double xLocation)
        {
            var bar = new ProgressBar();
            var label = new Label();

            bar.Location = new Point((int)(ClientSize.Width / 20 * xLocation), (int) (ClientSize.Height / 1.15));
            bar.Size = new Size(ClientSize.Width / 5, ClientSize.Height / 20);

            label.Location = new Point(bar.Location.X, bar.Location.Y - bar.Size.Height);
            label.Size = bar.Size;
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
            label.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            label.Text = statName;

            SizeChanged += (sender, args) =>
            {
                bar.Location = new Point((int)(ClientSize.Width / 20 * xLocation), (int)(ClientSize.Height / 1.15));
                bar.Size = new Size(ClientSize.Width / 5, ClientSize.Height / 20);

                label.Location = new Point(bar.Location.X, bar.Location.Y - bar.Size.Height);
                label.Size = bar.Size;
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
            };

            return Tuple.Create(bar, label);
        }
    }
}
