using System.Collections;
using Game.Controller;
using Shared.Settings;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards; 

        private void Start() {
            _playerCommander.Bind(Loader.BattleController.Source.PlayerCommander);
            _enemyCommander.Bind(Loader.BattleController.Source.EnemyCommander);
            _playerHandCards.Init(Loader.BattleController.Source.PlayerHead, Settings.HAND_CART_COUNT);
        }
    }
}
