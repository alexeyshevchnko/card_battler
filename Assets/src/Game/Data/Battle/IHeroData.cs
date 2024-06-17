using System.Collections.Generic;

namespace Game.Data.Battle{
    public interface IHeroData : ICardData, IDeserializationData
    {
        int GetHealth();
        void ResetFullAttack(int val);
        IReadOnlyList<ChargeData> GetCharges();
        int GetFullAttack();
    }
}
