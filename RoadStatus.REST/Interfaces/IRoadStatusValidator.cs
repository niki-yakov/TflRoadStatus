using RoadStatus.REST.Models;

namespace RoadStatus.REST.Interfaces
{
    public interface IRoadStatusValidator
    {
        IPrint Print  { get; }
        NotFoundError HttpNotFoundError { get; set; }
        CorridorStatus CorridorStatus { get; set; }
        int GetRoadCurrentStatus(string road);
    }
}