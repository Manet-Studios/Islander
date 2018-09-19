using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Image thumbnailIcon;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private Button[] optionButtons;

    private bool waitingToContinue;

    private DialogueOption nextOption;

    public void StartDialogue(Sprite thumbnail, bool playerOption, string text, IEnumerable<DialogueOption> nextOptions)
    {
        waitingToContinue = !playerOption;
        if (playerOption == false)
        {
        }
    }

    private void Update()
    {
        if (waitingToContinue && Input.anyKey)
        {
        }
    }
}