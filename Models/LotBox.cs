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

        [ConcurrencyCheck]
        public long LockEndTime { get; set; }

        public GameType Type { get; set; }
    }
}
