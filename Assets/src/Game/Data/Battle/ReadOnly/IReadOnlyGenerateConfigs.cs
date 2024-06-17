using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle.ReadOnly{
    
    public interface IReadOnlyGenerateConfigs 
    {
        ICommanderData PlayerCommander { get; }
        ICommanderData EnemyCommander { get; }

        IReadOnlyHeroListData PlayerHeroes { get; }
        IReadOnlyHeroListData EnemyHeroes { get; }

        IReadOnlyCardActionList PlayerCardAction { get; }
        IReadOnlyCardActionList EnemyCardAction { get; }
    }
}
