namespace Sealite.TrainAssessment;

public class LinkDistance
{
    private const int MIN_DISTANCE = 1;
    private const int MAX_DISTANCE = 10;
    public static LinkDistance Create(int value) => new(value);        

    public int Value { get; }

    private LinkDistance(int value)
    {
        if (value < MIN_DISTANCE || value > MAX_DISTANCE)  throw new InvalidOperationException($"A LinkDistance must be between {MIN_DISTANCE} and {MAX_DISTANCE}");
        Value = value;
    }

    public override string ToString() => Value.ToString();
    public override bool Equals(object? obj) => GetHashCode() == obj?.GetHashCode();
    public override int GetHashCode() => Value.GetHashCode();
}