using System.Collections;
using System.Threading.Tasks;
using Game.Controller;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        private BattleController _battleController;

        private void Start() {
            _battleController = new BattleController();

            StartCoroutine(TryConnect());
        }


        IEnumerator TryConnect() {
            var result = _battleController.TryConnect(this);
            while (!_battleController.Source.IsConnect) {
                yield return new WaitForSeconds(1);
            }

            if (result)
            {
                _playerCommander.Bind(_battleController.Source.PlayerCommander);
                _enemyCommander.Bind(_battleController.Source.EnemyCommander);
                _playerHandCards.Init(_battleController.Source.PlayerHead, Settings.HAND_CART_COUNT);
            }
        } 
    }
}
