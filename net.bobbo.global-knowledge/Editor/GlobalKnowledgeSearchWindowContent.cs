using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BobboNet.Editor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    public class GlobalKnowledgeSearchWindowContent : SearchWindowContent
    {
        public GlobalKnowledgeSearchWindowContent(string initialString, UnityAction<string> onCompleteString, int maxKeys = 10) : base(initialString, onCompleteString, maxKeys)
        {

        }

        protected override string[] GetKeys()
        {
            return GlobalKnowledgeKeyDatabase.GetKeys();
        }
    }
}