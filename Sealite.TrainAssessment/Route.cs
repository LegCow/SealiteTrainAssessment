using System.Text;

namespace Sealite.TrainAssessment;

public class Route
{
    private readonly List<Link> _links = new();
    public IReadOnlyList<Link> Links => _links.AsReadOnly();
    public Town StartTown => _links.First().StartTown;
    public Town EndTown => _links.Last().EndTown;
    public int TotalStops => _links.Count;
    public int TotalLength => _links.Sum(x => x.Distance.Value);

    public Route(Link link)
    {
        _links.Add(link);
    }

    public Route(IEnumerable<Link> links)
    {
        _links.AddRange(links);
    }

    public Route TravelTo(Town town)
    {
        var copyLinks = _links.ToList();
        var newLink = EndTown.GetLinkTo(town);
        copyLinks.Add(newLink);

        return new Route(copyLinks);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var displayStart = true;

        foreach (var route in _links)
        {
            if (displayStart)
            {
                sb.Append($"{route.StartTown}");
                displayStart = false;
            }
            sb.Append($"=>{route.EndTown}");
        }

        sb.Append($" ({TotalLength})");
        return sb.ToString();
    }
    public override bool Equals(object? obj)
    {
        return GetHashCode().Equals(obj?.GetHashCode());
    }
    public override int GetHashCode()
    {
        int hashCode = 0;
        foreach (var link in _links)
        {
            hashCode = HashCode.Combine(hashCode, link);
        }
        return hashCode;
    }
}