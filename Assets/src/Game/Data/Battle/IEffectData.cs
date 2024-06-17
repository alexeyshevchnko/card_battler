using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle{
    
    public interface IEffectData
    {
        EffectType GetEffectType();
        int GetValue();
    }
    
}
