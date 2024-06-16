using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Battle.Layouts
{

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class CircularLayout3D : MonoBehaviour
    {
        private enum Alignment
        {
            Top,
            Bottom,
            Right,
            Left
        }

        [SerializeField] private float _radius = 1.0f;
        [SerializeField] private float _angleOffset = 0.0f;
        [SerializeField] private Alignment _alignment = Alignment.Top;
        [SerializeField] private float _spacingAngle = 10.0f;
        [SerializeField] private bool _rotateObjects = true;
        [SerializeField] private float _offsetZ = 0;

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

                if (!_rotateObjects)
                {
                    for (int i = 0; i < _objectsToLayout.Length; i++)
                    {
                        Layout3DItem obj = _objectsToLayout[i];
                        obj.MyTrans.rotation = Quaternion.identity;
                    }
                }
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
            var activeObjects = new List<Layout3DItem>();

            for (int i = 0; i < childCount; i++)
            {
                var trans = transform.GetChild(i);
                var item = trans.GetComponent<Layout3DItem>();
                if (item == null)
                {
                    item = trans.gameObject.AddComponent<Layout3DItem>();
                    item.Init();
                } 

                if (item.MyGO.activeInHierarchy)
                {
                    activeObjects.Add(item);
                }
            }

            _objectsToLayout = activeObjects.ToArray();
        }

        private void LayoutObjects()
        {
            float angleDelta = _spacingAngle * Mathf.Deg2Rad;
            float startAngle = Mathf.Deg2Rad;
            Vector3 centerPosition = transform.position;

            int objectsCount = _objectsToLayout.Length;
            float totalAngle = objectsCount * angleDelta;
            var offset = Mathf.Deg2Rad * (_angleOffset);
            float initialAngle = startAngle + (offset * Mathf.PI - totalAngle) / 2.0f;

            for (int i = 0; i < objectsCount; i++)
            {
                Layout3DItem obj = _objectsToLayout[i];
                if (obj.MyGO.activeInHierarchy)
                {
                    float angle = initialAngle + i * angleDelta;

                    switch (_alignment)
                    {
                        case Alignment.Top:
                            angle += Mathf.PI / 2.0f;
                            break;
                        case Alignment.Bottom:
                            angle -= Mathf.PI / 2.0f;
                            break;
                        case Alignment.Right:
                            break;
                        case Alignment.Left:
                            angle += Mathf.PI;
                            break;
                        default:
                            break;
                    }

                    Vector3 localPosition = CalculatePositionOnCircle(angle);
                    Vector3 worldPosition = centerPosition + localPosition;
                    worldPosition.z = _offsetZ * i;
                    obj.MyTrans.position = worldPosition;

                    if (_rotateObjects)
                    {
                        Vector3 direction = worldPosition - centerPosition;
                        obj.MyTrans.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                    }
                }
            }
        }

        private Vector3 CalculatePositionOnCircle(float angleRadians)
        {
            float x = Mathf.Cos(angleRadians) * _radius;
            float y = Mathf.Sin(angleRadians) * _radius;

            return new Vector3(x, y, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
