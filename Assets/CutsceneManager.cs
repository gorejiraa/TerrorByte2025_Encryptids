using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CutsceneManager : MonoBehaviour
{

    public MinigameManager minigameManager;
    public TextMeshProUGUI dialogueTextElement;
    public GameObject dialogueBox;
    private float dialogueOpacity = 0f;
    private bool runningDialogue = true;
    private bool dialogueTyped = false;
    private int dialogueIndex = 0;
    private int dialogueLineCharactersTyped = 0;
    private int endOfDialogue;
    private int dialogueTimer = 0;
    public int dialogueSlowness = 5;

    private string[,] dialogue =
    {
        {"Mothman", "Hey you big baddie zombie im gonna kill you"},
        {"Elias", "Nuh uhn"},
        {"Mothman", "Yuh huh buddy youre a stinky little bozo and I'm gonna shoot you with my funny gun"},
        {"Elias", "Nuh uhn nuh uhn nuh uhn"}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endOfDialogue = dialogue.GetLength(0) - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (runningDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!dialogueTyped)
                {
                    dialogueLineCharactersTyped = dialogue[dialogueIndex, 1].Length;
                    dialogueTyped = true;
                }
                else
                {
                    if (dialogueIndex < endOfDialogue)
                    {
                        dialogueIndex++;
                        dialogueLineCharactersTyped = 0;
                        dialogueTyped = false;
                        dialogueOpacity = 0f;
                    }
                    else
                    {
                        dialogueOpacity = 0f;
                        dialogueBox.SetActive(false);
                        runningDialogue = false;
                        minigameManager.StartMinigame();
                    }
                }
            }

            Color boxColor = dialogueBox.GetComponent<Image>().color;
            boxColor.a = Math.Min(boxColor.a + 0.01f, 1f);
            dialogueBox.GetComponent<Image>().color = boxColor;

            dialogueTimer++;

            if (dialogueTimer >= dialogueSlowness) 
            {
                IncrementDialogue();
                dialogueTimer = 0;
            }
            
            FillTextBox();
        }
    }

    void FillTextBox() 
    {
        string dialogueToDisplay = dialogue[dialogueIndex, 1].Substring(0, Math.Min(dialogueLineCharactersTyped, dialogue[dialogueIndex, 1].Length));

        dialogueTextElement.text = dialogueToDisplay;
    }

    void IncrementDialogue() 
    {
        if (dialogueLineCharactersTyped != dialogue[dialogueIndex, 1].Length)
        {
            dialogueLineCharactersTyped = Math.Min(dialogueLineCharactersTyped + 1, dialogue[dialogueIndex, 1].Length);
        }
        else 
        {
            dialogueTyped = true;
        }
    }
}
