using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace BobboNet.Editor
{
    public abstract class SearchWindowContent : PopupWindowContent
    {
        private SearchField searchField;
        private string searchString = "";
        private int maxKeys;
        private int selectedIndex = 0;
        private string selectedText = "";
        private int currentKeyCount = 0;
        private Vector2 lastMousePosition;
        private List<string> keyList;
        private UnityEngine.Events.UnityAction<string> onCompleteString;



        public SearchWindowContent(string initialString, UnityEngine.Events.UnityAction<string> onCompleteString, int maxKeys = 10)
        {
            searchField = new SearchField();
            searchField.SetFocus();
            searchField.downOrUpArrowKeyPressed += OnUpOrDownKeyPressed;

            searchString = initialString;
            this.maxKeys = maxKeys;
            this.onCompleteString = onCompleteString;
        }



        public override Vector2 GetWindowSize()
        {
            float height = EditorStyles.toolbarSearchField.fixedHeight;

            if (keyList != null)
            {
                height += keyList.Count * EditorGUIUtility.singleLineHeight;
            }


            return new Vector2(200, height);
        }

        public override void OnGUI(Rect rect)
        {
            Rect toolbarRect = new Rect(rect.x, rect.y, rect.width, EditorStyles.toolbarSearchField.fixedHeight);
            Rect contentRect = new Rect(rect.x, rect.y + toolbarRect.height, rect.width, rect.height - toolbarRect.height);

            searchString = searchField.OnToolbarGUI(toolbarRect, searchString);
            EditorGUI.DrawRect(contentRect, Color.grey);

            keyList = GenerateKeyList(searchString, maxKeys);
            Rect buttonRect = new Rect(rect.x, contentRect.y, rect.width, EditorGUIUtility.singleLineHeight);

            float currentMouseDelta = (Event.current.mousePosition - lastMousePosition).sqrMagnitude;


            selectedIndex = keyList.IndexOf(selectedText);
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            for (int i = 0; i < keyList.Count; i++)
            {
                if (buttonRect.Contains(Event.current.mousePosition))
                {
                    if (currentMouseDelta > 0.1f)
                    {
                        selectedIndex = i;
                    }

                    if (Event.current.type == EventType.MouseUp)
                    {
                        SelectResult();
                    }
                }

                if (selectedIndex == i)
                {
                    EditorGUI.DrawRect(buttonRect, new Color32(180, 180, 180, 255));
                }

                GUI.Label(buttonRect, keyList[i]);
                buttonRect.y += buttonRect.height;
            }



            if (Event.current.keyCode == KeyCode.Tab && keyList.Count > 0)
            {
                searchString = keyList[selectedIndex];
                searchField.SetFocus();

            }






            lastMousePosition = Event.current.mousePosition;
            if (keyList.Count > 0)
            {
                selectedText = keyList[selectedIndex];
            }



            if (Event.current.keyCode == KeyCode.Return)
            {
                SelectResult(true);
            }



            if (EditorWindow.focusedWindow == editorWindow && EditorWindow.mouseOverWindow == editorWindow)
            {
                editorWindow.Repaint();
            }
        }

        private void SelectResult(bool useWhiteSpace = false)
        {
            if (string.IsNullOrWhiteSpace(searchString) && useWhiteSpace)
            {
                onCompleteString.Invoke("");
            }
            else if (keyList.Count > 0)
            {
                searchString = keyList[selectedIndex];
                onCompleteString.Invoke(searchString);
            }
            editorWindow.Close();
        }

        private void OnUpOrDownKeyPressed()
        {
            int delta = 0;
            if (Event.current.keyCode == KeyCode.UpArrow)
            {
                delta = -1;
            }
            else if (Event.current.keyCode == KeyCode.DownArrow)
            {
                delta = 1;
            }


            if (selectedIndex + delta >= currentKeyCount)
            {
                selectedIndex = currentKeyCount - 1;
            }
            else if (selectedIndex + delta < 0)
            {
                selectedIndex = 0;
            }
            else
            {
                selectedIndex += delta;
            }

            if (keyList.Count > 0)
            {
                selectedText = keyList[selectedIndex];
            }
        }



        private List<string> GenerateKeyList(string filterString, int maxKeyCount)
        {
            bool isEmptyFilter = string.IsNullOrWhiteSpace(filterString);

            string[] allKeys = GetKeys();
            List<string> filteredKeys = new List<string>();
            currentKeyCount = 0;

            for (int i = 0; i < allKeys.Length; i++)
            {
                if (isEmptyFilter || allKeys[i].Contains(filterString))
                {
                    filteredKeys.Add(allKeys[i]);
                }
            }


            List<string> output = new List<string>();
            for (int i = 0; i < maxKeyCount && i < filteredKeys.Count; i++)
            {
                currentKeyCount++;
                output.Add(filteredKeys[i]);
            }




            return output;
        }


        protected abstract string[] GetKeys();
    }
}
