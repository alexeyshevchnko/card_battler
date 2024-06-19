using System.Collections.Generic;
using Game.Data.Base;

namespace Game.Data.Battle.ReadOnly {
    public interface ICardActionData : ICardData, IDeserializationData {
        IReadOnlyList<IEffectData> Effects { get; }
        IEffectData FirstEffect { get; }
    }
}
