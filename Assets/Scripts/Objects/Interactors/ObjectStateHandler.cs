using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class ObjectStateHandler : MonoBehaviour
{

    private IEnumerable<Interactor> interactor;

    [SerializeField]
    private short maxStates;
    public short MaxStates => maxStates;

    [SerializeField]
    private short state;
    public short State 
    {
        get
        {
            return state;
        }
        set
        {
            if (value >= maxStates)
                state = 0;
            else if (value < 0)
                state = (short)(maxStates - 1);
            else
                state = value;
        }
    }

    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    private SoundMng soundManager;

    public event Action<ObjectStateHandler,short> OnChangeState;

    private Animator anim;

    private void Awake()
    {
        interactor = GetComponents<Interactor>();
        foreach (Interactor i in interactor)
        {
            if (i != null)
            {
                i.OnGoToNext += ChangeStates;
                i.OnGoToLast += ChangeToLast;
                i.OnGoTo += ChangeToState;
            }
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
    private void ChangeStates(IterationType iteration)
    {
        State += (short)iteration;
        //ActivateOtherObjects();
        OnChangeState?.Invoke(this,State);

        if (anim == null) return;
        //Cenas realcionadas com o efeito do states
        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
        //Actions
        if(sound != null)
            soundManager.PlaySound(sound, transform.position);

        //etc
    }

    private void ChangeToLast()
    {
        short mx = (short)(maxStates - 1);
        ChangeToState(mx);
    }

    //Change to the passed state
    public void ChangeToState(short wantedState)
    {
        
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

