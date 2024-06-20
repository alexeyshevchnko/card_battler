using System.Collections.Generic;

namespace Game.Model.Interface{

    public interface IActionCardManager {
        IReadOnlyList<ICardAction> CardsData { get; }
        List<ICardAction> CardsReadList { get; }
    }

}
