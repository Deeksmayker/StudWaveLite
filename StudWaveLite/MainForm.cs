using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudWaveLite.Model;


namespace StudWaveLite
{
    public partial class MainForm : Form
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
        public Label MoneyLabel;

        private Player player;
        private World world;
        private Dictionary<int, Dictionary<int, List<MonthEvent>>> plot;
        private DateInfo dateInfo;

        private MonthEvent currentEvent;

        //Игровые стадии в таком порядке: Еда на месяц => Активность в свободное время => Событие => по кругу
        public MainForm()
        {
            InitializeComponent();
            DrawInterface();

            StartNewGame();
        }

        private void StartNewGame()
        {
            player = Player.Instance;
            world = World.Instance;
            plot = Plot.GetPlotDictionary();
            dateInfo = DateInfo.Instance;

            DateInfoLabel.Text = dateInfo.GetDateAndCourse();
            HealthBar.Item1.Value = player.Health;
            MoodBar.Item1.Value = player.Mood;
            StudyBar.Item1.Value = player.Study;
            MoneyLabel.Text = player.GetMoney();

            TextLabel.Text = plot[0][8][0].TextEvent;
            FirstChoiceButton.Text = plot[0][8][0].FirstChoice.ChoiceText;
            currentEvent = plot[0][8][0];
            FirstChoiceButton.Click += (sender, args) =>
            {
                if (world.IsFoodStage)
                {
                    player.Money -= 5000;
                    player.Health += 10;
                    player.Mood -= 5;
                }

                else if (world.IsFreeActivityStage)
                {
                    player.Study += 10;
                    player.Health -= 10;
                    player.Mood -= 20;
                }

                else if (world.IsEventStage)
                {
                    currentEvent.FirstChoice.WorldInteract(currentEvent.FirstChoice.CheckSucces());
                }

                PrepareNextStage(currentEvent.FirstChoice);
                RefreshAllStats();
            };

            SecondChoiceButton.Click += (sender, args) =>
            {
                if (world.IsFoodStage)
                {
                    player.Money -= 8000;
                    player.Health += 5;
                    player.Mood += 10;
                }

                else if (world.IsFreeActivityStage)
                {
                    player.Study -= 10;
                    player.Health += 10;
                    player.Mood -= 10;
                }

                else if (world.IsEventStage)
                {
                    currentEvent.SecondChoice.WorldInteract(currentEvent.SecondChoice.CheckSucces());
                }

                PrepareNextStage(currentEvent.SecondChoice);
                RefreshAllStats();
            };

            ThirdChoiceButton.Click += (sender, args) =>
            {
                if (world.IsFoodStage)
                {
                    player.Money -= 2000;
                    player.Health -= 20;
                    player.Mood -= 20;
                }

                else if (world.IsFreeActivityStage)
                {
                    player.Study -= 20;
                    player.Health -= 10;
                    player.Mood += 20;
                }

                else if (world.IsEventStage)
                {
                    currentEvent.ThirdChoice.WorldInteract(currentEvent.ThirdChoice.CheckSucces());
                }

                PrepareNextStage(currentEvent.ThirdChoice);
                RefreshAllStats();
            };
        }

        private void PrepareNextStage(Choice choice)
        {
            if (world.IsFoodStage)
            {
                //Подготовка стадии свободного времени
                TextLabel.Text = "Чем будешь заниматься в свободное время";
                FirstChoiceButton.Text = "Пора и за ум взяться";
                SecondChoiceButton.Text = "Спорт мой выбор";
                ThirdChoiceButton.Text = "Поработали пора и честь знать.....";

                world.IsFoodStage = false;
                world.IsFreeActivityStage = true;
            }

            else if (world.IsFreeActivityStage)
            {
                //Подготовка стадии события
                foreach (var monthEvent in plot[dateInfo.Course][dateInfo.Month])
                {
                    if (monthEvent.IsAvailableEvent())
                    {
                        TextLabel.Text = monthEvent.TextEvent;
                        FirstChoiceButton.Text = monthEvent.FirstChoice.ChoiceText;
                        SecondChoiceButton.Text = monthEvent.SecondChoice.ChoiceText;
                        ThirdChoiceButton.Text = monthEvent.ThirdChoice.ChoiceText;
                        currentEvent = monthEvent;
                    }
                }

                world.IsFreeActivityStage = false;
                world.IsEventStage = true;
            }

            else if (world.IsEventStage)
            {
                //Подготовка стадии после события
                TextLabel.Text = choice.CheckSucces()
                    ? choice.SuccesAfterChoice
                    : choice.FailAfterChoice;
                FirstChoiceButton.Text = choice.ButtonTextAfterChoice;
                SecondChoiceButton.Text = choice.ButtonTextAfterChoice;
                ThirdChoiceButton.Text = choice.ButtonTextAfterChoice;

                world.IsEventStage = false;
                world.IsAfterEventStage = true;
            }

            else if (world.IsAfterEventStage)
            {
                //Подготовка стадии выбора еды
                TextLabel.Text = "Что будем есть на этот раз";
                FirstChoiceButton.Text = "В этот раз готовить буду сам (5к рупий)";
                SecondChoiceButton.Text = "Думаю и в кафе иногда можно сгонять (8к)";
                ThirdChoiceButton.Text = "доши к.....(за две уложимся....)";

                world.IsAfterEventStage = false;
                world.IsFoodStage = true;
            }
        }

        private void RefreshAllStats()
        {
            DateInfoLabel.Text = dateInfo.GetDateAndCourse();
            MoneyLabel.Text = player.GetMoney();
            HealthBar.Item1.Value = player.Health;
            MoodBar.Item1.Value = player.Mood;
            StudyBar.Item1.Value = player.Study;
        }

        #region Interface

        private void DrawInterface()
        {
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

            MoodBar = GetStatBar("Настроение", 8);
            GamePanel.Controls.Add(MoodBar.Item1);
            GamePanel.Controls.Add(MoodBar.Item2);

            StudyBar = GetStatBar("Учёба", 15);
            GamePanel.Controls.Add(StudyBar.Item1);
            GamePanel.Controls.Add(StudyBar.Item2);

            MoneyLabel = GetMoneyLabel();
            GamePanel.Controls.Add(MoneyLabel);
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
            label.TextAlign = ContentAlignment.TopCenter;
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
            button.Font = new Font(button.Font.FontFamily, ClientSize.Width / 70);
            button.TextAlign = ContentAlignment.MiddleCenter;

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

            bar.Location = new Point((int)(ClientSize.Width / 20 * xLocation), (int)(ClientSize.Height / 1.15));
            bar.Size = new Size(ClientSize.Width / 5, ClientSize.Height / 20);

            label.Location = new Point(bar.Location.X, bar.Location.Y - bar.Size.Height);
            label.Size = bar.Size;
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / FontSizeSeparator);
            label.TextAlign = ContentAlignment.MiddleCenter;
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

        public Label GetMoneyLabel()
        {
            var label = new Label();

            label.Location = new Point((int)(ClientSize.Width / 1.15), 40);
            label.Size = new Size(ClientSize.Width / 9, ClientSize.Height / 12);
            label.BackColor = Color.Beige;
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 60);
            label.Text = "₽ 229111";
            label.TextAlign = ContentAlignment.MiddleCenter;

            SizeChanged += (sender, args) =>
            {
                label.Location = new Point((int)(ClientSize.Width / 1.15), 40);
                label.Size = new Size(ClientSize.Width / 9, ClientSize.Height / 12);
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 60);
            };

            return label;
        }

        #endregion

    }
}
