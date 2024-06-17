﻿using UnityEngine;

namespace Game.Data.Battle{

    [System.Serializable]
    public class CommanderData : ICommanderData, ISerializationData
    {
        [SerializeField] protected string _name;
        [SerializeField] protected int _health;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;

        public string GetName() => _name;
        public CardType GetCardType() => _cardType;
        public CardMechanicType GetCardMechanicType() => _cardMechanicType;
        public int GetHealth() => _health;

        public void SetJson(string val)
        {
            CommanderData temp = JsonUtility.FromJson<CommanderData>(val);
            _name = temp._name; 
            _health = temp._health;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;
        }

        public string GetJson()
        { 
            return JsonUtility.ToJson(this);
        }
    }
    
}