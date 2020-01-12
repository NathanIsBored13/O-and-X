using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Timers;

namespace OandX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public struct PObject
    {
        public object obj;
        public int type;
    }
    public enum Token
    {
        None,
        Naught,
        Cross
    }
    public partial class MainWindow : Window
    {
        private readonly int board_size = 3;
        private readonly int[] button_size = new int[] { 200, 200 };
        private readonly SolidColorBrush[] colours = new SolidColorBrush[3] { Brushes.OldLace, Brushes.Pink, Brushes.Lavender };
        private readonly string[] flags = new string[3] { "", "O", "X" };
        private Button[,] buttons;
        private Board board;
        private PObject[] players;
        private bool turn;
        private int turn_counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            Helper.SetBoardSize(board_size);
            Game_Board.Rows = board_size;
            Game_Board.Columns = board_size;
            buttons = new Button[board_size, board_size];
            for (int i = 0; i < Math.Pow(board_size, 2); i++)
            {
                buttons[i % board_size, (int)Math.Floor((double)i / board_size)] = new Button()
                {
                    Width = button_size[0],
                    Height = button_size[1],
                    Uid = string.Format("{0},{1}", i % board_size, (int)Math.Floor((double)i / board_size))
                };
                buttons[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]].Click += Button_Click;
                Game_Board.Children.Add(buttons[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]]);
            }
            players = new PObject[2];
            players[0] = new PObject
            {
                obj = new Human(board_size, Token.Naught),
                type = 0
            };
            /*players[1] = new PObject
            {
                obj = new Human(board_size, Token.Cross),
                is_human = true
            };*/
            /*players[0] = new PObject
            {
                obj = new AI(board_size, Token.Naught),
                is_human = false
            };*/
            players[1] = new PObject
            {
                obj = new MinMax_AI(board_size, Token.Cross),
                type = 1
            };
            Reset();
        }
        private void Turn()
        {
            PObject player = players[turn ? 0 : 1];
            if(player.type == 0)
            {
                Human human = (Human)player.obj;
                int[] buffer = human.Move(board);
                if (buffer[0] >= 0 && buffer[1] >= 0)
                {
                    turn = !turn;
                    board.Move(buffer, human.token);
                    Draw();
                    CheckWin();
                }
            }
            else
            {
                turn = !turn;
                if (player.type == 1)
                {
                    MinMax_AI ai = (MinMax_AI)player.obj;
                    board.Move(ai.Move(board), ai.token);
                }
                Draw();
                CheckWin();
            }
        }
        private int CheckWin()
        {
            int buffer = board.CheckWin();
            if (new int[2] { 1, 2 }.Contains(buffer)) MessageBox.Show(string.Format("{0} won!", (Token) buffer), "O and X");
            else if (buffer == 3) MessageBox.Show("Nobody won, it's a draw!", "O and X");
            if (new int[3] { 1, 2, 3 }.Contains(buffer)) Reset();
            return buffer;
        }
        private void Reset()
        {
            turn = true;
            turn_counter = 0;
            buttons[(int)Math.Floor((double)(board_size - 1) / 2), (int)Math.Floor((double)(board_size - 1) / 2)].Focus();
            SetButton(buttons[(int)Math.Floor((double)(board_size - 1) / 2), (int)Math.Floor((double)(board_size - 1) / 2)]);
            board = new Board(board_size);
            Draw();
        }
        private void Draw()
        {
            Turn_Counter_Display.Content = turn_counter++;
            for (int i = 0; i < Math.Pow(board_size, 2); i++)
            {
                buttons[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]].Background = colours[(int)board.GetBoardState()[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]]];
                buttons[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]].Content = flags[(int)board.GetBoardState()[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]]];
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetButton((Button)sender);
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (new Key[] { Key.Up, Key.Down, Key.Left, Key.Right }.Contains(e.Key))
            {
                if (e.Key == Key.Up) buttons[Helper.last_button[0], Helper.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                else if (e.Key == Key.Down) buttons[Helper.last_button[0], Helper.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                else if (e.Key == Key.Left) buttons[Helper.last_button[0], Helper.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                else if (e.Key == Key.Right) buttons[Helper.last_button[0], Helper.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                foreach (Button button in buttons) if (button.IsFocused) SetButton(button);
            }
            else if (e.Key == Key.Enter) Turn();
            else if (e.Key == Key.Escape) Close();
            e.Handled = true;
        }
        private void SetButton(Button sender)
        {
            Helper.last_button = new int[2] { Convert.ToInt32((sender).Uid.Split(',')[0]), Convert.ToInt32((sender).Uid.Split(',')[1]) };
            buttons[Helper.last_button[0], Helper.last_button[1]].Focus();
        }
    }
}
