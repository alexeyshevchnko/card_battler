
using System.Collections.Generic;

namespace Game.Data.Battle.ReadOnly{
    
    public interface IReadOnlyHeroListData 
    {
        IReadOnlyList<IHeroData> HeroList { get; }
    }
    
}
