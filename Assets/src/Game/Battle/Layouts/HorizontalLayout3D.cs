using UnityEngine;
using UnityEditor;

namespace Game.Battle.Layouts
{

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class HorizontalLayout3D : MonoBehaviour
    {
        private enum Alignment
        {
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

        private Layout3DItem[] _objectsToLayout;


        private void OnEnable()
        {
            Recalculate();

#if UNITY_EDITOR
            EditorApplication.update += OnEditorUpdate;
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.update -= OnEditorUpdate;
#endif
        }

#if UNITY_EDITOR
        private void OnEditorUpdate()
        {
            if (!Application.isPlaying)
            {
                Recalculate();
            }
        }
#endif

        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                Recalculate();
            }
        }

        public void Recalculate()
        {
            UpdateObjectsToLayout();
            LayoutObjects();
        }

        private void OnTransformChildrenChanged()
        {
            Init();
            Recalculate();
        }

        private void Init()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var trans = transform.GetChild(i);
                var item = trans.GetComponent<Layout3DItem>();
                item.Init();
            }
        }

        private void UpdateObjectsToLayout()
        {
            int childCount = transform.childCount;
            _objectsToLayout = new Layout3DItem[childCount];

            for (int i = 0; i < childCount; i++)
            {
                var trans = transform.GetChild(i);
                var item = trans.GetComponent<Layout3DItem>();
                if (item == null)
                {
                    item = trans.gameObject.AddComponent<Layout3DItem>();
                    item.Init();
                }

                _objectsToLayout[i] = item;
            }
        }

        private void LayoutObjects()
        {
            float totalWidth = CalculateTotalWidth();
            float currentX = CalculateInitialX(totalWidth);

            foreach (var obj in _objectsToLayout)
            {
                if (obj.MyGO.activeInHierarchy)
                {
                    if (!_rotateObjects)
                        obj.MyTrans.rotation = Quaternion.identity;

                    if (obj.Render != null)
                    {
                        float yPosition = _verticalOffset;
                        switch (_alignment)
                        {
                            case Alignment.CenterBottom:
                                yPosition += obj.Bounds.extents.y;
                                break;
                            case Alignment.CenterTop:
                                yPosition -= obj.Bounds.extents.y;
                                break;
                        }

                        obj.MyTrans.localPosition = new Vector3(currentX + obj.Bounds.extents.x, yPosition, 0);
                        currentX += obj.Bounds.size.x + _spacing;
                    }
                    else
                    {
                        Debug.LogError($"Object {obj.name} does not have a Render component.");
                    }
                }
            }
        }

        private float CalculateTotalWidth()
        {
            float totalWidth = 0;
            foreach (var obj in _objectsToLayout)
            {
                if (obj.MyGO.activeInHierarchy)
                {
                    Renderer render = obj.Render;
                    if (render != null)
                    {
                        totalWidth += render.bounds.size.x + _spacing;
                    }
                }
            }

            return totalWidth - _spacing;
        }

        private float CalculateInitialX(float totalWidth)
        {
            switch (_alignment)
            {
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
    }
}
