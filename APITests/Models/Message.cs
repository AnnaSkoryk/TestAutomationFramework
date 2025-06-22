namespace APITests.Models
{
    public class Message : IModel
    {
        public int responseCode { get; set; }
        public string message { get; set; }
    }
}
