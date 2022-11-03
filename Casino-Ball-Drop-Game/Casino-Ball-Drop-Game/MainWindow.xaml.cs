using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino_Ball_Drop_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Label[][] gameBoard = new Label[7][]; 
        private Grid gameBoardCont = new Grid();

        public MainWindow()
        {
            InitializeComponent();
            initBoard();
        }

        private void initBoard() 
        {
            int v = 5;
            int h = 5;
            

            gameBoardCont = new Grid();
            gameBoardCont.VerticalAlignment = VerticalAlignment.Top;
            gameBoardCont.HorizontalAlignment = HorizontalAlignment.Left;
            gameBoardCont.Width = 350;
            gameBoardCont.Height = 350;
            gameBoardCont.Margin = new Thickness(225,45,0,0);

            mainGrid.Children.Add(gameBoardCont);

            for (int i = 0; i < gameBoard.Length; i++)
                gameBoard[i] = new Label[5];

            for (int x = 0; x < gameBoard.Length; x++) 
            {
                for (int y = 0; y < gameBoard[x].Length; y++) 
                {
                    Label boardGrid = new Label();
                    boardGrid.HorizontalAlignment = HorizontalAlignment.Left;   
                    boardGrid.VerticalAlignment = VerticalAlignment.Top;
                    boardGrid.Width = 40;
                    boardGrid.Height = 40;
                    boardGrid.Margin = new Thickness(h,v,0,0);
                    boardGrid.BorderBrush = new SolidColorBrush(Colors.Black);
                    boardGrid.BorderThickness = new Thickness(2, 2, 2, 2);
                    gameBoard[x][y] = boardGrid;
                    gameBoardCont.Children.Add(gameBoard[x][y]);

                    h += 5 + (int)boardGrid.Width;
                }
                h = 5;
                v += 45;
            }
            

            
        }
    }
}
