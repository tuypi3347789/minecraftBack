using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private Sql Sql { get; set; }
        public TasksController(Sql sql)
        {
            this.Sql = sql;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [EnableCors("test")]
        [HttpPost]
        public void Post()
        {
            var cmd = this.Sql.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE test SET msg = @str WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", 1);
            cmd.Parameters.AddWithValue("@str", 4456);
            cmd.ExecuteNonQuery();
            Sql.Dispose();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
