
using System.Collections.Generic;

namespace Game.Data.Battle{
    public interface ICard
    {
        string GetName();
        int GetLevel();
        int GetStars();
        int GetHealth();
        string GetFullAttack();
        CardType GetCardType();
        //ICharge[] GetCharges();
        
        List<ChargeData> GetCharges();
        CardMechanicType GetCardMechanicType();
    }
}
