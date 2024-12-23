namespace Sealite.TrainAssessment;

public class Town
{
    public TownCode TownCode { get; }

    private readonly List<Link> _links = new();
    public IReadOnlyList<Link> Links => _links.AsReadOnly();

    
    public Town(char townChar)
    {
        TownCode = TownCode.Create(townChar);
    }

    public void AddConnection(Link route)
    {
        if (route.StartTown != this)
            throw new InvalidOperationException("Connections must originate from this town");

        _links.Add(route);
    }

    public Link GetLinkTo(Town destination)
    {
        var link = _links.SingleOrDefault(x => x.StartTown == this && x.EndTown == destination);

        if (link is null)
            throw new InvalidOperationException($"A link from town {TownCode} and town {destination.TownCode} does not exist.");

        return link;
    }

    public Route TravelTo(Town destination)
    {
        var link = GetLinkTo(destination);
        var route = new Route(link);
        return route;
    }

    public override string ToString() => TownCode.ToString();
    public override bool Equals(object? obj) => GetHashCode().Equals(obj?.GetHashCode());
    public override int GetHashCode() => TownCode.Value.GetHashCode();
}