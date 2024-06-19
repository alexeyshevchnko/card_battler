using System.Collections.Generic;
using Game.Data.Base;

namespace Game.Data.Battle.ReadOnly {

    public interface IHeroData : ICardData, IDeserializationData {
        int Health { get; }
        void ResetFullAttack(int val);
        IReadOnlyList<IChargeData> Charges { get; }
        int FullAttack { get; }
    }

}
