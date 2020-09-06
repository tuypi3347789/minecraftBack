using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using WebApplication1.Model;
using System.Security.Cryptography;
using System.Collections;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private static string HMACSHA256(string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }
        private readonly string sql;

        private Sql Sql { get; set; }
        public PlayerController(Sql sql)
        {
            this.Sql = sql;
        }
        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [EnableCors("test")]
        [HttpPost("login")]
        public List<Player> LoginPost([FromBody] Player player)
        {
            player.playerPassword = HMACSHA256(player.playerPassword, "Min@!ecr@aft#");
            string connectionString = "server=localhost; database=minecraft; uid=root; pwd=User_123;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandText = @"SELECT * FROM player WHERE `player_account` = @account AND `player_password` = @password";
            cmd.Parameters.AddWithValue("@account", player.playerAccount);
            cmd.Parameters.AddWithValue("@password", player.playerPassword);
            List<Player> list = new List<Player>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Player play = new Player();
                    play.playerAccount = HMACSHA256(reader["player_account"].ToString(), "acc!ount@9970#85$74t1%");
                    play.playerName = reader["player_name"].ToString();
                    list.Add(play);
                }
            }
            return list;
        }

        // POST api/<PlayerController>
        [EnableCors("test")]
        [HttpPost("register")]
        public int Post([FromBody] Player player)
        {
            player.playerPassword = HMACSHA256(player.playerPassword, "Min@!ecr@aft#");
            string connectionString = "server=localhost; database=minecraft; uid=root; pwd=User_123;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandText = @"SELECT * FROM player WHERE `player_name` = @name";
            cmd.Parameters.AddWithValue("@name", player.playerName);
            bool dr = cmd.ExecuteReader().HasRows;
            conn.Close();
            if (dr == true)
            {
                return 1;
            }
            else
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.CommandText = @"SELECT * FROM player WHERE `player_email` = @email";
                cmd.Parameters.AddWithValue("@email", player.playerEmail);
                bool er = cmd.ExecuteReader().HasRows;
                conn.Close();
                if (er == true)
                {
                    return 2;
                }
                else
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    cmd = new MySqlCommand(sql, conn);
                    cmd.CommandText = @"INSERT INTO player (`uuid`, `player_name`, `player_account`, `player_password`, `player_email`) VALUES (@UUID, @name, @account, @password, @email)";
                    cmd.Parameters.AddWithValue("@UUID", player.UUID);
                    cmd.Parameters.AddWithValue("@name", player.playerName);
                    cmd.Parameters.AddWithValue("@account", player.playerAccount);
                    cmd.Parameters.AddWithValue("@password", player.playerPassword);
                    cmd.Parameters.AddWithValue("@email", player.playerEmail);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    whiteListAdd.Main(player.UUID, player.playerName);
                    return 3;
                }
            }
            return 0;
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
