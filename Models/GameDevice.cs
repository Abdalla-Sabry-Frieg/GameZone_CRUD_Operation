namespace GameZone.Models
{
    public class GameDevice
    {
        //make a composite key to achevie to the relation many to many 
        public int GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int DeviceId { get; set; }
        public Device Device { get; set; } = default!;
    }
}
