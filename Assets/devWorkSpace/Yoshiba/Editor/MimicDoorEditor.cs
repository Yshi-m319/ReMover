//#define  DELDATA
using System;
using System.Collections.Generic;
using devWorkSpace.Yoshiba.Scripts;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace devWorkSpace.Yoshiba.Editor
{
    [CustomEditor(typeof(MimicDoor))]
    public class MimicDoorEditor : UnityEditor.Editor
    {
        
        int _pitchNum;
        private MimicDoor _mimicDoor;
        private int _maxNumOfRing;
        private SerializedProperty _pitches;
        private void OnEnable()
        {
            _mimicDoor = target as MimicDoor;
            if ((_mimicDoor is null))
                return;
            
            serializedObject.Update();
            _pitches = serializedObject.FindProperty("pitches");
            _maxNumOfRing = _mimicDoor.MAXNumOfRings;
            
            //ピッチをlimUp個までに制限する
            while (_pitches.arraySize > _maxNumOfRing)
            {
                _pitches.DeleteArrayElementAtIndex(_pitches.arraySize-1);
            }
            while (_pitches.arraySize < _maxNumOfRing)
            {
                _pitches.InsertArrayElementAtIndex(0);
            }
            
            serializedObject.ApplyModifiedProperties();

            _mimicDoor.genRings();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            var num = serializedObject.FindProperty("sizeOfRings");
            EditorGUILayout.PropertyField(num);
            _pitchNum = num.intValue;
            
            //EditorGUILayout.PropertyField(pitches);
            for (var i = 0; i < _pitchNum; i++)
            {
                EditorGUILayout.PropertyField(_pitches.GetArrayElementAtIndex(i),new GUIContent("Pitch"+(i+1)));
                
            }
            
            serializedObject.ApplyModifiedProperties();
            _mimicDoor.setRingsIsActive();
            _mimicDoor.moveRings();
            _mimicDoor.changeRingEdgeColor();
        }
        
    }
}
