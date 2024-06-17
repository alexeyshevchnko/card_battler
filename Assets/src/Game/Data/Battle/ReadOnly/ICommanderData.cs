namespace Game.Data.Battle.ReadOnly{
    public interface ICommanderData : IBaseICardData, IDeserializationData
    {
        int GetHealth();
    }
}
