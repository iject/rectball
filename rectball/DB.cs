using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace rectball
{
    public class DB
    {
        private static string connStr = "Server=localhost;DataBase=ball_and_rect;port=3306;User Id=root;password=";
        private static MySqlConnection conn = new MySqlConnection(connStr);
        private static MySqlCommand cmd = conn.CreateCommand();
            


        public DB()
        { 
            conn.Open();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS `ScoreRects` (" +
                              "Id int not null primary key, " +
                              "Score int not null);";
            cmd.ExecuteNonQuery();
        }

        public void InsertNew(int id, int score)
        {
            cmd.CommandText = "INSERT INTO `ScoreRects` (Id, Score) " +
                              $"Values ({id}, {score});";
            cmd.ExecuteNonQuery();
        }

        public void Update(int id, int score)
        {
            cmd.CommandText = $"UPDATE `ScoreRects` SET Score = {score} " +
                              $"WHERE Id = {id};";
            cmd.ExecuteNonQuery();
        }

        public void Truncate()
        {
            cmd.CommandText = "TRUNCATE TABLE `ScoreRects`;";
            cmd.ExecuteNonQuery();
        }
    }
}
