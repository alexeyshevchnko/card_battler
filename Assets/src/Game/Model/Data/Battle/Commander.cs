using Game.Model.Data.Base;
using Game.Model.Data.ReadOnly;
using Game.Model.Data.Type;
using UnityEngine;

namespace Game.Model.Data.Battle{

    [System.Serializable]
    public class Commander : ICommander, ISerialization, IDeserialization {
        [SerializeField] protected string _name;
        [SerializeField] protected int _health;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;

        public string Name => _name;
        public CardType CardType => _cardType;
        public CardMechanicType CardMechanicType => _cardMechanicType;
        public int Health => _health;

        public void SetJson(string val) {
            Commander temp = JsonUtility.FromJson<Commander>(val);
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
