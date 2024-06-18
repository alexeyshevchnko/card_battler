using System.Collections.Generic;
using Game.Data.Battle.ReadOnly;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Battle.UI.ViewBinders{
    
    public class DeckCardBind : MonoBehaviour
    {
        [SerializeField] Text _effectValTxt;
        [SerializeField] Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] Text _lvlTxt;

        public void Bind(ICardActionData data)
        {
            _effectValTxt.text = data.GetEffectData()[0].GetValue().ToString();
            _nameTxt.text = data.GetName();
            _lvlTxt.text = data.GetLevel().ToString();

            for (int i = 0; i < _starsIcons.Count; i++)
            {
                var isEnable = i <= data.GetStars();
                _starsIcons[i].enabled = isEnable;
            }
        }
    }
    
}
