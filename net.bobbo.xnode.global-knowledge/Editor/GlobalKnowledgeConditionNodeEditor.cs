using UnityEditor;
using UnityEngine;
using XNodeEditor;
using BobboNet.Knowledge;
using BobboNet.XNode;
using BobboNet.Editor;
using BobboNet.Editor.Knowledge;

namespace BobboNet.Editor.XNode
{
    [CustomNodeEditor(typeof(GlobalKnowledgeConditionNode))]
    public class GlobalKnowledgeConditionNodeEditor : NodeEditor
    {

        //
        //  Public Methods
        //

        public static void DrawKnowledgeCondition(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {
            switch (condition.conditionType)
            {
                case GlobalKnowledgeCondition.ConditionType.None:
                    DrawKnowledgeConditionNone(condition, objectToDirty);
                    break;
                case GlobalKnowledgeCondition.ConditionType.String:
                    DrawKnowledgeConditionString(condition, objectToDirty);
                    break;
                case GlobalKnowledgeCondition.ConditionType.Bool:
                    DrawKnowledgeConditionBool(condition, objectToDirty);
                    break;
                case GlobalKnowledgeCondition.ConditionType.Float:
                    DrawKnowledgeConditionFloat(condition, objectToDirty);
                    break;
                case GlobalKnowledgeCondition.ConditionType.Item:
                    // TODO - Re-implement
                    DrawKnowledgeConditionNone(condition, objectToDirty);
                    // DrawKnowledgeConditionItem(condition, objectToDirty);
                    break;

                default:
                    Debug.LogError("This should never get called!");
                    break;
            }
        }

        private static void DrawKnowledgeConditionName(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {
            DrawGlobalKnowledgeKeyField(condition.name, delegate (string result)
            {
                condition.name = result;
                EditorUtility.SetDirty(objectToDirty);
            });
        }

        public static void DrawGlobalKnowledgeKeyField(string key, UnityEngine.Events.UnityAction<string> onComplete)
        {
            EditorGUILayout.LabelField("Name:");

            GUIStyle style = new GUIStyle(EditorStyles.textField);
            Rect selectionRect = EditorGUILayout.BeginVertical();
            bool pressedField = GUILayout.Button(key, style);
            EditorGUILayout.EndVertical();

            if (pressedField) PopupWindow.Show(selectionRect, new GlobalKnowledgeSearchWindowContent(key, onComplete));

            EditorGUILayout.Space();
        }

        private static void DrawKnowledgeConditionNone(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {

        }

        private static void DrawKnowledgeConditionString(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {
            DrawKnowledgeConditionName(condition, objectToDirty);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("State:", GUILayout.Width(50));
            condition.stringState = (GlobalKnowledgeCondition.StringState)EditorGUILayout.EnumPopup(condition.stringState);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("String:", GUILayout.Width(50));
            condition.stringValue = EditorGUILayout.TextField(condition.stringValue);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawKnowledgeConditionBool(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {
            DrawKnowledgeConditionName(condition, objectToDirty);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("State:", GUILayout.Width(50));
            condition.boolState = (GlobalKnowledgeCondition.BoolState)EditorGUILayout.EnumPopup(condition.boolState);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawKnowledgeConditionFloat(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        {
            DrawKnowledgeConditionName(condition, objectToDirty);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("State:", GUILayout.Width(50));
            condition.floatState = (GlobalKnowledgeCondition.FloatState)EditorGUILayout.EnumPopup(condition.floatState);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Float:", GUILayout.Width(100));
            condition.floatValue = EditorGUILayout.FloatField(condition.floatValue);
            EditorGUILayout.EndHorizontal();
        }

        // TODO - Reimplement
        // private static void DrawKnowledgeConditionItem(GlobalKnowledgeCondition condition, UnityEngine.Object objectToDirty)
        // {
        //     EditorGUILayout.LabelField("Item:");
        //     condition.itemValue = EditorGUILayout.ObjectField(condition.itemValue, typeof(ScriptableInventoryItem), false) as ScriptableInventoryItem;

        //     EditorGUILayout.Space();

        //     EditorGUILayout.BeginHorizontal();
        //     EditorGUILayout.LabelField("State:", GUILayout.Width(50));
        //     condition.itemState = (GlobalKnowledgeCondition.ItemState)EditorGUILayout.EnumPopup(condition.itemState);
        //     EditorGUILayout.EndHorizontal();
        // }

        //
        //  Private Methods
        //

        protected void DrawExitsGUI(GlobalKnowledgeConditionNode node)
        {
            EditorGUILayout.BeginHorizontal();

            bool canRemoveExit = node.exits != null && node.exits.Count > 0;

            bool addButtonPressed = GUILayout.Button("+");
            EditorGUI.BeginDisabledGroup(!canRemoveExit);
            bool removeButtonPressed = GUILayout.Button("-");
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();


            if (addButtonPressed)
            {
                node.AddExit();
            }

            if (removeButtonPressed)
            {
                node.RemoveExit();
            }

            if (node.exits != null)
            {
                for (int i = 0; i < node.exits.Count; i++)
                {
                    DrawExit(node, i);
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }
            }
        }

        private void DrawExit(GlobalKnowledgeConditionNode node, int index)
        {
            GlobalKnowledgeCondition condition = node.exits[index];

            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(index + ":", GUILayout.Width(20));

            condition.conditionType = (GlobalKnowledgeCondition.ConditionType)EditorGUILayout.EnumPopup(condition.conditionType);
            NodeEditorGUILayout.PortField(node.GetOutputPort("e" + index), GUILayout.Width(20));

            GUILayout.EndHorizontal();


            DrawKnowledgeCondition(condition, node);


            /*
            switch (condition.conditionType)
            {
                case GraphCondition.ConditionType.None:
                    DrawExitEnd(condition);
                    break;
                case GraphCondition.ConditionType.Trigger:
                    DrawExitTrigger(condition);
                    break;
                case GraphCondition.ConditionType.Bool:
                    DrawExitBool(condition);
                    break;
                case GraphCondition.ConditionType.Float:
                    DrawExitFloat(condition);
                    break;
            }
            */
        }


    }
}
