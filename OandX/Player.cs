namespace OandX
{
    abstract class Player
    {
        public Token token { get; }
        public int size { get; }
        public Player(int size, Token token)
        {
            this.token = token;
            this.size = size;
        }
        public abstract int[] Move(Board board);
    }
}
