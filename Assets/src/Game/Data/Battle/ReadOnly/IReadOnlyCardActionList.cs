using System.Collections.Generic;

namespace Game.Data.Battle.ReadOnly{
    
    public interface IReadOnlyCardActionList 
    {
        IReadOnlyList<ICardActionData> CardActionList { get; }
    }
    
}
