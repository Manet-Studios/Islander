using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dialoguer : MonoBehaviour, IInteractable
{
    public event Action<Sprite, bool, string, IEnumerable<DialogueOption>> ChooseDialogueOptionEventHandler;

    [SerializeField]
    private List<DialogueOption> dialogueOptions;

    public int relationshipScore;
    private DialogueManager dm;

    private void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        ChooseDialogueOptionEventHandler += dm.StartDialogue;
    }

    private void OnDestroy()
    {
        ChooseDialogueOptionEventHandler = null;
    }

    public void Interact()
    {
        if (ChooseDialogueOptionEventHandler != null)
        {
            DialogueOption availableOption = dialogueOptions.Where(d => relationshipScore < d.relationshipRange.y && relationshipScore >= d.relationshipRange.x).FirstOrDefault();
            if (availableOption != null)
            {
                ChooseDialogueOptionEventHandler(availableOption.thumbnail, availableOption.playerOption, availableOption.dialogueText, availableOption.dialogueOptions.Where(d => relationshipScore < d.relationshipRange.y && relationshipScore >= d.relationshipRange.x));
            }
        }
    }

    public void ChooseDialogueOption(DialogueOption option)
    {
        relationshipScore += option.relationshipReward;

        if (ChooseDialogueOptionEventHandler != null)
        {
            ChooseDialogueOptionEventHandler(option.thumbnail, option.playerOption, option.dialogueText, option.dialogueOptions.Where(d => relationshipScore < d.relationshipRange.y && relationshipScore >= d.relationshipRange.x));
        }
    }
}

public interface IInteractable
{
    void Interact();
}

[System.Serializable]
public class DialogueOption
{
    public Sprite thumbnail;
    public bool playerOption;

    public Vector2Int relationshipRange;

    public string dialogueText;

    public int relationshipReward;

    public DialogueOption[] dialogueOptions;
}