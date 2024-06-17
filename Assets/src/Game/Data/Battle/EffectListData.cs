using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Battle{
    
    public class EffectListData 
    {
        public List<EffectData> _effects;

        public EffectListData(List<EffectData> effects)
        {
            _effects = effects;
        }
    }
    
}
