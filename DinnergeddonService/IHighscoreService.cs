using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DinnergeddonService
{
    [ServiceContract]
    interface IHighscoreService
    {
        [OperationContract]
        int GetHighscore(Guid accountId);

        [OperationContract]
        IDictionary<Guid, int> GetHighscores();

        [OperationContract]
        IDictionary<Guid, int> GetTopNHighscores(int n);
    }
}
