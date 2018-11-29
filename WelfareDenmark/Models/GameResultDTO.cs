using System;
using System.ComponentModel.DataAnnotations;

namespace WelfareDenmark.Models {
    public class GameResultDTO {
        [Display(Name = "Game Title")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "The Game Title must be between 5 and 100 characters long")]
        public string Name { get; set; }


        [Required]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double Score { get; set; }


        [Display(Name = "Date and Time")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}