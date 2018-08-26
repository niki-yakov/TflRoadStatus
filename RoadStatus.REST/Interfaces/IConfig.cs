namespace RoadStatus.REST.Interfaces
{
    public interface IConfig
    {
        string Url { get; set; }
        string AppID { get; set; }
        string AppKey { get; set; }
    }
}