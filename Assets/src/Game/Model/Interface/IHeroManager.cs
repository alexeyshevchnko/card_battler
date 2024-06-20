
using System.Collections.Generic;

namespace Game.Model.Interface{

    public interface IHeroManager {
        IReadOnlyList<IHero> Heroes { get; }
    }

}
