using System;

namespace OandX
{
    class Board
    {
        private Token[,] board_state;
        private int size;
        public Board(int size, Token[,] board_state = null)
        {
            this.size = size;
            if (board_state == null) this.board_state = new Token[size, size];
            else this.board_state = Copy(board_state);
        }
        private Token[,] Copy(Token[,] values)
        {
            Token[,] ret = new Token[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    ret[x, y] = values[x, y];
            return ret;
        }
        public void Move(int[] point, Token state)
        {
            board_state[point[0], point[1]] = state;
        }
        public bool IsValid(int[] position) => board_state[position[0], position[1]] == Token.None;
        public Board[,] GetChildren(Token token)
        {
            Board[,] ret = new Board[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (IsValid(new int[] { x, y }))
                    {
                        ret[x, y] = new Board(size, board_state);
                        ret[x, y].Move(new int[] { x, y }, token);
                    }
                    else ret[x, y] = null;
                }
            }
            return ret;
        }
        public int CheckWin()
        {
            int ret = 3;
            int buffer;
            for (int i = 0; i < Math.Pow(size, 2); i++)
            {
                if (board_state[Helper.GetPoint(i)[0], Helper.GetPoint(i)[1]] == Token.None) ret = 0;
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
