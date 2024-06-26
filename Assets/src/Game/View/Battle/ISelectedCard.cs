using Game.Model.Interface;

namespace Game.View.Battle{
    
    public interface ISelectedCard 
    {
        int Index { get; }
        ICardAction Data { get; }
    }
    
}
