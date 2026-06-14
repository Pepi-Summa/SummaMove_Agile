using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using SummaMove.Database;
using SummaMove.Models;

namespace SummaMove
{
    /// <summary>
    /// Interaction logic for Homepagina.xaml
    /// </summary>
    public partial class Homepagina : Window
    {
        private DatabaseManager db = new();
        private int currentUserId = 1;

        public Homepagina()
        {
            InitializeComponent();

            LoadChallenges();
        }


        private void LoadChallenges()
        {
            var challenges = db.GetChallenges();

            ChallengesList.Items.Clear();

            foreach (var challenge in challenges)
            {
                ChallengesList.Items.Add(
                    $"{challenge.Name} | " +
                    $"{challenge.Difficulty} | " +
                    $"{challenge.PointReward} punten");
            }
        }

        private void StartChallenge_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int challengeId = Convert.ToInt32(button.Tag);

            db.StartChallenge(currentUserId, challengeId);

            MessageBox.Show("Challenge gestart!");
        }

    }
}
