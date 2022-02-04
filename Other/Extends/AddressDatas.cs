using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate.Other.Extends
{
    abstract class AddressDatas : DataBase ,IAddressDatas
    {
        abstract public void Init();
        
    }
}
