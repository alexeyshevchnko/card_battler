using Game.Model.Interface;
using UnityEngine;

namespace Game.View.Battle{

    public class CommanderCard : MonoBehaviour {
        [SerializeField] UnityEngine.UI.Text _hpTxt;
        [SerializeField] UnityEngine.UI.Text _nameTxt;

        public void Bind(ICommander data) {
            _hpTxt.text = data.Health.ToString();
            _nameTxt.text = data.Name;
        }
    }

}
