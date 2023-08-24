using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Choice", menuName = "Scriptables/Create new player choice")]
public class PlayerChoice : ScriptableObject
{
    [TextArea(minLines:3, maxLines:10)]
    public string choice;

    public NarrationSequence next_narrationSequence;
}
