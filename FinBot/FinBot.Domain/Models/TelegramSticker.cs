using FinBot.Domain.Models.Entities;

namespace FinBot.Domain.Models
{
    public class TelegramSticker : Entity
    {
        public string Name { get; set; }
        public string FileidTelegram { get; set; }
        public StickerRole? StickerRole { get; set; }
        public bool IsAnimated { get; set; }
        public string Emoji { get; set; }
        
    }

    public enum StickerRole
    {
        Hi = 1,
        GoodMorning = 2,
        GoodNight = 3,
        Good = 4,
        Error = 5
    }
}
