using Shared.Settings;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        [SerializeField] private EnemyHandCards _enemyHandCards;
        [SerializeField] private EnemyHeroCards _enemyHeroCards;
        [SerializeField] private PlayerHeroCards _playerHeroCards;

        private void Start() {
            _playerCommander.Bind(Loader.BattleController.PlayerCommander);
            _enemyCommander.Bind(Loader.BattleController.EnemyCommander);
            _playerHandCards.Init(Loader.BattleController.PlayerHead);
            _enemyHandCards.Init(Loader.BattleController.EnemyHead);
            _enemyHeroCards.Init(Loader.BattleController.EnemyHeroes);
            _playerHeroCards.Init(Loader.BattleController.PlayerHeroes);
        }
    }
}
