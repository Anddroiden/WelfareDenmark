using System.Collections.Generic;

namespace WelfareDenmark.Models {
    public class BrainGame {
        public BrainGame() {
            GameResults = new HashSet<GameResult>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameResult> GameResults { get; set; }
    }
}