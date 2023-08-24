using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
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

 

    private void InitializeNarration(NarrationSequence narrationSequence)
    {
        npc_sentences.Clear();
        player_choices.Clear();

        DeleteAllPrevPlayerChoiceButtons();

        npc_text.text = "";

        foreach (var npcSentence in narrationSequence.npc_sentences)
        {
            npc_sentences.Enqueue(npcSentence);
        }
        foreach (var playerChoice in narrationSequence.player_choices)
        {
            player_choices.Add(playerChoice);
        }  

        UI.SetActive(true);
    }

    public void StartNarration(NarrationSequence narrationSequence)
    {
        if(narrationSequence == null)
        {
            EndNarration();
            return;
        }
        InitializeNarration(narrationSequence);

        ContinueNarration();

 
    }

    public void ContinueNarration()
    {
        Debug.Log("next sentence!");
        if (npc_sentences.Count > 0)
        {
            if (makeLettersAppearOneByOne == false)
            {
                npc_text.text = npc_sentences.Dequeue();
            }else
            {
                StartCoroutine(Lettering(npc_sentences.Dequeue()));
            }
        }
        else if (player_choices.Count > 0)
        {
            foreach (var playerChoice in player_choices)
            {

                int index = player_choices.IndexOf(playerChoice);
                CreatePlayerChoiceButton(playerChoice, index);
            }
        }
        else
        {
            // Handle narration completion here
            EndNarration();
        }
    }

    private void CreatePlayerChoiceButton(PlayerChoice playerChoice, int index)
    {
        Button button = Instantiate(player_choice_button_prefab, player_choice_spawn_pos[index].position, Quaternion.identity);
        current_player_choices_buttons.Add(button);
        button.GetComponentInChildren<TextMeshProUGUI>().text = playerChoice.choice;
        button.onClick.AddListener(delegate { StartNarration(playerChoice.next_narrationSequence); });
        button.transform.SetParent(UI.transform);
    }

    private void EndNarration()
    {
        //npc_text.text = "";
        UI.SetActive(false);

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
        foreach(char letter in sentence.ToCharArray())
        {
            npc_text.text += letter;
            yield return null;
        }
    }
}
