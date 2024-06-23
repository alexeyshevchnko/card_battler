using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.View.Battle {

    public class PlayerHandCard : HandCard, IDragHandler, IEndDragHandler, IBeginDragHandler {
        [SerializeField] private float _dragPositionZ = -2;
        [SerializeField] private Vector3 _dragScale = Vector3.one * 2;

        private Canvas _canvas;
        private Vector3 _startDragPosition;
        private Transform _originalDragParent;

        protected override void Awake() {
            base.Awake();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null) {
                Debug.LogError("Canvas not found in parent hierarchy.");
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _startDragPosition = _root.position;
            _originalDragParent = _root.parent;
            _root.localScale = _dragScale;
            _root.SetParent(_canvas.transform, true);
        }

        public void OnDrag(PointerEventData eventData) {
            Vector3 newPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out newPosition
            );
            newPosition.z = _dragPositionZ;
            _root.position = newPosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            _root.SetParent(_originalDragParent, true);
            _root.localScale = Vector3.one;
            _root.position = _startDragPosition;
        }
    }

}
