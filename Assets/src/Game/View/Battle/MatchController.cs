using Game.Model.Source;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        private LocalSource _counfig;

        void Awake() {
            _counfig = new LocalSource();
            _playerCommander.Bind(_counfig.PlayerCommander);
            _enemyCommander.Bind(_counfig.EnemyCommander);
            _playerHandCards.Init(_counfig.PlayerHead, Settings.HAND_CART_COUNT);

            UpdateView();
        }

        void UpdateView() { }

        void Update() { }
    }
}
