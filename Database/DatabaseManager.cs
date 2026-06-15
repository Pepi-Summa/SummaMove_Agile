using MySql.Data.MySqlClient;
using SummaMove.Models;
using System;
using System.Collections.Generic;

namespace SummaMove.Database
{
    public class DatabaseManager
    {
        private readonly string connStr =
            "server=localhost;user=root;password=;database=summamovedatabase;";

        private MySqlConnection CreateConnection()
        {
            return new MySqlConnection(connStr);
        }

        public List<Challenge> GetChallenges()
        {
            List<Challenge> challenges = new();

            using (var conn = CreateConnection())
            {
                conn.Open();

                string query = "SELECT * FROM challenges LIMIT 3";

                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    challenges.Add(new Challenge
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
            using var conn = CreateConnection();
            conn.Open();

            string query = @"
                INSERT INTO user_challenges (user_id, challenge_id, completed)
                VALUES (@userId, @challengeId, false)";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@challengeId", challengeId);

            cmd.ExecuteNonQuery();
        }

        public void CompleteChallenge(int userId, int challengeId)
        {
            using var conn = CreateConnection();
            conn.Open();

            // 1. Check of challenge bestaat en status ophalen
            string checkQuery = @"
                SELECT completed
                FROM user_challenges
                WHERE user_id = @userId
                AND challenge_id = @challengeId
                LIMIT 1";

            using var checkCmd = new MySqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@userId", userId);
            checkCmd.Parameters.AddWithValue("@challengeId", challengeId);

            object checkResult = checkCmd.ExecuteScalar();

            if (checkResult == null)
            {
                throw new Exception("Start deze challenge eerst.");
            }

            bool alreadyCompleted = Convert.ToBoolean(checkResult);

            if (alreadyCompleted)
            {
                throw new Exception("Challenge is al voltooid.");
            }

            // 2. Reward ophalen
            string rewardQuery = @"
                SELECT point_reward
                FROM challenges
                WHERE id = @challengeId";

            using var rewardCmd = new MySqlCommand(rewardQuery, conn);
            rewardCmd.Parameters.AddWithValue("@challengeId", challengeId);

            object rewardResult = rewardCmd.ExecuteScalar();

            int reward = Convert.ToInt32(rewardResult ?? 0);

            // 3. Challenge afronden
            string completeQuery = @"
                UPDATE user_challenges
                SET completed = true,
                    completed_at = NOW()
                WHERE user_id = @userId
                AND challenge_id = @challengeId";

            using var completeCmd = new MySqlCommand(completeQuery, conn);
            completeCmd.Parameters.AddWithValue("@userId", userId);
            completeCmd.Parameters.AddWithValue("@challengeId", challengeId);

            completeCmd.ExecuteNonQuery();

            // 4. Points toevoegen
            string pointsQuery = @"
                UPDATE users
                SET points = points + @reward
                WHERE id = @userId";

            using var pointsCmd = new MySqlCommand(pointsQuery, conn);
            pointsCmd.Parameters.AddWithValue("@reward", reward);
            pointsCmd.Parameters.AddWithValue("@userId", userId);

            pointsCmd.ExecuteNonQuery();
        }
        public class AuthService
        {
            private string connStr =
                "server=localhost;user=root;password=;database=summamovedatabase;";

            public void AutoLogin()
            {
                using var conn = new MySqlConnection(connStr);
                conn.Open();

                string query = @"
                SELECT id, username, points
                FROM users
                WHERE id = 1
                LIMIT 1";

                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CurrentUser.UserId = Convert.ToInt32(reader["id"]);
                    CurrentUser.Username = reader["username"].ToString();
                    CurrentUser.Points = Convert.ToInt32(reader["points"]);
                }
            }
        }
    }
}