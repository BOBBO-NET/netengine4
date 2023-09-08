using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using System.Text;

namespace BobboNet
{
    [RequireComponent(typeof(Canvas))]
    public class DevConsoleManager : MonoBehaviour
    {
        [System.Serializable]
        public class StateChangeEvent : UnityEngine.Events.UnityEvent<bool> { }

        [System.Serializable]
        public class Settings
        {
            public TMP_FontAsset font;
            public int sortingOrder = -100;
        }

        public StateChangeEvent onStateChange;

        [SerializeField]
        private TMP_InputField inputField;

        [SerializeField]
        private TextMeshProUGUI logText;

        private Settings settings;
        private DevConsoleLogic consoleLogic;
        private Canvas canvas;
        private bool consoleOpened = false;
        private bool flagLogUpdate = false;
        private List<string> currentConsoleStrings;
        private const int maxConsoleStrings = 200;

        //
        //  Initialization
        //

        [Inject]
        public void Inject(DevConsoleManager.Settings settings, DevConsoleLogic consoleLogic)
        {
            this.settings = settings;
            this.consoleLogic = consoleLogic;
        }

        public void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.sortingOrder = settings.sortingOrder;

            foreach (TextMeshProUGUI textMesh in GetComponentsInChildren<TextMeshProUGUI>())
            {
                textMesh.font = settings.font;
            }

            // Setup components
            currentConsoleStrings = new List<string>();
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += OnUnityLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= OnUnityLog;
        }

        //
        //  Tick
        //

        private void Update()
        {
            if (consoleOpened)
            {
                UpdateWhileOpened();
            }
            else
            {
                UpdateWhileClosed();
            }
        }

        private void UpdateWhileOpened()
        {
            // Force the input field to be active
            if (!inputField.isFocused) inputField.ActivateInputField();

            if (flagLogUpdate)
            {
                flagLogUpdate = false;
                OnUpdatedLogs();
            }

            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Escape))
            {
                SetState(false);
            }
        }

        private void UpdateWhileClosed()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote) && Input.GetKey(KeyCode.LeftShift))
            {
                SetState(true);
            }
        }



        //
        //  Public Methods
        //

        public void SetState(bool isOpen)
        {
            consoleOpened = isOpen;
            canvas.enabled = isOpen;

            if (isOpen)
            {
                inputField.enabled = true;
                inputField.ActivateInputField();
                inputField.text = "";
            }
            else
            {
                inputField.text = "";
                inputField.DeactivateInputField();
                inputField.enabled = false;
            }

            onStateChange.Invoke(isOpen);
        }

        //
        //  Private Methods
        //

        private void OnUnityLog(string logString, string stackTrace, LogType type)
        {
            if (currentConsoleStrings.Count > maxConsoleStrings)
            {
                currentConsoleStrings.RemoveAt(0);
            }
            currentConsoleStrings.Add($"[{type}] {logString}");

            flagLogUpdate = true;
        }

        private void OnInputFieldEndEdit(string inp)
        {
            if (!(consoleOpened && Input.GetButtonDown("Submit"))) return;

            // Pass the input into the dev console, and process it
            Debug.Log(consoleLogic.ProcessCommand(inp));
            inputField.text = "";
        }

        private void OnUpdatedLogs()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < currentConsoleStrings.Count; i++)
            {
                sb.AppendLine(currentConsoleStrings[i]);
            }

            logText.text = sb.ToString();
        }
    }
}
