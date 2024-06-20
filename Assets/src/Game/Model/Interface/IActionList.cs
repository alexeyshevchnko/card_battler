using System.Collections.Generic;

namespace Game.Model.Interface{

    public interface IActionList {
        IReadOnlyList<ICardAction> CardsData { get; }
        List<ICardAction> CardsReadList { get; }
    }

}
