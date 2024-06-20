using Game.Model.Data.Battle;
using Game.Model.Data.Source;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        private Configs _counfig;

        void Awake() {
            _counfig = new Configs();
            _playerCommander.Bind(_counfig.PlayerCommander);
            _enemyCommander.Bind(_counfig.EnemyCommander);
            _playerHandCards.Init(_counfig.PlayerHead, Configs.HAND_CART_COUNT);

            UpdateView();
        }

        void UpdateView() { }

        void Update() { }
    }
}
