namespace HahnCargoSim.Models.Grid;
public class ConnectionEdge
{
    public int Id { get; set; }
    public int Cost { get; set; }
    public TimeSpan Time { get; set; }
    public Connection Connection { get; set; }
}
