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
        private Label[][] gameBoard = new Label[10][]; 
        private Grid gameBoardCont = new Grid();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void initBoard() 
        {
            gameBoardCont = new Grid();
            gameBoardCont.VerticalAlignment = VerticalAlignment.Top;
            gameBoardCont.HorizontalAlignment = HorizontalAlignment.Left;
            gameBoardCont.Width = 800;
            gameBoardCont.Height = 800;
        }
    }
}
