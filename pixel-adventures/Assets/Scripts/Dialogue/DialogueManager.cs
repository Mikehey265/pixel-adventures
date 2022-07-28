using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    #region Public Variables

    [SerializeField] public GameObject dialogueBox;
    public bool justStarted;
        
    #endregion

    #region Private Variables

    [SerializeField] int currentLine;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject nameBox;
    [SerializeField] GameObject portraitWindow;
    string[] dialogueLines;
    const string startsWithSignifierString = "n-";
    bool isPlayer;
        
    #endregion

    #region Public Methods

    public void ContinueDialogue(){
        if(!justStarted){
            currentLine++;
            if(currentLine >= dialogueLines.Length){
                dialogueBox.SetActive(false);
                justStarted = true;
                PlayerController.Instance.canMove = true;
                PlayerController.Instance.canAttack = true;
            }else{
                CheckName();
                dialogueText.text = dialogueLines[currentLine];
            }
        }else{
            justStarted = false;
        }
    }

    public void ShowDialogue(string[] newLines, bool isPerson){
        justStarted = true;
        dialogueLines = newLines;
        currentLine = 0;
        CheckName();

        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        nameBox.SetActive(isPerson);
        PlayerController.Instance.canMove = false;
        ContinueDialogue();
    }

    public void ShowPortrait(bool isPerson){
        if(isPerson == false){
            portraitWindow.transform.parent.gameObject.SetActive(false);
        }else{
            portraitWindow.transform.parent.gameObject.SetActive(true);
        }
    }

    public void CheckName(){
        if(dialogueLines[currentLine].StartsWith(startsWithSignifierString)){
            nameText.text = dialogueLines[currentLine].Replace(startsWithSignifierString, "");
            currentLine++;

            if(nameText.text == "Mike"){
                portraitWindow.GetComponent<Image>().sprite = PlayerController.Instance.portrait;
            }else{
                portraitWindow.GetComponent<Image>().sprite = DialogueActivator.FindObjectOfType<DialogueActivator>().portrait;
            }
        }
    }
        
    #endregion
}
