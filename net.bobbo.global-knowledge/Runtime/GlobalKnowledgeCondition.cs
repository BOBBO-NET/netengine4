namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class GlobalKnowledgeCondition
    {
        public enum ConditionType
        {
            None,
            Bool,
            Float,
            String,
            Item
        }

        public enum BoolState
        {
            TransitionIfTrue,
            TransitionIfFalse
        }

        public enum FloatState
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        public enum StringState
        {
            Equals,
            NotEquals
        }

        public enum ItemState
        {
            HasItem,
            DoesNotHaveItem
        }

        public enum TimeRunState
        {
            IsEqual,
            IsNotEqual
        }





        public ConditionType conditionType;
        public string name;

        public BoolState boolState;

        public float floatValue;
        public FloatState floatState;

        public string stringValue;
        public StringState stringState;

        // TODO - re-introduce
        // public ScriptableInventoryItem itemValue;
        public ItemState itemState;
    }
}