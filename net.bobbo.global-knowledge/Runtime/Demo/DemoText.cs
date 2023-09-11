using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace BobboNet.Knowledge.Demo
{
    [RequireComponent(typeof(Text))]
    public class DemoText : MonoBehaviour
    {
        public KnowledgeAny knowledge;

        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            knowledge.AddOnValueChangedListener(OnKnowledgeChanged);
        }

        private void OnKnowledgeChanged(object newValue)
        {
            text.text = $"Value is '{newValue}'";
        }
    }
}

