using System;
using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.Models {
    public class GameResult {
        [Required]
        public long Id { get; set; }
        [Required]
        [Range(0,10000)]
        public double Score { get; set; }
        [Required]
        [StringLength(100)]
        public string Player { get; set; }
        [Required]
        public BrainGame BrainGame { get; set; }
        public DateTime DateTime { get; set; }
    }
}