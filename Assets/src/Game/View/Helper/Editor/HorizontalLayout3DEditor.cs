using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.View.Helper.Editor{

    [CustomEditor(typeof(HorizontalLayout3D))]
    public class HorizontalLayout3DEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            HorizontalLayout3D layout3D = (HorizontalLayout3D)target;
            if (GUILayout.Button("Save positions"))
            {
                layout3D.SavePositions();
            }

        }

    }

}
