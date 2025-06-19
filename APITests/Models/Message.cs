using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.Models
{
    public class Message : IModel
    {
        public int responseCode { get; set; }
        public string message { get; set; }
    }
}
