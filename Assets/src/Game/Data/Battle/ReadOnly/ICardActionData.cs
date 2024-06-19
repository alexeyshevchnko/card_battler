using System.Collections.Generic;

namespace Game.Data.Battle.ReadOnly {
    public interface ICardActionData : ICardData, IDeserializationData {
        IReadOnlyList<IEffectData> Effects { get; }
        IEffectData FirstEffect { get; }
    }
}
