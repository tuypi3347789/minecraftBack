using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using WebApplication1.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private Sql Sql { get; set; }
        private Sql SSql { get; set; }
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

        // POST api/<PlayerController>
        [EnableCors("test")]
        [HttpPost("register")]
        public int Post([FromBody] Player player)
        {
            var cmd = this.Sql.Connection.CreateCommand() as MySqlCommand;
            var emd = this.Sql.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM player WHERE `player_name` = @name";
            cmd.Parameters.AddWithValue("@name", player.playerName);
            bool dr = cmd.ExecuteReader().HasRows;
            Console.WriteLine("123");
            if (dr == true)
            {
                return 1;
            } 
            else
            {
                emd.CommandText = @"SELECT * FROM player WHERE `player_email` = @email";
                emd.Parameters.AddWithValue("@email", player.playerEmail);
                bool er = emd.ExecuteReader().HasRows;
                if (er == true)
                { 
                    return 2; 
                } 
                else
                {
                    return 3;
                }
            }
            Sql.Dispose();
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
