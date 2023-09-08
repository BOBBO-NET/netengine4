using UnityEditor;
using UnityEngine;
using XNodeEditor;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;
using BobboNet.Timelines;

namespace BobboNet.Editor.Timelines
{
    [CustomNodeGraphEditor(typeof(TimelineGraph))]
    public class TimelineGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            if (typeof(TimelineNode).IsAssignableFrom(type))
            {
                return base.GetNodeMenuName(type);
            }
            else return null;
        }
    }
}
