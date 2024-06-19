using System.Collections.Generic;

namespace Game.Data.Battle.ReadOnly {

    public interface IReadOnlyCardActionList {
        IReadOnlyList<ICardActionData> CardsData { get; }
        List<ICardActionData> CardsList { get; }
    }

}
