using IChibanGameServer.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace IChibanGameServer.Models
{
    public class LotBox
    {

        public int Id { get; set; }
        
        public string BoxId { get; set; }

        [ConcurrencyCheck]
        public string? LastUserId { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        //沒有甚麼好的實作想法, 但不推薦下面這個作法
        //public string[] Tags { get; set; }

        public string CoverImageUrl { get; set; }

        //JSON字串?或是用逗號分隔的字串?
        public string DisplayImageUrls { get; set; }

        public int Price { get; set; }

        //SHA-256加密用的原始字串
        public string PrizePositions { get; set; }
        //SHA-256鹽值
        public string secretKey { get; set; }
        //SHA-256加密後的雜湊值
        public string hash { get; set; }



        [ConcurrencyCheck]
        public DateTime LockEndTime { get; set; }

        public GameType Type { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public DateTime OpenTime { get; set; }
        
        public DateTime CloseTime { get; set; }

        
    }
}
