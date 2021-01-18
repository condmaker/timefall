using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DialogueSystem;
using System;
using TMPro;

public class DialogueDisplayHandler : MonoBehaviour
{
    [SerializeField]
    private bool OnLoad;

    [SerializeField]
    private TextMeshProUGUI dialogueDisplayTarget; 
    // Depois maybe tirar o serializable
    [SerializeField]
    private DialogueScript currentScript;
    [SerializeField]
    private float displaySpeed;

    [SerializeField]
    private GameObject buttonLayout;
    [SerializeField]
    private GameObject buttonPREFAB;


    private NodeData dialogueLine;

    private string currentGUID;
    private string dialogueText;

    private WaitForSeconds effectSpeed;

    private bool ended;
    private bool inDialogue;
    public Action endDialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (OnLoad)
        {
            StartDialolgue(currentScript);
        }
    }

    public void StartDialolgue(DialogueScript script)
    {
        
        currentScript = script;
        PrepareNewDialogue();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PassDialogue();
        }
    }

    public void PassDialogue()
    {
        

        if (inDialogue)
        {
            if (ended)
            {
                if (buttonLayout.transform.childCount > 0)
                    return;
                NextLine(0);
            }
            else
            {
                dialogueDisplayTarget.text += dialogueText;
                dialogueText = "";
                buttonLayout.SetActive(true);
                ended = true;
                StopCoroutine("TypeWriterEffect");
            }

        }
    }


    public void PrepareNewDialogue()
    {
        //Display Specificationss
        effectSpeed = new WaitForSeconds(displaySpeed);
        
        //Initialize First Line
        dialogueLine = currentScript.GetNodeByIndex(0);
        dialogueText = currentScript.GetNodeByIndex(0).Dialogue;
        currentGUID = currentScript.GetNodeByIndex(0).GUID;

        //Handle button Layout
        InstatiateChoices();
        DisplayLine();
    }

    private void InstatiateChoices()
    {

        foreach(Transform g in buttonLayout.transform)
        {
            Destroy(g.gameObject);
        }

        int choiceNumb = dialogueLine.OutPorts.Count;
        if (choiceNumb == 0) return;
        
        //This probably needs a rework :c
        for(int i = 0; i < choiceNumb; i++)
        {          
            GameObject temp = Instantiate(buttonPREFAB, transform.position, Quaternion.identity, buttonLayout.transform);


            //Depois mudar isto 
            temp.GetComponent<TextMeshProUGUI>().text = dialogueLine.OutPorts[i].Name;

            ChoiceSelector cs = temp.GetComponent<ChoiceSelector>();
            cs.ChoiceNumb = i;
            cs.NextLine = NextLine;
        }

        buttonLayout.SetActive(false);
        
    }


    public void NextLine(int choice)
    {
        dialogueLine = 
               currentScript.GetNextNode(dialogueLine, choice);
       
        if (dialogueLine == null)
        {
            EndDialogue();
            return;
        }

        dialogueText = dialogueLine.Dialogue;

        InstatiateChoices();
        DisplayLine();
    }

    private void EndDialogue()
    {
        endDialogue?.Invoke();
        inDialogue = false;
        dialogueDisplayTarget.text = "";
        StopCoroutine("TypeWriterEffect");
    }

    private void DisplayLine()
    {
        StopCoroutine("TypeWriterEffect");
        StartCoroutine("TypeWriterEffect");
    }

    IEnumerator TypeWriterEffect()
    {
        inDialogue = true;
        ended = false;
        dialogueDisplayTarget.text = "";
        while (dialogueText.Length > 0)
        {
            yield return effectSpeed;
            char nextChar = dialogueText[0];
            dialogueDisplayTarget.text += nextChar;
            dialogueText = dialogueText.Substring(1);
        }
        ended = true;
        buttonLayout.SetActive(true);
    }
                

}
