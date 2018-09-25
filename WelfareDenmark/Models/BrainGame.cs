using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmark.Models
{
    public class BrainGame
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<GameResult> Results { get; set; } = new List<GameResult>();
    }
}
