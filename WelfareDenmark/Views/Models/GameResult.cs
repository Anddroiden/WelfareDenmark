using System;

namespace WelfareDenmark.Models
{
    public class GameResult
    {
        public long Id { get; set; }
        public double Score { get; set; }
        public string Player { get; set; }
        public BrainGame BrainGame { get; set; }
    }
        public DateTime dateTime { get; set; }
		
		}
}