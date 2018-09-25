using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmark.Models
{
    public class BrainGame
    {
        public string Name { get; set; }
        public IEnumerable<GameResult> Results { get; set; }
    }
}
