using System.Collections.Generic;
using Game.Model.Interface;

namespace Game.Model.Source{

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
