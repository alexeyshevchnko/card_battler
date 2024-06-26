using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.View.Battle {

    public class PlayerHandCard : HandCard, IDragHandler, IEndDragHandler, IBeginDragHandler, ISelectedCard {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _dragPositionZ = -2;
        [SerializeField] private Vector3 _dragScale = Vector3.one * 2;
        private Transform _originalParent;

        private Canvas _canvas;
        private Vector3 _startDragPosition;
        private Transform _originalDragParent;

        protected override void Awake() {
            _canvasGroup.blocksRaycasts = true;
            base.Awake();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null) {
                Debug.LogError("Canvas not found in parent hierarchy.");
            }
        }

        

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _startDragPosition = transform.position;
            _originalParent = transform.parent;
            transform.SetParent(transform.parent.root);
            //transform.localScale = _dragScale;
            transform.SetAsLastSibling();  
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out newPosition
            );
            newPosition.z = _dragPositionZ;
            transform.position = newPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<HeroCard>() != null)
            {
                Debug.Log("Dropped on: " + eventData.pointerEnter.name); 
            }
            else
            {
                Debug.Log("Not dropped on a valid slot, returning to original position"); 
            }

            transform.SetParent(_originalParent);
            transform.position = _startDragPosition;
            _canvasGroup.blocksRaycasts = true;
        }
    }

}
