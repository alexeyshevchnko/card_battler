using System.Collections;
using System.Collections.Generic; 

namespace Game.Model.Interface{

    public interface ISource {
        IEnumerator Connect();

        ICommander PlayerCommander { get; }
        ICommander EnemyCommander { get; }

        IReadOnlyList<ICardAction> PlayerDeck { get; }
        IReadOnlyList<ICardAction> EnemyDeck { get; }

        IReadOnlyList<ICardAction> PlayerHead { get; }
        IReadOnlyList<ICardAction> EnemyHead { get; }

        IHeroManager PlayerHeroes { get; }
        IHeroManager EnemyHeroes { get; }

        bool IsConnect { get; }
    }
}
