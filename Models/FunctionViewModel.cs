using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FunctionChallenge.Models
{
    public class FunctionViewModel
    {
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int a { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int b { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int c { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Value should be between 1 and 100")]
        public int step { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int from { get; set; }
        [Required]
        [Range(-100, 100, ErrorMessage = "Value should be between -100 and 100")]
        public int to { get; set; }
        public string points { get; set; }
    }
}
