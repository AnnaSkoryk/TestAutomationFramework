namespace APITests.Models
{
    public class Brand
    {
        public int id { get; set; }
        public string brand { get; set; }
    }

    public class Brands : IModel
    {
        public int responseCode { get; set; }
        public List<Brand> brands { get; set; }
    }

}
