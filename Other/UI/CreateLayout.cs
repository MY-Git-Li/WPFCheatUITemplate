using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate.Other.UI
{

    class CreateLayout
    {
        Grid grid;
        ResourceDictionary resourceDictionary;
        int row = 0;

        public CreateLayout(ResourceDictionary resourceDictionary)
        {
            this.resourceDictionary = resourceDictionary;
        }

        public void SetGrid(Grid grid)
        {
            this.grid = grid;
            row = 0;
        }

        public Description CreatShowDescription(GameFun gameFun)
        {
            Description sc = new Description();

            sc.keyDescription = CreatKeyTextBlock(gameFun.gameFunDataAndUIStruct.uIData.KeyDescription_SC);
            sc.funDescription = CreatFunTextBlock(gameFun.gameFunDataAndUIStruct.uIData.FunDescribe_SC);

            SetPosition(sc.keyDescription, true);
            grid.Children.Add(sc.keyDescription);
            return sc;
        }

        public void CreatSeparate(int offset = 30)
        {
            AddRowDefin(offset);
            UpDateRow();
        }

        public void CreatSeparate(LanguageUI languageUI,int offset = 30)
        {
            AddRowDefin(offset);

            languageUI.textBlock = CreatSeparateTextBlock(languageUI.Description_SC);

            SetPosition(languageUI.textBlock, true);
            grid.Children.Add(languageUI.textBlock);

            UpDateRow();
        }

        public MyStackPanel CreatMyStackPanel(GameFun gameFun, GameFunUI gameFunUI)
        {

            MyStackPanel myStackPanel = new MyStackPanel();

            StackPanel stackPanel = CreatStackPanel();
            SetPosition(stackPanel, false);

            if (gameFun.gameFunDataAndUIStruct.uIData.IsTrigger)
            {
                myStackPanel.button = CreatButton();
                stackPanel.Children.Add(myStackPanel.button);

            }
            else
            {
                myStackPanel.checkBox = CreatCheckBox();
                stackPanel.Children.Add(myStackPanel.checkBox);
            }

            stackPanel.Children.Add(gameFunUI.showDescription.funDescription);

            if (gameFun.gameFunDataAndUIStruct.uIData.IsAcceptValue)
            {
                Slider slider = CreatSlider();
                TextBox textBox = CreatTextBox();

                slider.Maximum = gameFun.gameFunDataAndUIStruct.uIData.SliderMaxNum;
                slider.Minimum = gameFun.gameFunDataAndUIStruct.uIData.SliderMinNum;
                slider.Value = slider.Minimum > 1 ? slider.Minimum : 1;

                myStackPanel.ValueEntered = slider;

                //实例化绑定对象
                Binding textBinding = new Binding();
                //设置要绑定源控件
                textBinding.Source = slider;
                //设置要绑定属性
                textBinding.Path = new PropertyPath("Value");

                textBinding.Mode = BindingMode.TwoWay;

                if (gameFun.gameFunDataAndUIStruct.uIData.IsShowDecimal)
                {
                    slider.IsSnapToTickEnabled = false;
                    textBinding.StringFormat = "{0:F1}";
                }

                //设置绑定到要绑定的控件
                textBox.SetBinding(TextBox.TextProperty, textBinding);


                //实例化绑定对象
                Binding textBinding2 = new Binding();
                //设置要绑定源控件
                textBinding2.Source = slider;
                //设置要绑定属性
                textBinding2.Path = new PropertyPath("IsEnabled");
                //设置绑定到要绑定的控件
                textBox.SetBinding(TextBox.IsEnabledProperty, textBinding2);


                //设置只允许输入数字
                textBox.PreviewTextInput += (sender, e) =>
                {
                    System.Text.RegularExpressions.Regex re;

                    re = new System.Text.RegularExpressions.Regex("[^0-9-]+");
                    
                    e.Handled = re.IsMatch(e.Text);
                };

                //及时显示更改
                textBox.TextChanged += (s, e) => 
                {
                    var tt = s as TextBox;
                    double v;
                    try
                    {
                        v = System.Double.Parse(tt.Text);
                    }
                    catch
                    {
                        v = 0;
                    }
                    

                    if (v > slider.Maximum)
                    {
                        tt.Text = ((float)slider.Maximum).ToString();
                    }

                    if (v < slider.Minimum)
                    {
                        tt.Text = ((float)slider.Minimum).ToString();
                    }


                    slider.Value = v;
                };

                stackPanel.Children.Add(slider);
                stackPanel.Children.Add(textBox);

            }

            grid.Children.Add(stackPanel);

            return myStackPanel;

        }

        public void UpDateRow()
        {
            row++;
        }

        public void AddRowDefin(int offset = 30)
        {
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(offset)
            });
        }


        void SetPosition(UIElement control, bool isStart)
        {
            if (isStart)
            {
                Grid.SetRow(control, row);
                Grid.SetColumn(control, 0);
            }
            else
            {
                Grid.SetRow(control, row);
                Grid.SetColumn(control, 1);
            }

        }

       
        TextBlock CreatSeparateTextBlock(string con)
        {
            TextBlock keydiscrb = new TextBlock();
            keydiscrb.Style = resourceDictionary["SeparateDescribeText"] as Style;
            keydiscrb.Text = con;
            keydiscrb.Margin = new Thickness(0, 10, 0, 0);

            return keydiscrb;
        }

        TextBlock CreatKeyTextBlock(string con)
        {
            TextBlock keydiscrb = new TextBlock();
            keydiscrb.Style = resourceDictionary["DescribeText"] as Style;
            keydiscrb.Text = con;
            keydiscrb.Margin = new Thickness(0, 10, 0, 0);



            return keydiscrb;
        }

        TextBlock CreatFunTextBlock(string con)
        {
            TextBlock keydiscrb = new TextBlock();
            keydiscrb.Style = resourceDictionary["DescribeText"] as Style;
            keydiscrb.Text = con;
            keydiscrb.Margin = new Thickness(10, 0, 0, 0);
            return keydiscrb;
        }

        StackPanel CreatStackPanel()
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(-40, 10, 0, 0);
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;


            return stackPanel;
        }
        CheckBox CreatCheckBox()
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Style = resourceDictionary["chkBullet"] as Style;
            checkBox.IsChecked = false;
            checkBox.IsEnabled = false;

            return checkBox;
        }

        Button CreatButton()
        {
            Button button = new Button();
            button.Style = resourceDictionary["Describebutton"] as Style;
            button.IsEnabled = false;

            return button;
        }
        Slider CreatSlider()
        {
            Slider slider = new Slider();
            slider.Margin = new Thickness(3, 4, 0, 0);
            slider.Height = 18;
            slider.Width = 150;
            slider.Minimum = 1;
            slider.Maximum = 100;
            slider.IsSnapToTickEnabled = true;
            slider.FontSize = 15;
            slider.HorizontalContentAlignment = HorizontalAlignment.Left;
            slider.VerticalAlignment = VerticalAlignment.Center;
            slider.Style = resourceDictionary["SliderStyle1"] as Style;

            return slider;
        }

        TextBox CreatTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Style = resourceDictionary["MyTextBoxStyle"] as Style;
            textBox.FontSize = 15;
            textBox.Text = "25";
            return textBox;
        }

    }

}
