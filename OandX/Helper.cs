using System;
namespace OandX
{
    static class Helper
    {
        public static int[] last_button;
        private static int board_size;
        public static void SetBoardSize(int size)
        {
            board_size = size;
        }
        public static int[] GetPoint(int i) => new int[2] { i % board_size, (int)Math.Floor((double)i / board_size) };
    }
}
