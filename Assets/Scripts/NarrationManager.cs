using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    // inspectables

    [SerializeField]
    private float letteringSpeed = 0.2f;

    [SerializeField]
    private TextMeshProUGUI npcText;

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
    private bool DontDestroyOnLoad = false;


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

        if(DontDestroyOnLoad) {
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
        Debug.Log("text: " + npcText.text);

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
            Debug.Log("HERE");
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
                // Handle narration completion here
                Debug.Log("HERE 2");

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

    private void EndNarration()
    {
        //npc_text.text = "";
        Debug.Log("END");
        UI.SetActive(false);
        isNpcTalking = false;
        if (currentNarrationSequence != null && currentNarrationSequence.MyEvent != null)
        {
            currentNarrationSequence.callEvent();
        }

        // disable all buttons and all UI etc.
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
        Debug.Log("Finished Lettering");
        // make continue button appear here
        continueButton.SetActive(true);
        isNpcTalking = false;
    }
}
