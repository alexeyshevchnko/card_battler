namespace Game.Data.Battle{
    public interface ICommanderData : IBaseICardData, IDeserializationData
    {
        int GetHealth();
    }
}
