using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Model.Interface;

namespace Game.Model.Interface{

    public interface ISource {
        Task<bool> Connect();

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
