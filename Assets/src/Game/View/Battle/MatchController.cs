using DG.Tweening;
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

        private ISelectedCard _selectedCard;
        public ISelectedCard SelectedCard => _selectedCard;

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

        private void SelectEnableHero(ISelectedCard heroCard) {
            _enemyHeroCards.SelectEnableHero(heroCard.Data);
            _playerHeroCards.SelectEnableHero(heroCard.Data);
        }

        internal void UnselectAllHero() {
            _enemyHeroCards.UnselectAllHero();
            _playerHeroCards.UnselectAllHero();
        }

        internal void SetDragPlayerCard(ISelectedCard heroCard) {
            _selectedCard = heroCard;
            if (heroCard != null) {
                SelectEnableHero(heroCard);
            }
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
                        cardPlayer.PlayAttackOnHero(toPos, 0, 3, () => { ApplyAttack(cardPlayer, cardEnemy, 5); })
                                  .OnComplete(() => {
                                      toPos = _enemyCommander.GetRootWorldPosition();
                                      cardPlayer.PlayAttackOnHero(
                                          toPos, 4, 4, () => { ApplyAttack(_enemyCommander, cardEnemy, 5); });
                                  });
                    }
                    else {
                        var toPos = _enemyCommander.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnCommander(
                            toPos, 0, 4, () => { ApplyAttack(cardPlayer, _enemyCommander, 5); });
                    }
                }
                else {
                    var index = Random.Range(0, _enemyHeroCards.GetCardCount());
                    var cardPlayer = _enemyHeroCards.GetCard(index);
                    if (isAttackHero)
                    {
                        var cardEnemy = _playerHeroCards.GetCard(index);
                        var toPos = cardEnemy.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnHero(toPos, 0, 3, () => { ApplyAttack(cardPlayer, cardEnemy, 5); })
                                   .OnComplete(() => { 
                                        toPos = _playerCommander.GetRootWorldPosition();
                                        cardPlayer.PlayAttackOnCommander(
                                            toPos, 4, 4, () => { ApplyAttack(cardPlayer, _playerCommander, 5); });
                                   });
                    }
                    else
                    {
                        var toPos = _playerCommander.GetRootWorldPosition();
                        cardPlayer.PlayAttackOnCommander(toPos, 0, 4, () => {
                            ApplyAttack(cardPlayer, _playerCommander, 5);
                        });
                    }
                }
            }
        }

        void ApplyAttack(IAliveCard fromCard, IAliveCard toCard, float damage) {
            toCard.AliveEntity.ApplyDamage(damage, fromCard.GetRootTransform());
        }

//#endif
    }
}
