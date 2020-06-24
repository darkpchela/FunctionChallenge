using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionChallenge.Models
{
    public class UserData
    {
        [Key]
        public int UserDataId { get; set; }
        public int RangeFrom { get; set; }
        public int RangeTo { get; set; }
        public double Step { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }
}
