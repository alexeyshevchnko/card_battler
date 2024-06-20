using System.Collections.Generic;

namespace Game.Model.Data.ReadOnly{

    public interface IConfigs {
        ICommander PlayerCommander { get; }
        ICommander EnemyCommander { get; }

        IReadOnlyList<ICardAction> PlayerDeck { get; }
        IReadOnlyList<ICardAction> EnemyDeck { get; }

        IReadOnlyList<ICardAction> PlayerHead { get; }
        IReadOnlyList<ICardAction> EnemyHead { get; }

        IHeroList PlayerHeroes { get; }
        IHeroList EnemyHeroes { get; }
    }
}
