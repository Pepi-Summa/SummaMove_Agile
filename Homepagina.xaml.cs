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
        private string connStr ="server=localhost;user=root;password=;database=summamovedatabase;";
        private DatabaseManager db = new();
        private int currentUserId = 1; /// testaccount om aan de klant te tonen

        public Homepagina()
        {
            InitializeComponent();

            LoadChallenges();
        }


        private void LoadChallenges()
        {
            ChallengesList.ItemsSource = db.GetChallenges();
        }

        private void StartChallenge_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int challengeId = Convert.ToInt32(button.Tag);

            db.StartChallenge(currentUserId, challengeId);

            MessageBox.Show("Challenge gestart!");
        }

        private void CompleteChallenge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;

                MessageBox.Show($"Tag = {button.Tag}");

                int challengeId = Convert.ToInt32(button.Tag);

                db.CompleteChallenge(currentUserId, challengeId);

                MessageBox.Show("Challenge voltooid!");

                LoadChallenges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int GetUserPoints()
        {
            using (MySqlConnection conn =
                new MySqlConnection(
                "server=localhost;user=root;password=;database=summamovedatabase;"))
            {
                conn.Open();

                string query =
                    "SELECT points FROM users WHERE id = @id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", currentUserId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

    }
}
