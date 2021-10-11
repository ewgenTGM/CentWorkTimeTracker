using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class ResponseModel<T>
    {
        public bool Ok { get; set; } = true;
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
    }
}