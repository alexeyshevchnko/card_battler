using Game.Data.Types;

namespace Game.Data.Battle.ReadOnly{
    public interface IBaseICardData
    {
        string GetName();
        CardType GetCardType();
        CardMechanicType GetCardMechanicType();
    }
}
