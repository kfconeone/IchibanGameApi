namespace IChibanGameServer.ViewModels
{
    public class LotDrawResultModel
    {
        
        public string LotBoxId{ get; set; }
        public GameType Type { get; set; }
        public long LockEndTime { get; set; }
        public ResponseLotModel[] Lots { get; set; }
    }


    public class ResponseLotModel {
        public string LotId { get; set; }
        public string OwnerId { get; set; }
        public string Level { get; set; }
    }
}
