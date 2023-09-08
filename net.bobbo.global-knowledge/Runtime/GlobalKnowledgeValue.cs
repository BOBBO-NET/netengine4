using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class GlobalKnowledgeValue
    {
        public enum ValueType
        {
            Bool,
            Float,
            String
        }

        public string key = "";
        public ValueType type;

        public bool boolValue = true;
        public float floatValue = 0;
        public string stringValue = "";
    }
}