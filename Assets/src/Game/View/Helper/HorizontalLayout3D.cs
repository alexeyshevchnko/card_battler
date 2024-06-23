using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.View.Helper{

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class HorizontalLayout3D : MonoBehaviour {
        private enum Alignment {
            Left,
            Center,
            Right,
            CenterBottom,
            CenterTop
        }

        [SerializeField] private float _spacing = 0.1f;
        [SerializeField] private Alignment _alignment = Alignment.Center;
        [SerializeField] private float _verticalOffset = 0.0f;
        [SerializeField] private bool _rotateObjects = false;
        [SerializeField] private List<Vector3> _savedPositions = new List<Vector3>();

        private Layout3DItem[] _objectsToLayout;
        private bool _isInit = false;

        private void OnEnable() {
            Recalculate();

#if UNITY_EDITOR
            EditorApplication.update += OnEditorUpdate;
#endif
        }

        private void OnDisable() {
#if UNITY_EDITOR
            EditorApplication.update -= OnEditorUpdate;
#endif
        }

#if UNITY_EDITOR
        private void OnEditorUpdate() {
            if (!Application.isPlaying) {
                Recalculate();
            }
        }
#endif

        private void OnValidate() {
            if (!Application.isPlaying) {
                Init();
                Recalculate();
            }
        }

        public void Recalculate() {
            UpdateObjectsToLayout();
            LayoutObjects();
        }

        private void OnTransformChildrenChanged() {
            //if (_isRecalculateChildrenChanged) {
            if (!Application.isPlaying)
                Recalculate();
            //}
        }

        private void Init() {
            if (!_isInit) {
                int childCount = transform.childCount;
                for (int i = 0; i < childCount; i++) {
                    var trans = transform.GetChild(i);
                    var item = trans.GetComponent<Layout3DItem>();
                    item.Init();
                }

                _isInit = true;
            }
        }

        private void UpdateObjectsToLayout() {
            int childCount = transform.childCount;
            _objectsToLayout = new Layout3DItem[childCount];

            for (int i = 0; i < childCount; i++) {
                var trans = transform.GetChild(i);
                var item = trans.GetComponent<Layout3DItem>();
                if (item == null) {
                    item = trans.gameObject.AddComponent<Layout3DItem>();
                    item.Init();
                }

                _objectsToLayout[i] = item;
            }
        }

        private void LayoutObjects() {
            float totalWidth = CalculateTotalWidth();
            float currentX = CalculateInitialX(totalWidth);

            foreach (var obj in _objectsToLayout) {
                if (obj.gameObject.activeInHierarchy) {
                    if (!_rotateObjects)
                        obj.transform.rotation = Quaternion.identity;

                    if (obj.Render != null) {
                        float yPosition = _verticalOffset;
                        switch (_alignment) {
                            case Alignment.CenterBottom:
                                yPosition += obj.Bounds.extents.y;
                                break;
                            case Alignment.CenterTop:
                                yPosition -= obj.Bounds.extents.y;
                                break;
                        }

                        obj.transform.localPosition = new Vector3(currentX + obj.Bounds.extents.x, yPosition, 0);
                        currentX += obj.Bounds.size.x + _spacing;
                    }
                    else {
                        Debug.LogError($"Object {obj.name} does not have a Render component.");
                    }
                }
            }
        }

        private float CalculateTotalWidth() {
            float totalWidth = 0;
            foreach (var obj in _objectsToLayout) {
                if (obj.gameObject.activeInHierarchy) {
                    Renderer render = obj.Render;
                    if (render != null) {
                        totalWidth += render.bounds.size.x + _spacing;
                    }
                }
            }

            return totalWidth - _spacing;
        }

        private float CalculateInitialX(float totalWidth) {
            switch (_alignment) {
                case Alignment.Left:
                    return 0;
                case Alignment.Center:
                case Alignment.CenterBottom:
                case Alignment.CenterTop:
                    return -totalWidth / 2;
                case Alignment.Right:
                    return -totalWidth;
                default:
                    return 0;
            }
        }

        internal void SavePositions()
        {
            _savedPositions.Clear();
            foreach (var obj in _objectsToLayout)
            {
                _savedPositions.Add(obj.transform.position);
                Undo.RecordObject(obj.transform, "Save Layout Position");
                EditorUtility.SetDirty(obj.transform);
            }

            Debug.Log($"Positions saved for {gameObject.name}");
        }

        public Vector3 GetSlotPosition(int index) {
            return _savedPositions[index];
        }
    }
}
