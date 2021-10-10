﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFCheatUITemplate.Properties;

namespace CheatUITemplt
{
  
    class CreateLayout
    {
        Grid grid;
        ResourceDictionary resourceDictionary;
        int row=0;

        public CreateLayout(Grid grid, ResourceDictionary resourceDictionary)
        {
            this.grid = grid;
            this.resourceDictionary = resourceDictionary;
        }


        public Description CreatDescription_SC(GameFun gameFun)
        {
            Description sc = new Description();


            sc.keyDescription = CreatKeyTextBlock(gameFun.gameFunDateStruct.KeyDescription_SC);
            sc.funDescription = CreatFunTextBlock(gameFun.gameFunDateStruct.FunDescribe_SC);

            //SetPosition(sc.keyDescription, true);
            //grid.Children.Add(sc.keyDescription);
            return sc;
        }
        public Description CreatDescription_TC(GameFun gameFun)
        {
            Description sc = new Description();


            sc.keyDescription = CreatKeyTextBlock(gameFun.gameFunDateStruct.KeyDescription_TC);
            sc.funDescription = CreatFunTextBlock(gameFun.gameFunDateStruct.FunDescribe_TC);

            //SetPosition(sc.keyDescription, true);

            return sc;
        }
        public Description CreatDescription_EN(GameFun gameFun)
        {
            Description sc = new Description();


            sc.keyDescription = CreatKeyTextBlock(gameFun.gameFunDateStruct.KeyDescription_EN);
            sc.funDescription = CreatFunTextBlock(gameFun.gameFunDateStruct.FunDescribe_EN);

            //SetPosition(sc.keyDescription, true);

            return sc;
        }

        public Description CreatShowDescription(GameFun gameFun)
        {
            Description sc = new Description();

            sc.keyDescription = CreatKeyTextBlock(gameFun.gameFunDateStruct.KeyDescription_SC);
            sc.funDescription = CreatFunTextBlock(gameFun.gameFunDateStruct.FunDescribe_SC);

            SetPosition(sc.keyDescription, true);
            grid.Children.Add(sc.keyDescription);
            return sc;
        }

        public void CreatSeparate()
        {
            Description sc = new Description();
            sc.keyDescription = CreatKeyTextBlock("");
            SetPosition(sc.keyDescription, true);
            grid.Children.Add(sc.keyDescription);
        }

        public MyStackPanel CreatMyStackPanel(GameFun gameFun,GameFunUI gameFunUI)
        {

            MyStackPanel myStackPanel = new MyStackPanel();

            StackPanel stackPanel = CreatStackPanel();
            SetPosition(stackPanel,false);

            if (gameFun.gameFunDateStruct.IsTrigger)
            {
                myStackPanel.button = CreatButton();
                stackPanel.Children.Add(myStackPanel.button);

            }else
            {
                myStackPanel.checkBox = CreatCheckBox();
                stackPanel.Children.Add(myStackPanel.checkBox);
            }

            stackPanel.Children.Add(gameFunUI.showDescription.funDescription);

            if (gameFun.gameFunDateStruct.IsAcceptValue)
            {
                Slider slider = CreatSlider();
                TextBox textBox = CreatTextBox();

                slider.Maximum = gameFun.gameFunDateStruct.SliderMaxNum;
                slider.Minimum = gameFun.gameFunDateStruct.SliderMinNum;

                myStackPanel.ValueEntered = slider;
               //实例化绑定对象
               Binding textBinding = new Binding();
                //设置要绑定源控件
                textBinding.Source = slider;
                //设置要绑定属性
                textBinding.Path = new PropertyPath("Value");
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


                //实例化绑定对象
                Binding sliderBind = new Binding();
                //设置要绑定源控件
                sliderBind.Source = textBox;
                //设置要绑定属性
                sliderBind.Path = new PropertyPath("Text");
                //设置绑定到要绑定的控件
                slider.SetBinding(Slider.ValueProperty, sliderBind);




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

        public void AddRowDefin()
        {
            AddRowDefinitions();
        }




        void SetPosition(UIElement control,bool isStart)
        {
            if (isStart)
            {
                Grid.SetRow(control, row);
                Grid.SetColumn(control, 0);
            }else
            {
                Grid.SetRow(control, row);
                Grid.SetColumn(control, 1);
            }
           
        }

        void AddRowDefinitions()
        {
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(30)
            });

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
