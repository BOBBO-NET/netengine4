using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace BobboNet.Knowledge.Demo
{
    [RequireComponent(typeof(Toggle))]
    public class DemoToggle : MonoBehaviour
    {
        [Required]
        public KnowledgeBool knowledgeAsBool;

        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();

            toggle.isOn = knowledgeAsBool.GetCurrentValue();
            knowledgeAsBool.AddOnValueChangedListener(OnKnowledgeChanged);

            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool newValue)
        {
            if (knowledgeAsBool.GetCurrentValue() == newValue) return;
            knowledgeAsBool.SetCurrentValue(newValue);
        }

        private void OnKnowledgeChanged(bool newValue)
        {
            if (toggle.isOn == newValue) return;
            toggle.isOn = newValue;
        }

    }
}

