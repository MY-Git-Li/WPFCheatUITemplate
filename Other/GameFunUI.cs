using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CheatUITemplt
{
    struct Description
    {
        public TextBlock keyDescription;
        public TextBlock funDescription;
        //public Button button;
        //public Label funDescribe;
    }

    struct MyStackPanel
    {
       public Button button;
       public CheckBox checkBox;
       public Slider ValueEntered;

    }


    class GameFunUI
    {
        public GameFun gameFun;

       
        public Description simplifiedChinese;
        public Description traditionalChinese;
        public Description englishDescription;
        public Description showDescription;
        public MyStackPanel myStackPanel;


    }
}
