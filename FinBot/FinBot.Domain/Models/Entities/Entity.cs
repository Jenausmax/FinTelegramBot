using System.ComponentModel.DataAnnotations;
using FinBot.Domain.Interfaces;

namespace FinBot.Domain.Models.Entities
{
    public class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
