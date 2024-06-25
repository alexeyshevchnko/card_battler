using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.View.Battle {

    public interface IAliveCard {
        IAliveEntity AliveEntity { get; }
        Transform GetRootTransform();
    }

}
