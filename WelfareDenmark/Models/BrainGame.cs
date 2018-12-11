using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.Models {
    public class BrainGame {
        public BrainGame() {
            GameResults = new HashSet<GameResult>();
        }

        [DisplayName("Result number:")]
        public long Id { get; set; }

        [Required]
        [DisplayName("Game name:")]
        public string Name { get; set; }

        [DisplayName("Games result:")]
        public ICollection<GameResult> GameResults { get; set; }
    }
}