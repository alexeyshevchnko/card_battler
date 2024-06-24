using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        internal static MatchController Instance;

        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;

        [SerializeField] private PlayerHandCards _playerHandCards;
        [SerializeField] private EnemyHandCards _enemyHandCards;

        [SerializeField] private EnemyHeroCards _enemyHeroCards;
        [SerializeField] private PlayerHeroCards _playerHeroCards;

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            _playerCommander.Init(Loader.BattleController.PlayerCommander);
            _enemyCommander.Init(Loader.BattleController.EnemyCommander);
            _playerHandCards.Init(Loader.BattleController.PlayerHead);
            _enemyHandCards.Init(Loader.BattleController.EnemyHead);
            _enemyHeroCards.Init(Loader.BattleController.EnemyHeroes);
            _playerHeroCards.Init(Loader.BattleController.PlayerHeroes);
        }

        internal void SelectEnableHero(ICardAction data) {
            _enemyHeroCards.SelectEnableHero(data);
            _playerHeroCards.SelectEnableHero(data);
        }

        internal void UnselectAllHero() {
            _enemyHeroCards.UnselectAllHero();
            _playerHeroCards.UnselectAllHero();
        }

//#if UNITY_EDITOR

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Q)) {
                bool isPlayerAttack = Random.Range(0, 100) > 50;
                bool isAttackHero = Random.Range(0, 100) > 50;
                if (isPlayerAttack) {
                    var index = Random.Range(0, _playerHeroCards.GetCardCount());
                    var cardPlayer = _playerHeroCards.GetCard(index);
                    if (isAttackHero) {
                        var cardEnemy = _enemyHeroCards.GetCard(index);
                        var toPos = cardEnemy.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnHero(toPos, 5);
                    }
                    else {
                        var toPos = _enemyCommander.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnCommander(toPos, 5);
                    }
                }
                else {
                    var index = Random.Range(0, _enemyHeroCards.GetCardCount());
                    var cardPlayer = _enemyHeroCards.GetCard(index);
                    if (isAttackHero)
                    {
                        var cardEnemy = _playerHeroCards.GetCard(index);
                        var toPos = cardEnemy.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnHero(toPos, 5);
                    }
                    else
                    {
                        var toPos = _playerCommander.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnCommander(toPos, 5);
                    }
                }
            }
        }

//#endif
    }
}
