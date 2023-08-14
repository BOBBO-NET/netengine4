using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using BobboNet.Knowledge;
using BobboNet.XNode;

namespace BobboNet.Timelines
{

    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineGraphDirector : MonoBehaviour
    {
        public static Dictionary<string, TimelineGraphDirector> directors;


        [Header("Required Settings")]
        public string directorName = "SETME";

        [Header("Required Reference")]
        public TimelineGraph timelineGraph;

        [Header("Settings")]
        public bool startOnAwake = true;
        public TimelineDirectorSettings directorSettings = new TimelineDirectorSettings();


        private PlayableDirector playableDirector;
        private TimelineNode currentNode;

        private bool[] settingsCache;
        private bool playingTimeline = false;
        private double lastTime = 0;




        private void Awake()
        {
            GetComponents();
            SetupComponents();

            if (startOnAwake)
            {
                StartGraph();
            }
        }

        private void GetComponents()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        private void SetupComponents()
        {
            settingsCache = new bool[directorSettings.settings.Length];
        }



        private void OnEnable()
        {
            AddToDictionary();
        }

        private void OnDisable()
        {
            RemoveFromDictionary();
        }



        private void AddToDictionary()
        {
            if (directors == null)
            {
                // first!!!
                directors = new Dictionary<string, TimelineGraphDirector>();
            }

            if (directorName.Equals("SETME") || directors.ContainsKey(directorName))
            {
                Debug.LogError("YO! You need to set the name of " + gameObject.name + "'s TimelineGraphDirector to something unique!");
            }

            directors.Add(directorName, this);
        }

        private void RemoveFromDictionary()
        {
            if (directors == null)
            {
                return;
            }


            if (directors.ContainsKey(directorName))
            {
                directors.Remove(directorName);
            }
        }
















        private void Update()
        {
            if (playingTimeline)
            {
                CheckForTransition();

                if (playableDirector.extrapolationMode != DirectorWrapMode.Loop)
                {
                    if (playableDirector.time >= playableDirector.playableAsset.duration || (playableDirector.time - lastTime) < 0)
                    {
                        CheckForEndTransition();
                    }
                }

                lastTime = playableDirector.time;
            }

            UpdateSettings();
        }

        private void CheckForTransition()
        {
            for (int i = 0; i < currentNode.exits.Count; i++)
            {
                GlobalKnowledgeCondition condition = currentNode.exits[i];

                bool canTransition = GlobalKnowledge.EvaluateCondition(condition) && condition.conditionType != GlobalKnowledgeCondition.ConditionType.None;

                if (canTransition)
                {
                    GoToNextState(i);
                    return;
                }
            }
        }

        private void CheckForEndTransition()
        {
            for (int i = 0; i < currentNode.exits.Count; i++)
            {
                if (currentNode.exits[i].conditionType == GlobalKnowledgeCondition.ConditionType.None)
                {
                    GoToNextState(i);
                    return;
                }
            }
        }


        private void UpdateSettings()
        {
            CheckForSettingsCacheChanges();
            for (int i = 0; i < directorSettings.settings.Length; i++)
            {
                EvaluateSetting(i, directorSettings.settings[i]);
            }
        }

        private void CheckForSettingsCacheChanges()
        {
            int delta = settingsCache.Length - directorSettings.settings.Length;
            if (delta != 0)
            {
                bool[] newSettingsCache = new bool[directorSettings.settings.Length];


                if (delta > 0)
                {
                    for (int i = 0; i < directorSettings.settings.Length; i++)
                    {
                        newSettingsCache[i] = settingsCache[i];
                    }
                }
                else
                {
                    for (int i = 0; i < settingsCache.Length; i++)
                    {
                        newSettingsCache[i] = settingsCache[i];
                    }
                }

                settingsCache = newSettingsCache;
            }
        }

        private void EvaluateSetting(int index, DirectorSetting setting)
        {
            bool active = GlobalKnowledge.EvaluateCompoundCondition(setting.condition);

            if (active == settingsCache[index])
            {
                return;
            }

            settingsCache[index] = active;

            switch (setting.settingType)
            {
                case DirectorSetting.SettingType.ShouldPause:
                    if (active)
                    {
                        playableDirector.Pause();
                    }
                    else
                    {
                        if (playableDirector.state == PlayState.Paused)
                        {
                            playableDirector.Resume();
                        }
                    }
                    break;

            }

        }




        private void GoToNextState(int exitIndex)
        {
            GlobalKnowledgeConditionNode node = currentNode.GetNext(exitIndex);

            if (node != null)
            {
                SetCurrentNode((TimelineNode)node);
            }
            else
            {
                SetCurrentNode(null);
            }
        }





        private void StartGraph()
        {
            if (timelineGraph == null)
            {
                return;
            }

            SetCurrentNode(timelineGraph.startingNode);
        }

        private void SetCurrentNode(TimelineNode newNode)
        {
            currentNode = newNode;
            lastTime = 0;

            if (currentNode != null && currentNode.timeline != null)
            {
                playableDirector.playableAsset = currentNode.timeline;
                playableDirector.extrapolationMode = currentNode.wrapMode;
                playableDirector.time = 0;
                playableDirector.Play();
                playingTimeline = true;
            }
            else
            {
                playableDirector.Stop();
                playableDirector.playableAsset = null;
                playingTimeline = false;

                FinishGraph();
            }
        }

        private void FinishGraph()
        {

            Debug.Log(directorName + "STOPPED!");
        }

    }
}
