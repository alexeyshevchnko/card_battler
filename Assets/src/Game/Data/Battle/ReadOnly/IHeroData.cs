using System.Collections.Generic;

namespace Game.Data.Battle.ReadOnly {

    public interface IHeroData : ICardData, IDeserializationData {
        int GetHealth();
        void ResetFullAttack(int val);
        IReadOnlyList<IChargeData> GetCharges();
        int GetFullAttack();
    }

}
