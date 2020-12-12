using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//[RequireComponent(typeof(Animator))]
public class Toggable : MonoBehaviour
{

    [SerializeField]
    private short maxStates;

    //[SerializeField]
    private short state;
    public short State 
    {
        get
        {
            return state;
        }
        set
        {
            if (state == 0) state = 1;
            if (state >= maxStates)
                state = 1;
            else
                state = value;
        }
    }

    [HideInInspector]
    public bool multipleTogglers;

    //Keep both at zero if object is affected by only one toggler
    [SerializeField]
    private List<Toggler> togglers;
    [SerializeField]
    private List<short> wantedStateComb;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        State = 1;
    }

    public void CheckCombinations()
    {
        List<short> currentStateComb = new List<short>();
        //Sim, sim. Isto depois pode ser um for calem-se
        foreach(Toggler t in togglers)
        {
            currentStateComb.Add(t.State);
        }

        bool isEqual = CompareCollections(wantedStateComb, currentStateComb);

        if (isEqual)
        {
            ChangeStates();
        }
        else
        {
            //Default to first state -- 
            //Talvez por isto como variavel mas por agora n interessa
            ChangeStates(1);
        }
    }


    //Existe um cena chamada Enumerable.SequenceEqual 
    //q pode ser melhor para usar aqui
    //Fun fact se isto forem interfaces eles n comparam bem
    public bool CompareCollections(List<short> want, List<short> got)
    {

        for (short i = 0; i < want.Count; i++)
        {
            if (want[i] != got[i])
                return false;

        }
        return true;
    }


    //Change to the next state
    private void ChangeStates()
    {
        State++;
        if (anim == null) return;
        //Cenas realcionadas com o efeito do states
        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
        //Actions
        //etc
    }

    //Change to the passed state
    private void ChangeStates(short wantedState)
    {
        if (State == wantedState) return;
        State = wantedState;

        //Cenas relacionadas com o efeito dos states
       
        if (anim == null) return;
        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
        
        //Actions
        
        //etc
    }



    //PARA TESTES
    public void printList(ICollection c)
    {
        string l = "";
        foreach (short s in c)
        {
            l += s + "  |  ";
        }
        print(l);
    }

}



//Isto é para fazer uma cena quirky don't worry
#if UNITY_EDITOR
[CustomEditor(typeof(Toggable))]
public class Toggable_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        Toggable script = (Toggable)target;

        // draw checkbox for the bool
        script.multipleTogglers = EditorGUILayout.Toggle("Multiple Togglers", script.multipleTogglers);
        if (script.multipleTogglers) // if bool is true, show other fields
        {
            //script.togglers = EditorGUILayout.ObjectField("Togglers", script.togglers, typeof(InputField), true) as InputField;
            //script.wantedStateComb = EditorGUILayout.ObjectField("Wanted Comp", script.wantedStateComb, typeof(GameObject), true) as GameObject;
        }
    }
}
#endif