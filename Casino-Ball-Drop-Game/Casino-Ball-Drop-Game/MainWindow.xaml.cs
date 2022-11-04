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
        private Button[] gameButton = new Button[5];

        public MainWindow()
        {
            InitializeComponent();
            initBoard();
        }

        private void showPrizeMessage() 
        {
            if (gameBoard[6][0].Content != null) 
            {
              var confirm = MessageBox.Show("Winner! Slot 1", "", MessageBoxButton.OK);
                if (confirm == MessageBoxResult.OK) 
                {
                    gameBoard[6][0].Content = null;
                    for (int i = 0; i < gameButton.Length; i++)
                        gameButton[i].IsEnabled = true;
                }
            }                           
            else if (gameBoard[6][1].Content != null)
            {
                var confirm = MessageBox.Show("Lost! Slot 2", "", MessageBoxButton.OK);
                if (confirm == MessageBoxResult.OK)
                {
                    gameBoard[6][1].Content = null;
                    for (int i = 0; i < gameButton.Length; i++)
                        gameButton[i].IsEnabled = true;
                }
            }
            else if (gameBoard[6][2].Content != null)
            {
                var confirm = MessageBox.Show("Winner! Slot 3", "", MessageBoxButton.OK);
                if (confirm == MessageBoxResult.OK)
                {
                    gameBoard[6][2].Content = null;
                    for (int i = 0; i < gameButton.Length; i++)
                        gameButton[i].IsEnabled = true;
                }
            }
            else if (gameBoard[6][3].Content != null)
            {
                var confirm = MessageBox.Show("Lost! Slot 4", "", MessageBoxButton.OK);
                if (confirm == MessageBoxResult.OK)
                {
                    gameBoard[6][3].Content = null;
                    for (int i = 0; i < gameButton.Length; i++)
                        gameButton[i].IsEnabled = true;
                }
            }
            else if (gameBoard[6][4].Content != null)
            {
                var confirm = MessageBox.Show("Winner! Slot 5", "", MessageBoxButton.OK);
                if (confirm == MessageBoxResult.OK)
                {
                    gameBoard[6][4].Content = null;
                    for (int i = 0; i < gameButton.Length; i++)
                        gameButton[i].IsEnabled = true;
                }
            }
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

            for (int i = 0; i < gameButton.Length; i++)
            {
                Button button = new Button();
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.VerticalAlignment = VerticalAlignment.Top;
                button.Width = 40;
                button.Height = 20;
                button.Margin = new Thickness(h, v, 0, 0);
                button.Content = "Slot " + (i + 1);
                button.Click += button_click;
                button.Name = "button" + (i + 1);
                gameButton[i] = button;
                gameBoardCont.Children.Add(gameButton[i]);

                h += 5 + (int)button.Width;
            }
            v += 25;
            h = 5;

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

        private async void button_click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < gameButton.Length; i++)
                gameButton[i].IsEnabled = false;

            Random rnd = new Random();
            int counter = 0;
            int rowCounter = 0;
            int prevPlace = 0;
            int newPlace = 0;
            Ellipse ball = new Ellipse();

            switch (sender.ToString().Split(':')[1]) 
            {
                case " Slot 1":
                    counter = 0;
                    rowCounter = 0;
                    prevPlace = 0;
                    newPlace = 0;
                    while (counter < gameBoard.Length) 
                    {
                        await Task.Delay(500);

                        int x = rnd.Next() % 2;

                        if (counter < 1)
                        {
                            ball.VerticalAlignment = VerticalAlignment.Top;
                            ball.HorizontalAlignment = HorizontalAlignment.Left;                   
                            ball.Width = 20;
                            ball.Height = 20;
                            ball.Fill = new SolidColorBrush(Colors.Black);
                            gameBoard[rowCounter][0].Content = ball;
                            gameBoard[rowCounter][0].VerticalContentAlignment = VerticalAlignment.Center;
                            gameBoard[rowCounter][0].HorizontalContentAlignment = HorizontalAlignment.Center;
                            prevPlace = 0;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace >= gameBoard[0].Length - 1)
                                {
                                    newPlace = prevPlace - 1;

                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace + 1;

                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }                              
                            }
                            else if (x == 0)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace == 0)
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }                               
                            }
                        }

                        counter++;
                        rowCounter++;
                        
                    }
                    
                    break;

                case " Slot 2":
                    counter = 0;
                    rowCounter = 0;
                    prevPlace = 0;
                    newPlace = 0;
                    while (counter < gameBoard.Length)
                    {
                        await Task.Delay(500);

                        int x = rnd.Next() % 2;

                        if (counter < 1)
                        {
                            ball.VerticalAlignment = VerticalAlignment.Top;
                            ball.HorizontalAlignment = HorizontalAlignment.Left;
                            ball.Width = 20;
                            ball.Height = 20;
                            ball.Fill = new SolidColorBrush(Colors.Black);
                            gameBoard[rowCounter][1].Content = ball;
                            gameBoard[rowCounter][1].VerticalContentAlignment = VerticalAlignment.Center;
                            gameBoard[rowCounter][1].HorizontalContentAlignment = HorizontalAlignment.Center;
                            prevPlace = 1;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace >= gameBoard[0].Length - 1)
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                            else if (x == 0)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace == 0)
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                        }

                        counter++;
                        rowCounter++;
                    }
                    
                    break;
                case " Slot 3":
                    counter = 0;
                    rowCounter = 0;
                    prevPlace = 0;
                    newPlace = 0;
                    while (counter < gameBoard.Length)
                    {
                        await Task.Delay(500);

                        int x = rnd.Next() % 2;

                        if (counter < 1)
                        {
                            ball.VerticalAlignment = VerticalAlignment.Top;
                            ball.HorizontalAlignment = HorizontalAlignment.Left;
                            ball.Width = 20;
                            ball.Height = 20;
                            ball.Fill = new SolidColorBrush(Colors.Black);
                            gameBoard[rowCounter][2].Content = ball;
                            gameBoard[rowCounter][2].VerticalContentAlignment = VerticalAlignment.Center;
                            gameBoard[rowCounter][2].HorizontalContentAlignment = HorizontalAlignment.Center;
                            prevPlace = 2;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace >= gameBoard[0].Length - 1)
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                            else if (x == 0)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace == 0)
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                        }
                        counter++;
                        rowCounter++;
                    }
                    
                    break;
                case " Slot 4":
                    counter = 0;
                    rowCounter = 0;
                    prevPlace = 0;
                    newPlace = 0;
                    while (counter < gameBoard.Length)
                    {
                        await Task.Delay(500);

                        int x = rnd.Next() % 2;

                        if (counter < 1)
                        {
                            ball.VerticalAlignment = VerticalAlignment.Top;
                            ball.HorizontalAlignment = HorizontalAlignment.Left;
                            ball.Width = 20;
                            ball.Height = 20;
                            ball.Fill = new SolidColorBrush(Colors.Black);
                            gameBoard[rowCounter][3].Content = ball;
                            gameBoard[rowCounter][3].VerticalContentAlignment = VerticalAlignment.Center;
                            gameBoard[rowCounter][3].HorizontalContentAlignment = HorizontalAlignment.Center;
                            prevPlace = 3;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace >= gameBoard[0].Length - 1)
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                            else if (x == 0)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace == 0)
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                        }

                        counter++;
                        rowCounter++;
                    }
                    
                    break;
                case " Slot 5":
                    counter = 0;
                    rowCounter = 0;
                    prevPlace = 0;
                    newPlace = 0;
                    while (counter < gameBoard.Length)
                    {
                        await Task.Delay(500);

                        int x = rnd.Next() % 2;

                        if (counter < 1)
                        {
                            ball.VerticalAlignment = VerticalAlignment.Top;
                            ball.HorizontalAlignment = HorizontalAlignment.Left;
                            ball.Width = 20;
                            ball.Height = 20;
                            ball.Fill = new SolidColorBrush(Colors.Black);
                            gameBoard[rowCounter][4].Content = ball;
                            gameBoard[rowCounter][4].VerticalContentAlignment = VerticalAlignment.Center;
                            gameBoard[rowCounter][4].HorizontalContentAlignment = HorizontalAlignment.Center;
                            prevPlace = 4;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace >= gameBoard[0].Length - 1)
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }


                            }
                            else if (x == 0)
                            {
                                gameBoard[rowCounter - 1][prevPlace].Content = "";

                                if (prevPlace == 0)
                                {
                                    newPlace = prevPlace + 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                                else
                                {
                                    newPlace = prevPlace - 1;
                                    ball.VerticalAlignment = VerticalAlignment.Top;
                                    ball.HorizontalAlignment = HorizontalAlignment.Left;
                                    ball.Width = 20;
                                    ball.Height = 20;
                                    ball.Fill = new SolidColorBrush(Colors.Black);
                                    gameBoard[rowCounter][newPlace].Content = ball;
                                    gameBoard[rowCounter][newPlace].VerticalContentAlignment = VerticalAlignment.Center;
                                    gameBoard[rowCounter][newPlace].HorizontalContentAlignment = HorizontalAlignment.Center;
                                    prevPlace = newPlace;
                                }
                            }
                        }
                        counter++;
                        rowCounter++;
                    }
  
                    break;

            }

            showPrizeMessage();

        }
    }
}
