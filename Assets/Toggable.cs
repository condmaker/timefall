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
            if (state == maxStates)
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

        if(CompareCollections(wantedStateComb,currentStateComb))
        {
            ChangeStates();
        }
    }


    //Existe um cena chamada Enumerable.SequenceEqual 
    //q pode ser melhor para usar aqui
    public bool CompareCollections(IList want, IList got)
    {
        for(short i = 0; i < want.Count; i++)
        {
            if (want[i] != got[i])
                return false;
        }

        return true;
    }

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

}


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