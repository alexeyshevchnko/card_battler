﻿using System.Collections.Generic;
using Game.Model.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.View.Battle {
    public class HandCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
        [SerializeField] Transform _root;
        [SerializeField] TMP_Text _effectValTxt;
        [SerializeField] TMP_Text _nameTxt;
        [SerializeField] List<Image> _starsIcons;
        [SerializeField] TMP_Text _lvlTxt;
        [SerializeField] private float _dragPositionZ = -2;
        [SerializeField] private Vector3 _dragScale = Vector3.one * 2;

        private Canvas _canvas;
        private Vector3 _startDragPosition;
        private Transform _originalDragParent;

        private ICardAction _data;

        void Awake() {
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null) {
                Debug.LogError("Canvas not found in parent hierarchy.");
            }
        }

        public void Init(ICardAction data) {
            _data = data;
            UpdateView();
        }

        private void UpdateView() {
            _effectValTxt.text = _data.FirstEffect.Value.ToString();
            _nameTxt.text = _data.Name;
            _lvlTxt.text = _data.Level.ToString();

            for (int i = 0; i < _starsIcons.Count; i++) {
                var isEnable = i <= _data.Stars;
                _starsIcons[i].enabled = isEnable;
            }
        }

        internal void SetRootWorldPos(Vector3 pos) {
            _root.position = pos;
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

        private void OnMouseDown() {
            
        }
    }

}