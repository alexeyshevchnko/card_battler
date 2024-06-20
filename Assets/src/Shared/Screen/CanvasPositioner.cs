namespace Shared.Screen{
    using UnityEngine;

    public class CanvasPositioner : MonoBehaviour {
        [SerializeField] Canvas _worldCanvas;
        [SerializeField] RectTransform _canvasRect;
        [SerializeField] Camera _mainCamera;
        [SerializeField] float _canvasDistance = 10.0f;

        void Start() {
            Recalculate();
        }

        void Recalculate() {
            _worldCanvas.transform.position =
                _mainCamera.transform.position + _mainCamera.transform.forward * _canvasDistance;
            float frustumHeight = 2.0f * _canvasDistance * Mathf.Tan(_mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * _mainCamera.aspect;
            _canvasRect.sizeDelta = new Vector2(frustumWidth, frustumHeight);
        }

        private void OnValidate() {
            Recalculate();
        }
    }

}