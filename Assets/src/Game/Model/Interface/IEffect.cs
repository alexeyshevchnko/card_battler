using Game.Model.Type;

namespace Game.Model.Interface{

    public interface IEffect {
        EffectType EffectType { get; }
        int Value { get; }
    }

}
