namespace Sealite.TrainAssessment;

public static class Program
{
    public static async Task Main(string[] args)
    {

        ITrainNetworkRepository repository = new TrainNetworkFileRepository(@"C:\Dev\SealiteTrainAssessment\Sealite.TrainAssessment\Input.txt"); // TODO
        var network = await repository.LoadFromFile();
        var townA = network.GetTown('A');
        var townB = network.GetTown('B');
        var townC = network.GetTown('C');
        var townD = network.GetTown('D');
        var townE = network.GetTown('E');


        IRoutePlanner planner = new SimpleRecursiveRoutePlanner();

        //Test #7: Number of trips from A to C with exactly 4 stops is 3 ( A=>B=>C=>D=>C, A=>D=>C=>D=>C, A=>D=>E=>B=>C )
        var routes = planner.CreateRoutes(townA, 4)
            .Where(r => r.EndTown == townC)
            .Where(r => r.TotalStops == 4)
            .ToList();

        foreach (var route in routes)
        {
            Console.WriteLine(route);
        }
        
        Console.ReadKey();

    }
}
