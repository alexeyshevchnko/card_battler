using Game.Data.Base;
using Game.Data.Battle.ReadOnly;
using Game.Data.Types;
using UnityEngine;

namespace Game.Data.Battle {

    [System.Serializable]
    public class CommanderData : ICommanderData, ISerializationData {
        [SerializeField] protected string _name;
        [SerializeField] protected int _health;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;

        public string Name => _name;
        public CardType CardType => _cardType;
        public CardMechanicType CardMechanicType => _cardMechanicType;
        public int Health => _health;

        public void SetJson(string val) {
            CommanderData temp = JsonUtility.FromJson<CommanderData>(val);
            _name = temp._name;
            _health = temp._health;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

}
