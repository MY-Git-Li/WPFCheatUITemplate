using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.Interface;

namespace WPFCheatUITemplate.Core.Extends
{
    abstract class AddressDatas : DataBase ,IAddressDatas
    {
        abstract public void Init();
        
    }
}
