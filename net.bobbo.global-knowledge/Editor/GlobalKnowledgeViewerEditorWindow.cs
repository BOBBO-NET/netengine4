using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    public class GlobalKnowledgeViewerEditorWindow : EditorWindow
    {
        public Vector2 boolScrollPosition;
        public Vector2 floatScrollPosition;
        public Vector2 stringScrollPosition;

        private bool boolFoldout;
        private bool floatFoldout;
        private bool stringFoldout;


        [MenuItem("Window/Bobbo-Net/Global Knowledge Viewer")]
        public static void ShowWindow()
        {
            GetWindow(typeof(GlobalKnowledgeViewerEditorWindow));
        }

        private void OnGUI()
        {
            // TODO - Reimplement
            // bool isEnabled = GameController.hasStarted;
            bool isEnabled = false;


            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Global Knowledge Viewer", EditorStyles.boldLabel);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);


            var boolKeys = GlobalKnowledge.GetBooleanKeys();
            var floatKeys = GlobalKnowledge.GetFloatKeys();
            var stringKeys = GlobalKnowledge.GetStringKeys();

            EditorGUI.BeginDisabledGroup(!isEnabled);


            boolFoldout = EditorGUILayout.Foldout(boolFoldout, "Bool Knowledge");
            if (boolFoldout)
            {
                EditorGUI.indentLevel++;
                boolScrollPosition = EditorGUILayout.BeginScrollView(boolScrollPosition);
                foreach (string key in boolKeys)
                {
                    Rect rect = EditorGUILayout.BeginVertical(EditorStyles.textArea);
                    EditorGUILayout.LabelField(key);
                    EditorGUI.indentLevel++;
                    bool value;
                    GlobalKnowledge.GetBoolean(key, out value);
                    EditorGUILayout.Toggle(value);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(2);
                }
                EditorGUILayout.EndScrollView();
                EditorGUI.indentLevel--;
            }


            floatFoldout = EditorGUILayout.Foldout(floatFoldout, "Float Knowledge");
            if (floatFoldout)
            {
                EditorGUI.indentLevel++;
                floatScrollPosition = EditorGUILayout.BeginScrollView(floatScrollPosition);
                foreach (string key in floatKeys)
                {
                    Rect rect = EditorGUILayout.BeginVertical(EditorStyles.textArea);
                    EditorGUILayout.LabelField(key);
                    EditorGUI.indentLevel++;
                    float value;
                    GlobalKnowledge.GetFloat(key, out value);
                    EditorGUILayout.FloatField(value);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(2);
                }
                EditorGUILayout.EndScrollView();
                EditorGUI.indentLevel--;
            }


            stringFoldout = EditorGUILayout.Foldout(stringFoldout, "String Knowledge");
            if (stringFoldout)
            {
                EditorGUI.indentLevel++;
                stringScrollPosition = EditorGUILayout.BeginScrollView(stringScrollPosition);
                foreach (string key in stringKeys)
                {
                    Rect rect = EditorGUILayout.BeginVertical(EditorStyles.textArea);
                    EditorGUILayout.LabelField(key);
                    EditorGUI.indentLevel++;
                    string value;
                    GlobalKnowledge.GetString(key, out value);
                    EditorGUILayout.TextField(value);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(2);
                }
                EditorGUILayout.EndScrollView();
                EditorGUI.indentLevel--;
            }





            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();

        }

        private void Update()
        {
            // TODO - Reimplement
            // if (GameController.hasStarted)
            // {
            //     Repaint();
            // }
        }
    }
}