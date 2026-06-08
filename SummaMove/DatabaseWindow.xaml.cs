using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SummaMove
{
    public partial class DatabaseWindow : Window
    {
        string connStr = "server=localhost;user=root;password=;database=summamovedatabase;";

        public DatabaseWindow()
        {
            InitializeComponent();

            LoadTable("SELECT * FROM users", UsersGrid);
            LoadTable("SELECT * FROM challenges", ChallengesGrid);
            LoadTable("SELECT * FROM user_challenges", UserChallengesGrid);
            LoadTable("SELECT * FROM friends", FriendsGrid);
            LoadTable("SELECT * FROM leaderboard_entries", LeaderboardGrid);
            LoadTable("SELECT * FROM store_items", StoreItemsGrid);
            LoadTable("SELECT * FROM user_items", UserItemsGrid);
        }

        private void LoadTable(string query, DataGrid grid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    grid.ItemsSource = table.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}