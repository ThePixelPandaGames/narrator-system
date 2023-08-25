using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    [SerializeField]
    float letteringSpeed = 0.2f;

    private Queue<string> npc_sentences;
    private List<PlayerChoice> player_choices;

    public TextMeshProUGUI npc_text;

    public Button player_choice_button_prefab;
    public GameObject UI;

    private List<Button> current_player_choices_buttons;

    public Transform[] player_choice_spawn_pos;


    private static NarrationManager instance;

    public static NarrationManager Instance { get => instance; set => instance = value; }

    public bool makeLettersAppearOneByOne = false;

    private NarrationSequence current_narrationSequence;

    /// hide this in inspector
    public bool is_npc_talking = false;
    public bool is_player_talking = false;

    public GameObject continue_button;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        npc_sentences = new Queue<string>();
        player_choices = new List<PlayerChoice>();
        current_player_choices_buttons = new List<Button>();
    }



    private void InitializeNarration()
    {
        npc_sentences.Clear();
        player_choices.Clear();

        DeleteAllPrevPlayerChoiceButtons();

        npc_text.text = "";
        Debug.Log("text: " + npc_text.text);

        foreach (var npcSentence in current_narrationSequence.NpcSentences)
        {
            npc_sentences.Enqueue(npcSentence);
        }
        foreach (var playerChoice in current_narrationSequence.PlayerChoices)
        {
            player_choices.Add(playerChoice);
        }

        UI.SetActive(true);

        if (makeLettersAppearOneByOne)
        {
            continue_button.SetActive(false);
        }

    }

    public void StartNarration(NarrationSequence narrationSequence)
    {
        if (narrationSequence == null)
        {
            EndNarration();
            return;
        }
        current_narrationSequence = narrationSequence;

        InitializeNarration();

        ContinueNarration();
    }

    public void ContinueNarration()
    {
        try
        {
            is_npc_talking = true;
            is_player_talking = false;

            if (npc_sentences.Count > 0)
            {
                if (makeLettersAppearOneByOne == false)
                {
                    npc_text.text = npc_sentences.Dequeue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(Lettering(npc_sentences.Dequeue()));
                }
            }
            else if (player_choices.Count > 0)
            {
                foreach (var playerChoice in player_choices)
                {

                    int index = player_choices.IndexOf(playerChoice);
                    CreatePlayerChoiceButton(playerChoice, index);
                    is_player_talking = true;
                    is_npc_talking = false;
                }
            }
            else
            {
                // Handle narration completion here
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
        Button button = Instantiate(player_choice_button_prefab, player_choice_spawn_pos[index].position, Quaternion.identity);
        current_player_choices_buttons.Add(button);
        button.GetComponentInChildren<TextMeshProUGUI>().text = playerChoice.Choice;
        button.onClick.AddListener(delegate { StartNarration(playerChoice.NextNarrationSequence); });
        button.transform.SetParent(UI.transform);
    }

    private void EndNarration()
    {
        //npc_text.text = "";
        UI.SetActive(false);
        is_npc_talking = false;
        if (current_narrationSequence != null && current_narrationSequence.MyEvent != null)
        {
            current_narrationSequence.callEvent();
        }

        // disable all buttons and all UI etc.
    }

    private void DeleteAllPrevPlayerChoiceButtons()
    {

        foreach (var button in instance.current_player_choices_buttons)
        {
            Destroy(button.gameObject);
        }
        instance.current_player_choices_buttons.Clear();
    }

    IEnumerator Lettering(string sentence)
    {
        npc_text.text = "";
        continue_button.SetActive(false);
        foreach (char letter in sentence.ToCharArray())
        {
            npc_text.text += letter;
            yield return new WaitForSeconds(letteringSpeed);
        }
        Debug.Log("Finished Lettering");
        // make continue button appear here
        continue_button.SetActive(true);
        is_npc_talking = false;
    }
}
