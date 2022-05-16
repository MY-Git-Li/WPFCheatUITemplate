using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFCheatUITemplate.Core.UI
{
    class CreateUIGrid
    {
        Grid grid;
        int Column = -1;
        Grid curentGrid;
        CreateLayout createLayout;

        public CreateUIGrid(Grid grid, CreateLayout createLayout)
        {
            this.grid = grid;
            this.createLayout = createLayout;
            NextPage();
        }

        public void NextPage(int Offset = 0)
        {
            AddColumn();
            Column++;

            curentGrid = new Grid()
            {
                Margin = new Thickness(8+ Offset, 0,0,0)
            };

            curentGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(260)
            });
            curentGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            grid.Children.Add(curentGrid);

            SetPosition(curentGrid);

           

            createLayout.SetGrid(curentGrid);
        }

        void AddColumn()
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(645)
            });
        }

        void SetPosition(UIElement control)
        {
            Grid.SetColumn(control, Column);
        }
    }
}
