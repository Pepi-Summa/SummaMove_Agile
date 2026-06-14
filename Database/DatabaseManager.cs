using MySql.Data.MySqlClient;
using SummaMove.Models;

namespace SummaMove.Database
{
    public class DatabaseManager
    {
        private string connStr =
            "server=localhost;user=root;password=;database=summamovedatabase;";

        public List<Challenge> GetChallenges()
        {
            List<Challenge> challenges = new();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query =
                    "SELECT * FROM challenges LIMIT 3";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                MySqlDataReader reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    challenges.Add(new Challenge()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Difficulty = reader["difficulty"].ToString(),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        PointReward = Convert.ToInt32(reader["point_reward"])
                    });
                }
            }

            return challenges;
        }

        public void StartChallenge(int userId, int challengeId)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connStr))
            {
                conn.Open();

                string query =
                @"INSERT INTO user_challenges
                (user_id, challenge_id, completed)
                VALUES
                (@userId, @challengeId, false)";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@challengeId", challengeId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}