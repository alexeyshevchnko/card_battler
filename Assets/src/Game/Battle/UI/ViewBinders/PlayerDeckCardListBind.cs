using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Battle.UI.ViewBinders{
    
    public class PlayerDeckCardListBind : MonoBehaviour
    {
        [SerializeField] GameObject _playerDeckCardPref;
        [SerializeField] Transform _layoutDeck;
        List<DeckCardBind> _currentDeck;

        public void Bind(IReadOnlyCardActionList data, int maxCardCount)
        {
            _currentDeck = new List<DeckCardBind>();
            for (int i = 0; i < maxCardCount;  i++)
            {
                var go =  Instantiate(_playerDeckCardPref, _layoutDeck);
                var binder = go.GetComponent<DeckCardBind>();
                _currentDeck.Add(binder);
                binder.Bind(data.CardActionList[i]);
            }
        }

    }
    
}
