using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.Models
{
    public interface IModel
    {
        int responseCode { get; set; }
    }
}
