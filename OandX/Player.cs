using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandX
{
    class Player
    {
        protected int size;
        protected Token token;
        public Player(int size, Token token)
        {
            this.size = size;
            this.token = token;
        }
        public Token Get_Token() => token;
    }
}
