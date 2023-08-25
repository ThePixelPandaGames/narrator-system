using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Choice", menuName = "Scriptables/Create new player choice")]
public class PlayerChoice : ScriptableObject
{
    [TextArea(minLines:3, maxLines:10)]
    [SerializeField]
    private string choice;

    [SerializeField]
    private NarrationSequence nextNarrationSequence;

    public string Choice { get => choice; }
    public NarrationSequence NextNarrationSequence { get => nextNarrationSequence; }
}
