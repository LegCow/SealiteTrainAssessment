namespace Sealite.TrainAssessment;

public class TrainNetworkFileRepository : ITrainNetworkRepository
{
    private readonly string trainNetworkFileName;

    public TrainNetworkFileRepository(string trainNetworkFileName)
    {
        this.trainNetworkFileName = trainNetworkFileName;
    }


    public async Task<TrainNetwork> LoadFromFile()
    {
        var network = new TrainNetwork();

        var lines = await File.ReadAllLinesAsync(trainNetworkFileName);
        foreach (var line in lines)
            ParseRoute(network, line);

        return network;
    }

    private static void ParseRoute(TrainNetwork network, string line)
    {
        var tokens = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (tokens.Length != 3) throw new InvalidOperationException("Train Network File malformed: token length != 3");
        if (tokens[0].Length != 1) throw new InvalidOperationException("Train Network File malformed: code length != 1");
        if (tokens[1].Length != 1) throw new InvalidOperationException("Train Network File malformed: code length != 1");

        var from = network.GetOrCreateTown(tokens[0][0]);
        var to = network.GetOrCreateTown(tokens[1][0]);
        var distance = LinkDistance.Create(int.Parse(tokens[2]));

        var trainRoute = new Link(from, to, distance);
        from.AddConnection(trainRoute);
    }
}

