using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BobboNet.Knowledge;

namespace BobboNet.Timelines
{
    [System.Serializable]
    public class DirectorSetting
    {
        public enum SettingType
        {
            ShouldPause
        }

        public SettingType settingType;
        public CompoundGlobalKnowledgeCondition condition;
    }
}
