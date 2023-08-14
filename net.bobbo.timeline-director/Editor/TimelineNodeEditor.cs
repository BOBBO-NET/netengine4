using UnityEditor;
using UnityEngine;
using XNodeEditor;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using BobboNet.Editor.XNode;
using BobboNet.Timelines;

namespace BobboNet.Editor.Timelines
{
    [CustomNodeEditor(typeof(TimelineNode))]
    public class TimelineNodeEditor : GlobalKnowledgeConditionNodeEditor
    {
        public override void OnHeaderGUI()
        {
            TimelineNode node = target as TimelineNode;
            TimelineGraph graph = node.graph as TimelineGraph;

            if (graph.startingNode == node)
            {
                GUI.color = Color.yellow;
            }
            else
            {
                GUI.color = Color.white;
            }


            string name = node.name;
            if (node.timeline != null)
            {
                name = node.timeline.name;
            }

            GUILayout.Label(name, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
            GUI.color = Color.white;
        }

        public override void OnBodyGUI()
        {
            //base.OnBodyGUI();

            TimelineNode node = target as TimelineNode;
            TimelineGraph graph = node.graph as TimelineGraph;

            NodeEditorGUILayout.PortField(node.GetInputPort("previous"));

            NodeEditorGUILayout.PortField(node.GetOutputPort("next"));

            node.timeline = (TimelineAsset)EditorGUILayout.ObjectField(node.timeline, typeof(TimelineAsset), false);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Wrap Mode:", GUILayout.Width(75));
            node.wrapMode = (DirectorWrapMode)EditorGUILayout.EnumPopup(node.wrapMode);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DrawExitsGUI(node);
        }
    }
}
