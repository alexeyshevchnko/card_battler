using Game.Data.Battle.ReadOnly;
using UnityEngine;

namespace Game.Battle.UI.ViewBinders {

    public class CommanderCard : MonoBehaviour {
        [SerializeField] UnityEngine.UI.Text _hpTxt;
        [SerializeField] UnityEngine.UI.Text _nameTxt;

        public void Bind(ICommanderData data) {
            _hpTxt.text = data.Health.ToString();
            _nameTxt.text = data.Name;
        }
    }

}
