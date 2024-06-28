using Game.Model.Interface;
using Game.Model.Type;
using UnityEngine;

namespace Game.Model.Data{

    [System.Serializable]
    public class Commander : ICommander, ISerialization, IDeserialization {
        [SerializeField] protected string _name;
        [SerializeField] protected int _health;
        [SerializeField] protected CardType _cardType;
        [SerializeField] protected CardMechanicType _cardMechanicType;
        [SerializeField] private PlayerType _playerType;

        public string Name => _name;
        public CardType CardType => _cardType;
        public CardMechanicType CardMechanicType => _cardMechanicType;
        public int Health => _health;
        public PlayerType PlayerType => _playerType;

        public void SetJson(string val) {
            Commander temp = JsonUtility.FromJson<Commander>(val);
            _name = temp._name;
            _health = temp._health;
            _cardType = temp._cardType;
            _cardMechanicType = temp._cardMechanicType;
            _playerType = temp.PlayerType;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

}
