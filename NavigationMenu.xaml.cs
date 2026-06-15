
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

namespace SummaMove
{
    /// <summary>
    /// Interaction logic for NavigationMenu.xaml
    /// </summary>
    public partial class NavigationMenu : UserControl
    {
        public NavigationMenu()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Homepagina home = new Homepagina();
            home.Show();
        }

        private void ChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            ChallengeOverzicht challenge = new ChallengeOverzicht();
            challenge.Show();
        }

        private void ProfielButton_Click(object sender, RoutedEventArgs e)
        {
            profiel profielPagina = new profiel();
            profielPagina.Show();
        }

    }
}
