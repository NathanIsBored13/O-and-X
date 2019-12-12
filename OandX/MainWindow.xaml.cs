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
    public partial class MainWindow : Window
    {
        private readonly int[] board_size = new int[] { 3, 3 };
        private readonly int[] button_size = new int[] { 200, 200 };
        private Button[] segments;
        public MainWindow()
        {
            InitializeComponent();
            Game_Board.Rows = board_size[0];
            Game_Board.Columns = board_size[1];
            Game_Board.Width = button_size[0] * board_size[0];
            Game_Board.Height = button_size[1] * board_size[1];
            segments = new Button[board_size[0] * board_size[1]];
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i] = new Button()
                {
                    BorderThickness = new Thickness(2, 2, 2, 2),
                };
                Game_Board.Children.Add(segments[i]);
            }
        }
    }
}
