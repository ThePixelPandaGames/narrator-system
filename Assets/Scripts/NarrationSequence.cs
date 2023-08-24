using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new narration sequence", menuName = "Scriptables/Create new narration sequence")]
public class NarrationSequence : ScriptableObject
{
    [TextArea(minLines:3, maxLines:10)]
    public string[] npc_sentences;
    public PlayerChoice[] player_choices;
}
