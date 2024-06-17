using Game.Data.Types;

namespace Game.Data.Battle.ReadOnly{
    
    public interface IEffectData
    {
        EffectType GetEffectType();
        int GetValue();
    }
    
}
