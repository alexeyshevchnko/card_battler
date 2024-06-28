using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle{
    
    public interface ISelectedCard 
    {
        int Index { get; }
        ICardAction Data { get; }
        Transform myTrans { get; }
    }
    
}
