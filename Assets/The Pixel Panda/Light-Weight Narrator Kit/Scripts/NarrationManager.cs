using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightWeightNarrationTool
{
    public class NarrationManager : MonoBehaviour
    {
        // inspectables

        [SerializeField]
        private float letteringSpeed = 0.2f;

        [SerializeField]
        private TextMeshProUGUI npcText;

        [SerializeField]
        private TextMeshProUGUI npcName;

        [SerializeField]
        private GameObject continueButton;

        [SerializeField]
        private Button playerChoiceButtonPrefab;

        [SerializeField]
        private GameObject UI;

        [SerializeField]
        private Transform[] playerChoiceSpawnPos;

        [SerializeField]
        private bool makeLettersAppearOneByOne = false;

        [SerializeField]
        private bool dontDestroyOnSceneChange = false;


        // update v1.1
        [SerializeField]
        private GameObject imageContainer;


        // privates

        private static NarrationManager instance;
        private bool isNpcTalking = false;
        private bool isPlayerTalking = false;
        private List<Button> currentPlayerChoiceButtons;
        private NarrationSequence currentNarrationSequence;
        private Queue<string> npcSentences;
        private List<PlayerChoice> playerChoices;

        // accessors
        public static NarrationManager Instance { get => instance; set => instance = value; }
        public bool IsNpcTalking { get => isNpcTalking; }
        public bool IsPlayerTalking { get => isPlayerTalking; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            npcSentences = new Queue<string>();
            playerChoices = new List<PlayerChoice>();
            currentPlayerChoiceButtons = new List<Button>();

            if (dontDestroyOnSceneChange)
            {
                DontDestroyOnLoad(this);
            }
        }

        public bool NoOneIsTalking()
        {
            return (IsNpcTalking == false && isPlayerTalking == false);
        }



        private void InitializeNarration()
        {
            npcSentences.Clear();
            playerChoices.Clear();

            DeleteAllPrevPlayerChoiceButtons();

            npcText.text = "";


            if (currentNarrationSequence.NarratorName != "")
            {
                npcName.gameObject.SetActive(true);
                npcName.text = currentNarrationSequence.NarratorName;
            }
            else
            {
                Debug.Log(currentNarrationSequence.NarratorName);
                npcName.gameObject.SetActive(false);
                npcName.text = "";
            }

            foreach (var npcSentence in currentNarrationSequence.NpcSentences)
            {
                npcSentences.Enqueue(npcSentence);
            }
            foreach (var playerChoice in currentNarrationSequence.PlayerChoices)
            {
                playerChoices.Add(playerChoice);
            }

            UI.SetActive(true);

            if (makeLettersAppearOneByOne)
            {
                continueButton.SetActive(false);
            }
        }

        public void StartNarration(NarrationSequence narrationSequence)
        {
            if (narrationSequence == null)
            {
                EndNarration();
                return;
            }
            currentNarrationSequence = narrationSequence;

            InitializeNarration();

            ContinueNarration();
        }

        public void ContinueNarration()
        {
            try
            {
                isNpcTalking = true;
                isPlayerTalking = false;

                if (npcSentences.Count > 0)
                {
                    if (makeLettersAppearOneByOne == false)
                    {
                        npcText.text = npcSentences.Dequeue();
                        isNpcTalking = false;
                    }
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(Lettering(npcSentences.Dequeue()));
                    }
                }
                else if (playerChoices.Count > 0)
                {
                    foreach (var playerChoice in playerChoices)
                    {

                        int index = playerChoices.IndexOf(playerChoice);
                        CreatePlayerChoiceButton(playerChoice, index);
                        isPlayerTalking = true;
                        isNpcTalking = false;
                    }
                }
                else
                {
                    EndNarration();
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e);
                Debug.LogError("NarrationManager possibly not set up in your scene/project. Check if you have one (and only one) instance.");
            }


        }

        private void CreatePlayerChoiceButton(PlayerChoice playerChoice, int index)
        {
            Button button = Instantiate(playerChoiceButtonPrefab, playerChoiceSpawnPos[index].position, Quaternion.identity);
            currentPlayerChoiceButtons.Add(button);
            button.GetComponentInChildren<TextMeshProUGUI>().text = playerChoice.Choice;
            button.onClick.AddListener(delegate { StartNarration(playerChoice.NextNarrationSequence); });
            button.transform.SetParent(UI.transform);
        }

        // update v1.1
        private void EndNarration()
        {
            UI.SetActive(false);
            isNpcTalking = false;
            if (currentNarrationSequence != null && currentNarrationSequence.MyEvent != null)
            {
                currentNarrationSequence.callEvent();
            }

            
            if (currentNarrationSequence.PlayerChoices.Length == 0 && currentNarrationSequence.NextNarrationSequence != null)
            {
                StartNarration(currentNarrationSequence.NextNarrationSequence);
            }
            else if (currentNarrationSequence.PlayerChoices.Length > 0 && currentNarrationSequence.NextNarrationSequence != null)
            {
                throw new Exception("You can not have Player Choices AND another narration sequence follow after the end of this sequence.");
            }

            DeleteAllPrevPlayerChoiceButtons();
        }

        private void DeleteAllPrevPlayerChoiceButtons()
        {

            foreach (var button in instance.currentPlayerChoiceButtons)
            {
                Destroy(button.gameObject);
            }
            instance.currentPlayerChoiceButtons.Clear();
        }

        IEnumerator Lettering(string sentence)
        {
            npcText.text = "";
            continueButton.SetActive(false);
            foreach (char letter in sentence.ToCharArray())
            {
                npcText.text += letter;
                yield return new WaitForSeconds(letteringSpeed);
            }
            continueButton.SetActive(true);
            isNpcTalking = false;
        }


        // update v1.1
        private void AddOrChangeNPCImage()
        {
            if (currentNarrationSequence.NpcImage != null)
            {
                imageContainer.SetActive(true);
                imageContainer.GetComponent<Image>().sprite = currentNarrationSequence.NpcImage;
            }
            else
            {
                imageContainer.SetActive(false);
            }
        }
    }
}
