using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeaBattle.Models
{
    [Table(nameof(UserStats))]
    public class UserStats
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int TotalPlayTime { get; set; }
        public int LongestTimePlayed { get; set; }
        public int ShortestTimePlayed { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}