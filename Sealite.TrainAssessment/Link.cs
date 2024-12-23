namespace Sealite.TrainAssessment;

public class Link
{
    public Town StartTown { get; }
    public Town EndTown { get; }
    public LinkDistance Distance { get; }
    

    public Link(Town startTown, Town endTown, LinkDistance distance)
    { 
        StartTown = startTown;
        EndTown = endTown;
        Distance = distance;
    }

    public override string ToString() => $"{StartTown}=>{EndTown} ({Distance})";
    public override bool Equals(object? obj) => GetHashCode() == obj?.GetHashCode();
    public override int GetHashCode() => HashCode.Combine(StartTown, EndTown, Distance);
}