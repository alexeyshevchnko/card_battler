using Game.Battle.UI.ViewBinders;
using Game.Data.Battle;
using UnityEngine;

namespace Game.Battle{
    
    public class MatchController : MonoBehaviour
    {
        [SerializeField] private CommanderCardBind _playerCommanderView;
        [SerializeField] private CommanderCardBind _enemyCommanderView;
        [SerializeField] private PlayerDeckCardListBind _playerDeckCardList;
        private GenerateConfigs _counfig;

        void Awake()
        {
            _counfig = new GenerateConfigs();
            _playerCommanderView.Bind(_counfig.PlayerCommander);
            _enemyCommanderView.Bind(_counfig.EnemyCommander);

            _playerDeckCardList.Bind(_counfig.PlayerCardAction, GenerateConfigs.DECK_CARD_COUNT);
            

            UpdateView();
        }

        void UpdateView()
        {

        }
        
        void Update()
        {
            
        }
    }
    
}
