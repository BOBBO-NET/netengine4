using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    public class GlobalKnowledgeKeyDatabaseEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private string keyToAdd = "";
        private string searchString = "";


        private void OnEnable()
        {
            scrollPosition.x = EditorPrefs.GetFloat("GKEW_ScrollX", 0);
            scrollPosition.y = EditorPrefs.GetFloat("GKEW_ScrollY", 0);
            searchString = EditorPrefs.GetString("GKEW_SearchString", "");
        }

        private void OnDisable()
        {
            EditorPrefs.SetFloat("GKEW_ScrollX", scrollPosition.x);
            EditorPrefs.SetFloat("GKEW_ScrollY", scrollPosition.y);
            EditorPrefs.SetString("GKEW_SearchString", searchString);
        }


        [MenuItem("Window/Bobbo-Net/Global Knowledge Database")]
        public static void ShowWindow()
        {
            GetWindow(typeof(GlobalKnowledgeKeyDatabaseEditorWindow));
        }

        private void OnGUI()
        {



            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Global Knowledge Database Keys:");

            EditorGUILayout.LabelField("Filter:");
            searchString = EditorGUILayout.TextField(searchString);


            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            List<string> currentKeys = GenerateKeyList();
            for (int i = 0; i < currentKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.textArea);

                EditorGUILayout.TextField(currentKeys[i]);
                GUILayout.FlexibleSpace();
                bool removeSpecificButtonPressed = GUILayout.Button("X");

                EditorGUILayout.EndHorizontal();


                if (removeSpecificButtonPressed)
                {
                    GlobalKnowledgeKeyDatabase.RemoveAt(i);
                    i--;
                }
            }

            EditorGUILayout.EndScrollView();

            GUILayout.FlexibleSpace();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key To Add:", GUILayout.Width(80));
            keyToAdd = EditorGUILayout.TextField(keyToAdd);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            bool buttonRemovePressed = GUILayout.Button("-");
            bool buttonAddPressed = GUILayout.Button("+");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();



            if (buttonRemovePressed)
            {
                OnButtonRemovePressed();
            }

            if (buttonAddPressed)
            {
                OnButtonAddPressed();
            }
        }



        private void OnButtonRemovePressed()
        {
            GlobalKnowledgeKeyDatabase.RemoveKey(keyToAdd);
            keyToAdd = "";
        }

        private void OnButtonAddPressed()
        {
            GlobalKnowledgeKeyDatabase.AddKey(keyToAdd);
            keyToAdd = "";
        }






        private List<string> GenerateKeyList()
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return new List<string>(GlobalKnowledgeKeyDatabase.GetKeys());
            }


            string[] allKeys = GlobalKnowledgeKeyDatabase.GetKeys();
            List<string> output = new List<string>();

            for (int i = 0; i < allKeys.Length; i++)
            {
                if (allKeys[i].StartsWith(searchString))
                {
                    output.Add(allKeys[i]);
                }
            }


            return output;
        }
    }
}