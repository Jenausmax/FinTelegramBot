
namespace FinBot.Domain.Models
{
    public class Consumption
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public double Money { get; set; }
        public int CategoryId { get; set; }
    }
}
