using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Layouts{
    
    public class Layout3DItem : MonoBehaviour
    {
        [SerializeField] Renderer _render;
        
        private Transform _myTrans;
        private GameObject _myGo;
        internal Renderer Render => _render;
        internal Transform MyTrans => _myTrans;
        internal GameObject MyGO => _myGo;

        internal Bounds Bounds => _render.bounds;


        private void Awake()
        {
            Init();
        }

        internal void Init()
        {
            _myTrans = transform;
            _myGo = gameObject;

            if (_render == null)
            {
                _render = GetComponent<Renderer>();
                if (_render == null)
                {
                    _render = GetComponentInChildren<Renderer>();
                }
            }
        }

        private void OnValidate()
        {
            Init();
        }
    }
    
}
