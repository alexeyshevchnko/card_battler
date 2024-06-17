namespace Game.Data.Battle{
    public interface IBaseICardData
    {
        string GetName();
        CardType GetCardType();
        CardMechanicType GetCardMechanicType();
    }
}
