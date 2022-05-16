using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.GameFuns;

namespace WPFCheatUITemplate.Core.Exceptions
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
