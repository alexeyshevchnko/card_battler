
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Data.Battle.ReadOnly {
    public interface ICardData : IBaseICardData {
        int GetLevel();
        int GetStars();
    }
}
