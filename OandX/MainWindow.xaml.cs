using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OandX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        private readonly string[] flags = new string[5] { "", "O", "X", "Noughts", "Crosses" };
        private Button[,] segments;
        private Board board;
        private bool flag;
        public MainWindow()
        {
            InitializeComponent();
            Game_Board.Rows = board_size;
            Game_Board.Columns = board_size;
            Game_Board.Children.Clear();
            segments = new Button[board_size, board_size];
            for (int i = 0; i < Math.Pow(board_size, 2); i++)
            {
                segments[i % board_size, (int)Math.Floor((double)i / board_size)] = new Button()
                {
                    Width = button_size[0],
                    Height = button_size[1],
                    Uid = string.Format("{0},{1}", i % board_size, (int)Math.Floor((double)i / board_size))
                };
                segments[i % board_size, (int)Math.Floor((double)i / board_size)].Click += Button_Click;
                Game_Board.Children.Add(segments[i % board_size, (int)Math.Floor((double)i / board_size)]);
            }
            segments[0, 0].Focus();
            Button_Click(segments[0, 0], new RoutedEventArgs());
            board = new Board(board_size);
            Draw();
        }
        private void Turn()
        {
            if (board.IsValid(Input_buffer.last_button))
            {
                board = board.Move(Input_buffer.last_button, flag ? Token.Naught : Token.Cross);
                flag = !flag;
                Draw();
                int buffer = board.CheckWin();
                if (new int[2] { 1, 2 }.Contains(buffer)) MessageBox.Show(string.Format("{0} won!", flags[buffer + 2]), "O and X");
                else if (buffer == 3) MessageBox.Show("Nobody won, it's a draw!", "O and X");
            }
        }
        private void Draw()
        {
            for (int i = 0; i < Math.Pow(board_size, 2); i++)
            {
                segments[i % board_size, (int)Math.Floor((double)i / board_size)].Background = colours[(int)board.GetBoardState()[i % board_size, (int)Math.Floor((double)i / board_size)]];
                segments[i % board_size, (int)Math.Floor((double)i / board_size)].Content = flags[(int)board.GetBoardState()[i % board_size, (int)Math.Floor((double)i / board_size)]];
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input_buffer.last_button = new int[2] { Convert.ToInt32(((Button)sender).Uid.Split(',')[0]), Convert.ToInt32(((Button)sender).Uid.Split(',')[1]) };
            segments[Input_buffer.last_button[0], Input_buffer.last_button[1]].Focus();
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (new Key[] { Key.Enter, Key.Up, Key.Down, Key.Left, Key.Right }.Contains(e.Key))
            {
                if (e.Key == Key.Up) segments[Input_buffer.last_button[0], Input_buffer.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                else if (e.Key == Key.Down) segments[Input_buffer.last_button[0], Input_buffer.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                else if (e.Key == Key.Left) segments[Input_buffer.last_button[0], Input_buffer.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                else if (e.Key == Key.Right) segments[Input_buffer.last_button[0], Input_buffer.last_button[1]].MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                foreach (Button button in segments) if (button.IsFocused) Button_Click(button, new RoutedEventArgs());
                if (e.Key == Key.Enter) Turn();
            }
            else if (e.Key == Key.Escape) Close();
            e.Handled = true;
        }
    }
}
