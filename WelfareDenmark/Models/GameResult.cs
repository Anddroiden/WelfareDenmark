using System;
using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.Models {
    public class GameResult {
        public long Id { get; set; }
        [Required]
        [Range(0,10000)]
        public double Score { get; set; }
        [Required]
        [StringLength(100)]
        public string Player { get; set; }
        public BrainGame BrainGame { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}