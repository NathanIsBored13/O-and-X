using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandX
{
    class Board
    {
        private int size;
        private Token[,] board_state;
        public Board(int size)
        {
            this.size = size;
            board_state = new Token[size, size];
        }
        public Board(int size, Token[,] board_state)
        {
            this.size = size;
            this.board_state = board_state;
        }
        public Board Move(int[] point, Token state)
        {
            Board ret = new Board(size, board_state);
            ret.SetState(point, state);
            return ret;
        }
        public void SetState(int[] point, Token state) => board_state[point[0], point[1]] = state;
        public bool IsValid(int[] position) => board_state[position[0], position[1]] == Token.None;
        public int CheckWin()
        {
            int ret = 3;
            int buffer;
            for (int i = 0; i < Math.Pow(size, 2); i++)
            {
                if (board_state[i % size, (int)Math.Floor((double)i / size)] == Token.None) ret = 0;
            }
            for (int x = 0; x < size; x++)
            {
                buffer = (int)board_state[x, 0];
                for (int y = 1; y < size; y++)
                {
                    if ((int)board_state[x, y] != buffer) buffer = 0;
                }
                if (buffer != 0) ret = buffer;
            }
            for (int y = 0; y < size; y++)
            {
                buffer = (int)board_state[0, y];
                for (int x = 1; x < size; x++)
                {
                    if ((int)board_state[x, y] != buffer) buffer = 0;
                }
                if (buffer != 0) ret = buffer;
            }
            buffer = (int)board_state[0, 0];
            for (int i = 0; i < size; i++)
            {
                if ((int)board_state[i, i] != buffer) buffer = 0;
            }
            if (buffer != 0) ret = buffer;
            buffer = (int)board_state[0, size - 1];
            for (int i = 0; i < size; i++)
            {
                if ((int)board_state[i, size - i - 1] != buffer) buffer = 0;
            }
            if (buffer != 0) ret = buffer;
            return ret;
        }
        public Token[,] GetBoardState() => board_state;
    }
}
