using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    public class GlobalKnowledgeMonobehaviour : MonoBehaviour
    {
        public void SetBoolToTrue(string key)
        {
            GlobalKnowledge.SetBoolean(key, true);
        }

        public void SetBoolToFalse(string key)
        {
            GlobalKnowledge.SetBoolean(key, false);
        }
    }
}