namespace Sealite.TrainAssessment;

public class TrainNetwork
{
    private readonly List<Town> _towns = new();
    public IReadOnlyList<Town> Towns => _towns.AsReadOnly();

    public Town GetTown(char townChar)
    {
        var townCode = TownCode.Create(townChar);
        var town = _towns.SingleOrDefault(t => t.TownCode.Equals(townCode));
        if (town is null) throw new InvalidOperationException($"Town with code '{townChar}' was not found.");
        return town;
    }

    public Town GetOrCreateTown(char townChar)
    {
        var townCode = TownCode.Create(townChar);
        var town = _towns.SingleOrDefault(t => t.TownCode.Equals(townCode));
        if (town is null)
        {
            town = new Town(townChar);
            _towns.Add(town);
        }

        return town;
    }
}