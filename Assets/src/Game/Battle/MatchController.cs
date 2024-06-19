using Game.Battle.UI.ViewBinders;
using Game.Data.Battle;
using UnityEngine;

namespace Game.Battle {

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        private GenerateConfigs _counfig;

        void Awake() {
            _counfig = new GenerateConfigs();
            _playerCommander.Bind(_counfig.PlayerCommander);
            _enemyCommander.Bind(_counfig.EnemyCommander);

            _playerHandCards.Init(_counfig.PlayerCardAction, GenerateConfigs.HAND_CART_COUNT);


            UpdateView();
        }

        void UpdateView() { }

        void Update() { }
    }
}
