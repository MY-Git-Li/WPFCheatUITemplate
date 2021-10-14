using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFCheatUITemplate;

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

        public LanguageUI keylanguageUI;
        public LanguageUI funlanguageUI;

        public Description showDescription;
        public MyStackPanel myStackPanel;


    }
}
