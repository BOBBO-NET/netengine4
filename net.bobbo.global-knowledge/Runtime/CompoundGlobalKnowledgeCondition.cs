using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class CompoundGlobalKnowledgeCondition
    {
        public enum BooleanOperator
        {
            And,
            Or
        }


        public List<GlobalKnowledgeCondition> conditions;
        public List<BooleanOperator> operators;










        public void AddCondition(GlobalKnowledgeCondition newCondition)
        {
            VerifyLists();


            conditions.Add(newCondition);
            if (conditions.Count > 1)
            {
                operators.Add(new BooleanOperator());
            }
        }

        public void RemoveCondition()
        {
            VerifyLists();


            if (conditions.Count > 0)
            {
                conditions.RemoveAt(conditions.Count - 1);
            }

            if (operators.Count > 0)
            {
                operators.RemoveAt(operators.Count - 1);
            }
        }







        // Utility Methods
        private void VerifyLists()
        {
            if (conditions == null)
            {
                conditions = new List<GlobalKnowledgeCondition>();
            }
            if (operators == null)
            {
                operators = new List<BooleanOperator>();
            }
        }
    }
}
