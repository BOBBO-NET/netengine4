using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public abstract class KnowledgeCondition<KnowledgeType> : IKnowledgeCondition where KnowledgeType : IKnowledge
    {
        public KnowledgeType TargetKnowledge;

        public int TargetCase;

        //
        //  Public Method
        //

        /// <summary>
        /// Check to see if the condition is a match or not.
        /// </summary>
        /// <returns>True if the case matches, false otherwise.</returns>
        public bool CheckCondition() => GetCases().ToArray()[TargetCase].IsCase(TargetKnowledge);

        //
        //  Interface Methods
        //

        public IKnowledge GetTargetKnowledge() => TargetKnowledge;
        public abstract IEnumerable<IKnowledgeCase> GetCases();
    }
}