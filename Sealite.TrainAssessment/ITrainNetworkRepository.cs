namespace Sealite.TrainAssessment;

public interface ITrainNetworkRepository
{
    Task<TrainNetwork> LoadFromFile();
}

