using System.ComponentModel.DataAnnotations;

namespace IChibanGameServer.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public string LotId { get; set; }
        public string LotBoxId { get; set; }
        [ConcurrencyCheck]
        public string? OwnerId { get; set; }
        public string Level { get; set; }
        public int PositionIndex { get; set; }
        [ConcurrencyCheck]
        public long FinishedDateTime { get; set; }
    }
}
