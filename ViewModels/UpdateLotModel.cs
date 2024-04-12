using System.ComponentModel.DataAnnotations;

namespace IChibanGameServer.ViewModels
{

    
    public class UpdateLotModel
    {
        [Required(ErrorMessage = "UserId 是必須的")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "PositionIndexs 是必須的")]
        public int[] PositionIndexs { get; set; }

        [Required(ErrorMessage = "Type 是必須的")]
        public GameType Type { get; set; }
    }
}
