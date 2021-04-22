
namespace FinBot.Domain.Models
{
    /// <summary>
    /// Вид категории. Income - доход, Consumption - расход.
    /// </summary>
    public enum CategoryView
    {
        Income,
        Consumption
    }

    public class Category
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public string Date { get; set; }
        public double Money { get; set; }
        public CategoryView CategoryView { get; set; }
    }
}
