using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate.Other.UI
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

        public LanguageUI separatelanguageUI;

        public bool doNextPage;
        public int  nextPageOffset;
        public int SeparateOffset;

        public Description showDescription;
        public MyStackPanel myStackPanel;


    }
}
