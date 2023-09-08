using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using XNode;
using BobboNet.Knowledge;

namespace BobboNet.XNode
{
    public class GlobalKnowledgeConditionNode : Node
    {
        [System.Serializable]
        public class EmptyClass { /* no data needed */}

        public List<GlobalKnowledgeCondition> exits = new List<GlobalKnowledgeCondition>();



        public GlobalKnowledgeConditionNode GetNext(int exitChoice)
        {
            NodePort nextPort;

            nextPort = GetOutputPort("e" + exitChoice);

            if (!nextPort.IsConnected)
            {
                return null;
            }

            return nextPort.Connection.node as GlobalKnowledgeConditionNode;
        }



        public void AddExit()
        {
            if (exits == null)
            {
                exits = new List<GlobalKnowledgeCondition>();
            }

            exits.Add(new GlobalKnowledgeCondition());
            AddDynamicOutput(typeof(EmptyClass), ConnectionType.Override, TypeConstraint.Inherited, "e" + (exits.Count - 1));
        }

        public void RemoveExit()
        {
            if (exits == null || exits.Count == 0)
            {
                Debug.Log("Tried to remove an exit when no exits exist!");
                return;
            }

            exits.RemoveAt(exits.Count - 1);
            RemoveDynamicPort("e" + exits.Count);
        }
    }

}
