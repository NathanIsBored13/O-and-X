using System;
namespace OandX
{
    class MinMax_AI : Player
    {
        private Token opponent;
        public MinMax_AI(int size, Token token) : base(size, token)
        {
            if (token == Token.Naught) opponent = Token.Cross;
            else opponent = Token.Naught;
        }
        public override int[] Move(Board board)
        {
            int[] best = new int[3] { -1, -1, -1 };
            Board[,] boards = board.GetChildren(token);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (boards[x, y] != null)
                    {
                        int buffer = Evaluate(boards[x, y], false);
                        if (buffer > best[2]) best = new int[] { x, y, buffer };
                    }
                }
            }
            return new int[] { best[0], best[1] };
        }
        private int Evaluate(Board board, bool turn)
        {
            int ret = 2;
            int win = board.CheckWin();
            if (win == 0)
            {
                Board[,] children = board.GetChildren(turn ? token : opponent);
                foreach (Board child in children)
                {
                    if (child != null)
                    {
                        int buffer = Evaluate(child, !turn);
                        if ((turn && buffer > ret) || ret == 2) ret = buffer;
                        else if ((!turn && buffer < ret) || ret == 2) ret = buffer;
                    }
                }
            }
            else
            {
                if (win == (int)token) ret = 1;
                else if (win == (int)opponent) ret = -1;
                else if (win == 3) ret = 0;
            }
            return ret;
        }
    }
}
