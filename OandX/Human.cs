namespace OandX
{
    class Human : Player
    {
        public Human(int size, Token token) : base(size, token)
        {

        }
        public override int[] Move(Board board)
        {
            int[] ret = new int[2] { -1, -1 };
            if (board.IsValid(Helper.last_button)) ret = Helper.last_button;
            return ret;
        }
    }
}
