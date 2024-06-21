using System.Threading.Tasks;
using Game.Controller;
using UnityEngine;

namespace Game.View.Battle{

    public class MatchController : MonoBehaviour {
        [SerializeField] private CommanderCard _playerCommander;
        [SerializeField] private CommanderCard _enemyCommander;
        [SerializeField] private PlayerHandCards _playerHandCards;
        private BattleController _battleController;

        private async void Start() {
            _battleController = new BattleController();
            
            var result = await _battleController.TryConnect();
            if (result) {

                _playerCommander.Bind(_battleController.Source.PlayerCommander);
                _enemyCommander.Bind(_battleController.Source.EnemyCommander);
                _playerHandCards.Init(_battleController.Source.PlayerHead, Settings.HAND_CART_COUNT);
            }
        } 

        void Update() { }
    }
}
