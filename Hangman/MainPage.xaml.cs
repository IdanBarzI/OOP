using System;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hangman
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int lvl = START_LVL;
        private const int START_LVL = 0;
        private const int EAZY_LVL = 1;
        private const int NORMAL_LVL = 2;
        private const int HARD_LVL = 3;

        private int winCounter;
        private int loseCounter;

        private Board board;
        private string answertxtl;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            Eazy_lvl.Click += new RoutedEventHandler((sender, e) => Difficulty_lvl(sender, e, EAZY_LVL));
            Normal_lvl.Click += new RoutedEventHandler((sender, e) => Difficulty_lvl(sender, e, NORMAL_LVL));
            Hard_lvl.Click += new RoutedEventHandler((sender, e) => Difficulty_lvl(sender, e, HARD_LVL));
            board = new Board(Mycanvas, lvl);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void Difficulty_lvl(object sender, RoutedEventArgs e, int level)
        {
            this.lvl = level;
            NewGame();
            CollapsToPlay();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            board.AddElement();
            MakeRighrAnswerTB();
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            var key = args.VirtualKey;
            if (key >= VirtualKey.A && key <= VirtualKey.Z && lvl != START_LVL)
            {
                char playerGuesse = char.Parse(key.ToString());
                if (board.IsTheWordContainChar(playerGuesse))
                {
                    GuesseRight(playerGuesse);
                }
                else if (board.Picture.picsUri1.Count > 0)
                {
                    GuesseWrong(playerGuesse);
                }
            }
            if (key == VirtualKey.Escape)
            {
                ExitGame();
            }
        }

        private void GuesseRight(char guesse)
        {
            board.InsertRightCharToAnswer(guesse);
            answertxtl = new string(board.answer);
            RightChars.Text = answertxtl;
            if (board.IsWin())
            {
                WinOrLoseMsg("won", ++winCounter);
            }
        }

        private void GuesseWrong(char guesse)
        {
            WrongChars.Text = WrongChars.Text + "_" + guesse;
            board.Picture.UpdatetImage(board.Picture.picsUri1.Dequeue());
            if (board.IsLose())
            {
                WinOrLoseMsg("lost", ++loseCounter);
            }
        }

        private async void WinOrLoseMsg(string res,int cnt)
        {
            MessageDialog showDialog = new MessageDialog($"You have {res}, {cnt} times\nDo You want to play again?");
            showDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("Back to menu")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                NewGame();
            }
            else
            {
                lvl = START_LVL;
                CollapsToMenu();
            }
        }

        private void MakeRighrAnswerTB()
        {
            for (int i = 0; i < board.mysteryWord.Length; i++)
            {
                RightChars.Text = RightChars.Text + board.answer[i];
            }
        }

        private void ExitGame()
        {
            Application.Current.Exit();
        }

        private void NewGame()
        {
            board.RemoveElement();
            board = new Board(Mycanvas, lvl);
            board.AddElement();
            RightChars.Text = " ";
            MakeRighrAnswerTB();
            WrongChars.Text = " ";
        }

        private void CollapsToPlay()
        {
            OptionsGrid.Visibility = Visibility.Collapsed;
            PlayGrid.Visibility = Visibility.Visible;
        }

        private void CollapsToMenu()
        {
            OptionsGrid.Visibility = Visibility.Visible;
            PlayGrid.Visibility = Visibility.Collapsed;
        }
    }
}
