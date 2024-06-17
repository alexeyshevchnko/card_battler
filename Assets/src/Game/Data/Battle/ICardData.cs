
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Data.Battle{
    public interface ICardData : IBaseICardData
    {
        int GetLevel();
        int GetStars();
    }
}
