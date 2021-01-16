using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueDisplayHandler : MonoBehaviour
{

    public Action endDialogue;

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





    // Start is called before the first frame update
    private void Start()
    {
    }

    public void StartDialolgue(DialogueScript script)
    {
        currentScript = script;
        PrepareNewDialogue();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextLine(0);
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
        //There needs to be a function that checks the count of 
        //The output ports in data an instantiates buttons accordingly
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
        
        //Dialogue Window needs option string
        string[] optionS = new string[] { "Yes", "No" };

        //This probably needs a rework :c
        for(int i = 0; i < choiceNumb; i++)
        {          
            GameObject temp = Instantiate(buttonPREFAB, transform.position, Quaternion.identity, buttonLayout.transform);


            //Depois mudar isto 
            temp.GetComponent<TextMeshProUGUI>().text = optionS[i];

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
        dialogueDisplayTarget.text = "";

        while (dialogueText.Length > 0)
        {
            yield return effectSpeed;
            char nextChar = dialogueText[0];
            dialogueDisplayTarget.text += nextChar;
            dialogueText = dialogueText.Substring(1);
        }
        buttonLayout.SetActive(true);
    }
                

}
