using System.Collections.Generic;
using FinBot.Domain.Models.Entities;
namespace FinBot.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        /// <summary>
        /// true - Consumption, false - Income
        /// </summary>
        public CategoryRole Role { get; set; }
        public bool IsDelete { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Consumption> Consumptions { get; set; }
        public List<Income> Incomes { get; set; }

    }

    public enum CategoryRole
    {
        Income = 1,
        Consumption = 2
            
    }
}
