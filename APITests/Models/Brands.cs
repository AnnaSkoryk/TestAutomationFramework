using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.Models
{
    public class Brand
    {
        public int id { get; set; }
        public string brand { get; set; }
    }

    public class Brands
    {
        public int responseCode { get; set; }
        public List<Brand> brands { get; set; }
    }

}
