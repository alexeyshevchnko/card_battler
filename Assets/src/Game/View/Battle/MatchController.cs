using Shared.Settings;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards; 

        private void Start() {
            _playerCommander.Bind(Loader.BattleController.PlayerCommander);
            _enemyCommander.Bind(Loader.BattleController.EnemyCommander);
            _playerHandCards.Init(Loader.BattleController.PlayerHead, Settings.HAND_CART_COUNT);
        }
    }
}
