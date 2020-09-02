using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Player
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("uuid")]
        public string UUID { get; set; }
        [Column("player_name")]
        public string playerName { get; set; }
        [Column("player_account")]
        public string playerAccount { get; set; }
        [Column("player_password")]
        public string playerPassword { get; set; }
        [Column("player_email")]
        public string playerEmail { get; set; }
    }
}
