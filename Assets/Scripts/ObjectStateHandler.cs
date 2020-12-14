using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

//[RequireComponent(typeof(Animator))]
public class ObjectStateHandler : MonoBehaviour
{
    public IInteractor interactor;

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
            if (state >= maxStates-1)
                state = 0;
            else
                state = value;
        }
    }  

    public event Action<ObjectStateHandler,short> OnChangeState;

    private Animator anim;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
        if (interactor != null)
        {
            interactor.OnGoToNext += ChangeStates;          
            interactor.OnGoToFirst += ChangeToFirst;
            interactor.OnGoToLast += ChangeToLast;
        }

        anim = GetComponent<Animator>();
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
    //------------------------------

    //Change to the next state
    private void ChangeStates()
    {
        State++;
        //ActivateOtherObjects();
        OnChangeState?.Invoke(this,State);

        if (anim == null) return;
        //Cenas realcionadas com o efeito do states
        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
        //Actions
        

        //etc
    }

    private void ChangeToLast()
    {
        short mx = (short)(maxStates - 1);
        ChangeStates(mx);
    }

    private void ChangeToFirst()
    {
        ChangeStates(0);
    }

    //Change to the passed state
    private void ChangeStates(short wantedState)
    {
        if (State == wantedState) return;
        State = wantedState;
        OnChangeState?.Invoke(this, State);
        //Cenas relacionadas com o efeito dos states

        if (anim == null) return;
        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
                   
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

