namespace Sealite.TrainAssessment;

public class SimpleRecursiveRoutePlanner : IRoutePlanner
{

    public SimpleRecursiveRoutePlanner()
    {

    }

    public IEnumerable<Route> CreateRoutes(Town startTown, int maxLinks)
    {
        var links = new List<Link>();
        return CreateRoutes(startTown, links, maxLinks - 1);
    }

    private IEnumerable<Route> CreateRoutes(Town startTown, IEnumerable<Link> previousLinks, int maxLinks)
    {
        foreach (var newLink in startTown.Links)
        {
            var currentLinks = previousLinks.Append(newLink).ToList();
            var route = new Route(currentLinks);
            yield return route;
            if (maxLinks > 0)
            {
                foreach (var subRoutes in CreateRoutes(route.EndTown, currentLinks, maxLinks - 1))
                {
                    yield return subRoutes;
                }
            }
        }
    }
}
