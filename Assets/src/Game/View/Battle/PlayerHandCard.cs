using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.View.Battle {

    public class PlayerHandCard : HandCard, IDragHandler, IEndDragHandler, IBeginDragHandler {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _dragPositionZ = -2;
        [SerializeField] private Vector3 _dragScale = Vector3.one * 2;
        private Transform _originalParent;

        private Canvas _canvas;
        private Vector3 _startDragPosition;
        private Vector3 _startDragScale;
        private Transform _myTrans;
        private Transform _canvasTrans;
        private RectTransform _canvasRectTrans;


        protected override void Awake() {
            _myTrans = transform;
            _canvasGroup.blocksRaycasts = true;
            base.Awake();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null) {
                Debug.LogError("Canvas not found in parent hierarchy.");
            }
            else {
                _canvasTrans = _canvas.transform;
                _canvasRectTrans = _canvasTrans as RectTransform;
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            MatchController.Instance.SetSelectCard(this);
            _canvasGroup.blocksRaycasts = false;
            _startDragPosition = _myTrans.position;
            _startDragScale = _myTrans.localScale;
            _originalParent = _myTrans.parent;
            _myTrans.SetParent(_canvasTrans);
            _myTrans.localScale = _dragScale;
            _myTrans.SetAsLastSibling();
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRectTrans,
                Input.mousePosition,
                _canvas.worldCamera,
                out var pos);
            _myTrans.position = _canvasTrans.TransformPoint(pos);

            Vector3 newPos = _myTrans.localPosition;
            newPos.z = _dragPositionZ;
            _myTrans.localPosition = newPos;
        }

        public void OnEndDrag(PointerEventData eventData) { 
            _myTrans.SetParent(_originalParent);
            _myTrans.position = _startDragPosition;
            _myTrans.localScale = _startDragScale;
            _canvasGroup.blocksRaycasts = true;
            MatchController.Instance.UnselectAllHero();
        }
    }

}
