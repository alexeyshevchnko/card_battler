using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Layouts{
    
    public class Layout3DItem : MonoBehaviour
    {
        [SerializeField] Renderer _render;
        
        internal Renderer Render => _render; 

        internal Bounds Bounds => _render.bounds;


        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            Init();
        }

        internal void Init()
        { 

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
