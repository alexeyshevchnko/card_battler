
using System.Collections.Generic;

namespace Game.Model.Interface{

    public interface IHeroList {
        IReadOnlyList<IHero> Heroes { get; }
    }

}
