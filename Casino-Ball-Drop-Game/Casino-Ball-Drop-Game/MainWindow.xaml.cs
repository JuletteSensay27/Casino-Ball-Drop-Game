using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            foreach(Button btn in gameButton)
                btn.IsEnabled = false;
            
            /*
             * For testing only
             */

            balanceLbl.Content += " 10000";
        }

        private void showPrizeMessage() 
        {
            int winInd = 0;
            for (int i = 0; i < gameBoard[6].Length; i++) 
            {
                if (gameBoard[6][i].Content != null) 
                {
                    winInd = i;
                    break;
                }
            }

            switch (winInd) 
            {
                case 0:
                    MessageBox.Show("Slot 1!");
                    break;
                case 1:
                    MessageBox.Show("Slot 2!");
                    break;
                case 2:
                    MessageBox.Show("Slot 3!");
                    break;
                case 3:
                    MessageBox.Show("Slot 4!");
                    break;
                case 4:
                    MessageBox.Show("Slot 5!");
                    break;
                case 5:
                    MessageBox.Show("Slot 6!");
                    break;
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

        private void addValueBtn_Click(object sender, RoutedEventArgs e)
        {
            int toInc = int.Parse(wagerTbx.Text);
            toInc++;
            wagerTbx.Text = toInc.ToString();
        }

        private void subValueBtn_Click(object sender, RoutedEventArgs e)
        {
            int toDec = int.Parse(wagerTbx.Text);

            if (toDec <= 0)
                wagerTbx.Text = "0";
            else 
            {
                toDec--;
                wagerTbx.Text = toDec.ToString();
            }
        }

        private void confirmWagerBtn_Click(object sender, RoutedEventArgs e)
        {
            string rawWager = wagerTbx.Text;
            Regex pattern = new Regex(@"^[0-9]+$");

            if (wagerTbx.Text.Length < 1)
            {
                wagerTbx.Text = "0";
                wagerTbx.Select(wagerTbx.Text.Length, 0);
            }

            if (!pattern.IsMatch(rawWager))
            {
                MessageBox.Show("Invalid Amount!");
            }
            else
            {
                int playWager = int.Parse(rawWager);
                int playBal = int.Parse(balanceLbl.Content.ToString().Split(':')[1]);

                if (playWager == 0)
                    MessageBox.Show("You cannot play with no bet!");
                else
                {

                    int result = playWager > playBal ? 1 : 0;

                    switch (result) 
                    {
                        case 0:
                            MessageBox.Show("Play on!");
                            foreach (Button btn in gameButton)
                                btn.IsEnabled = true;
                            confirmWagerBtn.IsEnabled = false;
                            playBal -= playWager;
                            balanceLbl.Content = balanceLbl.Content.ToString().Split(':')[0] + ": " + playBal;

                            break;
                        case 1:
                            MessageBox.Show("Insufficient Funds");
                            break;
                    }
                }
                wagerTbx.Text = "0";
              
            }
        }
    }
}
