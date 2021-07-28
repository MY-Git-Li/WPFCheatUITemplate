using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatUITemplt
{
    class MyButtonManger
    {
        List<int> ButtonFunId = new List<int>();
        int index = 0;

        public void SetButtonFun(MyButton myButton, int id, HotSystem hotSystem)
        {
            ButtonFunId.Add(id);
            index += 1;
            myButton.SetOnClick(index, (i) => { hotSystem.ClickHotKeyFun(ButtonFunId[i]); });
        }

    }
}
