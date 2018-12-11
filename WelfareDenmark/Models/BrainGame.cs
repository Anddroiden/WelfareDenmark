using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.Models {
    public class BrainGame {
        public BrainGame() {
            GameResults = new HashSet<GameResult>();
        }

        [DisplayName("Resultat nummer:")]
        public long Id { get; set; }

        [Required]
        [DisplayName("Navn på spil:")]
        public string Name { get; set; }

        [DisplayName("Spil resultat:")]
        public ICollection<GameResult> GameResults { get; set; }
    }
}