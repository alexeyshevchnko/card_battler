using Game.Model.Interface;
using TMPro;
using UnityEngine;

namespace Game.View.Battle{

    public class CommanderCard : MonoBehaviour {
        [SerializeField] TMP_Text _hpTxt;
        [SerializeField] TMP_Text _nameTxt;

        public void Bind(ICommander data) {
            _hpTxt.text = data.Health.ToString();
            _nameTxt.text = data.Name;
        }
    }

}
