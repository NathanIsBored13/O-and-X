using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandX
{
    class AI : Player
    {
        private Token opponent;
        public AI(int size, Token token, Token opponent) : base(size, token)
        {
            this.opponent = opponent;
        }
        public int[] Move(Board board)
        {

            throw new NotImplementedException();
        }
    }
}
