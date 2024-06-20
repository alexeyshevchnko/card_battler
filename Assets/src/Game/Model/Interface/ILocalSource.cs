using System.Collections.Generic;
using Game.Model.Interface;

namespace Game.Model.Interface{

    public interface ILocalSource {
        ICommander PlayerCommander { get; }
        ICommander EnemyCommander { get; }

        IReadOnlyList<ICardAction> PlayerDeck { get; }
        IReadOnlyList<ICardAction> EnemyDeck { get; }

        IReadOnlyList<ICardAction> PlayerHead { get; }
        IReadOnlyList<ICardAction> EnemyHead { get; }

        IHeroManager PlayerHeroes { get; }
        IHeroManager EnemyHeroes { get; }
    }
}
