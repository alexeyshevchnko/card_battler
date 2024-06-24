using Game.Model.Interface;
using TMPro;
using UnityEngine;

namespace Game.View.Battle{

    public class CommanderCard : MonoBehaviour {
        [SerializeField] protected Renderer _renderer;
        [SerializeField] TMP_Text _hpTxt;
        [SerializeField] TMP_Text _nameTxt;
        [SerializeField] private AliveEntity _aliveEntity;

        public IAliveEntity AliveEntity => _aliveEntity;

        public void Init(ICommander data) { 
            _hpTxt.text = data.Health.ToString();
            _nameTxt.text = data.Name;
            _aliveEntity.SetMaxHealth(data.Health);
            _aliveEntity.SetAlive();
        }

        internal Vector3 GetRootWorldPosition()
        {
            return _renderer.transform.position;
        }
    }

}
