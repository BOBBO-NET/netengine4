using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    public static class GlobalKnowledge
    {
        [System.Serializable]
        public class KnowledgeEvent : UnityEngine.Events.UnityEvent { }

        [System.Serializable]
        public class KnowledgeBoolEvent : UnityEngine.Events.UnityEvent<string, bool> { }

        [System.Serializable]
        public class KnowledgeFloatEvent : UnityEngine.Events.UnityEvent<string, float> { }

        [System.Serializable]
        public class KnowledgeStringEvent : UnityEngine.Events.UnityEvent<string, string> { }

        private class KnowledgeDictionary<T>
        {
            private Dictionary<string, T> dict;

            public KnowledgeDictionary()
            {
                dict = new Dictionary<string, T>();
            }






            public bool Get(string key, out T value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    value = default(T);
                    return false;
                }

                if (dict.ContainsKey(key))
                {
                    value = dict[key];
                    return true;
                }
                else
                {
                    value = default(T);
                    return false;
                }
            }

            public void Set(string key, T value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError("Tried to set knowledge with an empty or null key!");
                }

                if (dict.ContainsKey(key))
                {
                    dict[key] = value;
                }
                else
                {
                    dict.Add(key, value);
                }
            }

            public Dictionary<string, T>.KeyCollection GetKeys()
            {
                return dict.Keys;
            }
        }




        public static KnowledgeEvent onKnowledgeUpdated;
        public static KnowledgeBoolEvent onKnowledgeBoolUpdated;
        public static KnowledgeFloatEvent onKnowledgeFloatUpdated;
        public static KnowledgeStringEvent onKnowledgeStringUpdated;

        private static KnowledgeDictionary<bool> booleanDict;
        private static KnowledgeDictionary<float> floatDict;
        private static KnowledgeDictionary<string> stringDict;


        static GlobalKnowledge()
        {
            booleanDict = new KnowledgeDictionary<bool>();
            floatDict = new KnowledgeDictionary<float>();
            stringDict = new KnowledgeDictionary<string>();


            onKnowledgeUpdated = new KnowledgeEvent();
            onKnowledgeBoolUpdated = new KnowledgeBoolEvent();
            onKnowledgeFloatUpdated = new KnowledgeFloatEvent();
            onKnowledgeStringUpdated = new KnowledgeStringEvent();
        }






        public static bool EvaluateCompoundCondition(CompoundGlobalKnowledgeCondition compoundCondition)
        {
            if (compoundCondition.conditions == null || compoundCondition.conditions.Count == 0)
            {
                return false;
            }


            bool result = EvaluateCondition(compoundCondition.conditions[0]);
            for (int i = 1; i < compoundCondition.conditions.Count; i++)
            {
                switch (compoundCondition.operators[i - 1])
                {
                    case CompoundGlobalKnowledgeCondition.BooleanOperator.And:
                        result = result && EvaluateCondition(compoundCondition.conditions[i]);
                        break;

                    case CompoundGlobalKnowledgeCondition.BooleanOperator.Or:
                        result = result || EvaluateCondition(compoundCondition.conditions[i]);
                        break;
                }
            }

            // Dear future icy:
            // technically, this should have some sort of order of operations. In C#, && evaluates before ||.
            // this code does not have that.
            // I don't think it will be an issue in the future? who knows?
            // You know.
            //          ~Past Icy

            return result;
        }

        public static bool EvaluateCondition(GlobalKnowledgeCondition condition)
        {
            switch (condition.conditionType)
            {
                case GlobalKnowledgeCondition.ConditionType.Bool:
                    return EvaluateConditionBool(condition);

                case GlobalKnowledgeCondition.ConditionType.Float:
                    return EvaluateConditionFloat(condition);

                case GlobalKnowledgeCondition.ConditionType.String:
                    return EvaluateConditionString(condition);

                case GlobalKnowledgeCondition.ConditionType.Item:
                    return EvaluateConditionItem(condition);

                case GlobalKnowledgeCondition.ConditionType.None:
                    return true;

                default:
                    Debug.LogError("This should never get called!!");
                    return false;
            }
        }

        private static bool EvaluateConditionBool(GlobalKnowledgeCondition condition)
        {
            bool value;
            bool hasKnowledge = GetBoolean(condition.name, out value);

            if (!hasKnowledge)
            {
                value = false;
            }

            if (condition.boolState == GlobalKnowledgeCondition.BoolState.TransitionIfTrue)
            {
                return value;
            }
            else
            {
                return !value;
            }
        }

        private static bool EvaluateConditionFloat(GlobalKnowledgeCondition condition)
        {
            float value;
            bool hasKnowledge = GetFloat(condition.name, out value);

            if (!hasKnowledge)
            {
                return false;
            }

            switch (condition.floatState)
            {
                case GlobalKnowledgeCondition.FloatState.LessThan:
                    return value < condition.floatValue;

                case GlobalKnowledgeCondition.FloatState.LessThanOrEqualTo:
                    return value <= condition.floatValue;

                case GlobalKnowledgeCondition.FloatState.EqualTo:
                    return value == condition.floatValue;

                case GlobalKnowledgeCondition.FloatState.GreaterThanOrEqualTo:
                    return value >= condition.floatValue;

                case GlobalKnowledgeCondition.FloatState.GreaterThan:
                    return value > condition.floatValue;


                default:
                    Debug.LogError("This should never print!");
                    return false;
            }
        }

        private static bool EvaluateConditionString(GlobalKnowledgeCondition condition)
        {
            string value;
            bool hasKnowledge = GetString(condition.name, out value);

            if (!hasKnowledge)
            {
                return false;
            }


            switch (condition.stringState)
            {
                case GlobalKnowledgeCondition.StringState.Equals:
                    return value.Equals(condition.stringValue);

                case GlobalKnowledgeCondition.StringState.NotEquals:
                    return !value.Equals(condition.stringValue);

                default:
                    Debug.LogError("This should never print!!");
                    return false;
            }
        }

        private static bool EvaluateConditionItem(GlobalKnowledgeCondition condition)
        {
            // TODO - Reimplement
            // BobboNet.Player.PlayerController playerController = GameController.GetSystem<BobboNet.Player.PlayerController>();
            // //bool playerHasItem = playerController.GetInventoryController().ContainsItem(condition.itemValue);

            // // Todo - implement above
            // bool playerHasItem = true;

            // switch (condition.itemState)
            // {
            //     case GlobalKnowledgeCondition.ItemState.HasItem:
            //         return playerHasItem;

            //     case GlobalKnowledgeCondition.ItemState.DoesNotHaveItem:
            //         return !playerHasItem;

            //     default:
            //         Debug.LogError("This should never get called!");
            //         break;
            // }

            return false;
        }








        public static void SetValue(GlobalKnowledgeValue value)
        {
            switch (value.type)
            {
                case GlobalKnowledgeValue.ValueType.Bool:
                    SetBoolean(value.key, value.boolValue);
                    break;

                case GlobalKnowledgeValue.ValueType.Float:
                    SetFloat(value.key, value.floatValue);
                    break;

                case GlobalKnowledgeValue.ValueType.String:
                    SetString(value.key, value.stringValue);
                    break;
            }
        }


        public static bool GetBoolean(string key, out bool value)
        {
            return booleanDict.Get(key, out value);
        }

        public static bool GetBoolean(string key, out GlobalKnowledgeValue value)
        {
            bool boolValue;

            if (booleanDict.Get(key, out boolValue))
            {
                value = new GlobalKnowledgeValue();
                value.key = key;
                value.boolValue = boolValue;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public static void SetBoolean(string key, bool value)
        {
            booleanDict.Set(key, value);
            onKnowledgeUpdated.Invoke();
            onKnowledgeBoolUpdated.Invoke(key, value);
        }

        public static Dictionary<string, bool>.KeyCollection GetBooleanKeys()
        {
            return booleanDict.GetKeys();
        }



        public static bool GetFloat(string key, out float value)
        {
            return floatDict.Get(key, out value);
        }

        public static bool GetFloat(string key, out GlobalKnowledgeValue value)
        {
            float floatValue;

            if (floatDict.Get(key, out floatValue))
            {
                value = new GlobalKnowledgeValue();
                value.key = key;
                value.floatValue = floatValue;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public static void SetFloat(string key, float value)
        {
            floatDict.Set(key, value);
            onKnowledgeUpdated.Invoke();
            onKnowledgeFloatUpdated.Invoke(key, value);
        }

        public static Dictionary<string, float>.KeyCollection GetFloatKeys()
        {
            return floatDict.GetKeys();
        }



        public static bool GetString(string key, out string value)
        {
            return stringDict.Get(key, out value);
        }

        public static bool GetString(string key, out GlobalKnowledgeValue value)
        {
            string stringValue;

            if (stringDict.Get(key, out stringValue))
            {
                value = new GlobalKnowledgeValue();
                value.key = key;
                value.stringValue = stringValue;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public static void SetString(string key, string value)
        {
            stringDict.Set(key, value);
            onKnowledgeUpdated.Invoke();
            onKnowledgeStringUpdated.Invoke(key, value);
        }

        public static Dictionary<string, string>.KeyCollection GetStringKeys()
        {
            return stringDict.GetKeys();
        }



    }
}