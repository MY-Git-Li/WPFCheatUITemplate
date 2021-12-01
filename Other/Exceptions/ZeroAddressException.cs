using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Exceptions
{
    public class ZeroAddressException: ApplicationException
    {
        public GameData gameData;
        public ZeroAddressException(string message, GameData gameData) : base(message)
        {
            this.gameData = gameData;
        }
    }
}
