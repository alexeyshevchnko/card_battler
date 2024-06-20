
using System.Collections.Generic;

namespace Game.Model.Data.ReadOnly{

    public interface IHeroList {
        IReadOnlyList<IHero> Heroes { get; }
    }

}
