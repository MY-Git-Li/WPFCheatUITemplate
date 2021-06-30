using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace CheatUITemplt
{
    class MyButton
    {
        ButtonBase button;
        public delegate void MyButtonFun(int index);
        MyButtonFun myButtonFun;
        int index;
        public MyButton(ButtonBase button)
        {
            this.button = button;
            //SetButton((Button)this.button);
        }

        public void SetOnClick(int idIndex, MyButtonFun fun)
        {
            index = idIndex - 1;
            myButtonFun = fun;
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            myButtonFun(index);
        }

        //设置按钮，取消自动获得焦点
        //private void SetButton(Button button)
        //{
        //    MethodInfo methodinfo = button.GetType().GetMethod("SetStyle", BindingFlags.NonPublic
        //        | BindingFlags.Instance | BindingFlags.InvokeMethod);
        //    methodinfo.Invoke(button, BindingFlags.NonPublic
        //        | BindingFlags.Instance | BindingFlags.InvokeMethod, null, new object[]
        //        {ControlStyles.Selectable,false}, Application.CurrentCulture);
        //}
    }
}
