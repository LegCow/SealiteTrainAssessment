using System.Text;

namespace Sealite.TrainAssessment;

public class TownCode
{
    private const char MIN_CODE = 'A';
    private const char MAX_CODE = 'Z';

    public static TownCode Create(char value) => new(value);        

    public char Value { get; }

    private TownCode(char value)
    {
        var ascii = Encoding.ASCII.GetBytes(new char[] { value, MIN_CODE, MAX_CODE });
        if (ascii[0] < ascii[1] || ascii[0] > ascii[2]) throw new InvalidOperationException($"value must be {MIN_CODE}-{MAX_CODE}");
        Value = value;

    }

    public override string ToString() => Value.ToString();
    public override bool Equals(object? obj)
    {
        return GetHashCode() == obj?.GetHashCode();
    } 
    public override int GetHashCode() => Value.GetHashCode();
}