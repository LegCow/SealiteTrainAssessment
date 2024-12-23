namespace Sealite.TrainAssessment;

public interface IRoutePlanner
{
    IEnumerable<Route> CreateRoutes(Town startTown, int maxLinks);
}
