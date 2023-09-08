using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using XNode;
using BobboNet.XNode;
using BobboNet.Knowledge;

namespace BobboNet.Timelines
{
    public class TimelineNode : GlobalKnowledgeConditionNode
    {
        [Input] public EmptyClass previous;

        public TimelineAsset timeline;
        public DirectorWrapMode wrapMode = DirectorWrapMode.None;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        public override object GetValue(NodePort port) { return null; }

        public TimelineNode GetPrevious()
        {
            NodePort previousPort = GetInputPort("previous");

            if (!previousPort.IsConnected)
            {
                return null;
            }

            return previousPort.Connection.node as TimelineNode;
        }

        //
        //  Context Menu Item
        //

        [ContextMenu("Make Starting Node")]
        public void MakeStartingNode()
        {
            TimelineGraph parentGraph = graph as TimelineGraph;

            parentGraph.startingNode = this;
            //UnityEditor.EditorUtility.SetDirty(parentGraph);
        }
    }
}
