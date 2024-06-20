using System.Collections.Generic;

namespace Game.Model.Data.ReadOnly{

    public interface IActionList {
        IReadOnlyList<ICardAction> CardsData { get; }
        List<ICardAction> CardsReadList { get; }
    }

}
