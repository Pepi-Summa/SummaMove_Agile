using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaMove.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Difficulty { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointReward { get; set; }
    }
}
