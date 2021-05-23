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

        public Panel MainMenuPanel;
        public Label GameNameLabel;
        public Button NewGameButton;
        public Button LoadGameButton;
        public Button SettingsButton;
        public Button CloseGameButton;

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

        private Game game;
        private Player player;
        private World world;
        private Plot plot;
        private DateInfo dateInfo;

        private MonthEvent currentEvent;

        //Игровые стадии в таком порядке: Еда на месяц => Активность в свободное время => Событие => по кругу
        public MainForm()
        {
            game = new Game();
            player = new Player();
            world = new World();
            dateInfo = new DateInfo();
            plot = new Plot(player, world, dateInfo);

            InitializeComponent();
            

            DrawMainMenu();
        }

        private void StartNewGame()
        {
            DrawGameInterface();

            DateInfoLabel.Text = dateInfo.GetDateAndCourse();
            HealthBar.Item1.Value = player.Health;
            MoodBar.Item1.Value = player.Mood;
            StudyBar.Item1.Value = player.Study;
            MoneyLabel.Text = player.GetMoney();

            currentEvent = plot.GetAvailableMonthEvent(0, 8);
            TextLabel.Text = currentEvent.TextEvent;
            FirstChoiceButton.Text = currentEvent.FirstChoice.ChoiceText;

            FirstChoiceButton.Click += (sender, args) =>
            {
                game.FirstButtonWorldInteract(player, world, dateInfo, currentEvent);
                PrepareNextStage(currentEvent.FirstChoice);
                RefreshAllStats();
            };

            SecondChoiceButton.Click += (sender, args) =>
            {
                game.SecondButtonWorldInteract(player, world, dateInfo, currentEvent);
                PrepareNextStage(currentEvent.SecondChoice);
                RefreshAllStats();
            };

            ThirdChoiceButton.Click += (sender, args) =>
            {
                game.ThirdButtonWorldInteract(player, world, dateInfo, currentEvent);
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
                currentEvent = plot.GetAvailableMonthEvent(dateInfo.Course, dateInfo.Month);
                TextLabel.Text = currentEvent.TextEvent;
                FirstChoiceButton.Text = currentEvent.FirstChoice.ChoiceText;
                SecondChoiceButton.Text = currentEvent.SecondChoice.ChoiceText;
                ThirdChoiceButton.Text = currentEvent.ThirdChoice.ChoiceText;

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

        private void SetMainMenuButtonsActions()
        {
            NewGameButton.Click += (sender, args) =>
            {
                MainMenuPanel.Hide();
                StartNewGame();
            };


            CloseGameButton.Click += (sender, args) =>
            {
                this.Close();
            };
        }

        #region Interface

        private void DrawMainMenu()
        {
            MainMenuPanel = GetPanel();
            Controls.Add(MainMenuPanel);

            GameNameLabel = GetGameNameLabel();
            MainMenuPanel.Controls.Add(GameNameLabel);

            NewGameButton = GetButton(1);
            NewGameButton.Text = "Новая игра";
            MainMenuPanel.Controls.Add(NewGameButton);

            LoadGameButton = GetButton(1.5);
            LoadGameButton.Text = "Загрузить игру";
            MainMenuPanel.Controls.Add(LoadGameButton);

            SettingsButton = GetButton(2);
            SettingsButton.Text = "Настройки";
            MainMenuPanel.Controls.Add(SettingsButton);

            CloseGameButton = GetButton(2.5);
            CloseGameButton.Text = "Выйти";
            MainMenuPanel.Controls.Add(CloseGameButton);

            SetMainMenuButtonsActions();
        }

        public Label GetGameNameLabel()
        {
            var label = GetTextLabel();
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 30);
            label.Text = "StudWaveLite";

            SizeChanged += (sender, args) =>
            {
                label.Font = new Font(label.Font.FontFamily, ClientSize.Width / 30);
            };

            return label;
        }

        private void DrawGameInterface()
        {
            GamePanel = GetPanel();
            Controls.Add(GamePanel);

            TextLabel = GetTextLabel();
            GamePanel.Controls.Add(TextLabel);

            FirstChoiceButton = GetButton(1);
            SecondChoiceButton = GetButton(1.5);
            ThirdChoiceButton = GetButton(2);
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

        public Panel GetPanel()
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

        public Button GetButton(double yLocation)
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
