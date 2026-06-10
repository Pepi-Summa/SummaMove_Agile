using MySql.Data.MySqlClient;
using System;
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

            LoadTable("SELECT * FROM challenges", ChallengesGrid);
            LoadTable("SELECT * FROM store_items", StoreItemsGrid);
        }

        private void LoadTable(string query, DataGrid grid)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();

                adapter.Fill(table);

                grid.ItemsSource = table.DefaultView;
            }
        }

        private void AddChallenge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connStr))
                {
                    conn.Open();

                    string query =
                    @"INSERT INTO challenges
            (difficulty, name, description, point_reward)
            VALUES
            (@difficulty, @name, @description, @reward)";

                    MySqlCommand cmd =
                        new MySqlCommand(query, conn);

                    ComboBoxItem selectedDifficulty =
                        (ComboBoxItem)DifficultyBox.SelectedItem;

                    cmd.Parameters.AddWithValue(
                        "@difficulty",
                        selectedDifficulty.Content.ToString()
                    );

                    cmd.Parameters.AddWithValue(
                        "@name",
                        ChallengeNameBox.Text
                    );

                    cmd.Parameters.AddWithValue(
                        "@description",
                        ChallengeDescriptionBox.Text
                    );

                    cmd.Parameters.AddWithValue(
                        "@reward",
                        int.Parse(ChallengeRewardBox.Text)
                    );

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Challenge added");

                    LoadTable(
                        "SELECT * FROM challenges",
                        ChallengesGrid
                    );

                    DifficultyBox.SelectedIndex = -1;
                    ChallengeNameBox.Clear();
                    ChallengeDescriptionBox.Clear();
                    ChallengeRewardBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddStoreItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connStr))
                {
                    conn.Open();

                    string query =
                    @"INSERT INTO store_items
            (name, product_type)
            VALUES
            (@name, @type)";

                    MySqlCommand cmd =
                        new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue(
                        "@name",
                        StoreItemNameBox.Text
                    );

                    ComboBoxItem selected =
                        (ComboBoxItem)ProductTypeBox.SelectedItem;

                    cmd.Parameters.AddWithValue(
                        "@type",
                        selected.Content.ToString()
                    );

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Store item added");

                    LoadTable(
                        "SELECT * FROM store_items",
                        StoreItemsGrid
                    );

                    StoreItemNameBox.Clear();
                    ProductTypeBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}