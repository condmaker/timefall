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

    // Utilizado aqui para ser 1 se não ouver input
    [SerializeField]
    private int volume = 1;

    [SerializeField]
    private SoundMng soundManager;

    public event Action<ObjectStateHandler,short> OnChangeState;

    private Animator anim;

    private bool isPlaying;
    private bool firstStateChange;

    private void Awake()
    {
        firstStateChange = false;

        interactor = GetComponents<Interactor>();
        foreach (Interactor i in interactor)
        {
            if (i != null)
            {
                i.OnGoToNext += ChangeToNext;
                i.OnGoToLast += ChangeToLast;
                i.OnGoTo += ChangeToSpecific;
            }
        }

        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        PlayAnimation();
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


    //Change to the next state following the iteration
    private void ChangeToNext(IterationType iteration)
    {
        short newState = (short)(State + (short)iteration);
        ChangeToState(newState);
    }


    //Change to last state
    private void ChangeToLast()
    {
        short newState = (short)(maxStates - 1);
        ChangeToState(newState);
    }


    //Change to the passed state
    private void ChangeToSpecific(short wantedState)
    {
        ChangeToState(wantedState);               
    }


    //Change to the passed state
    public void ChangeToState(short wantedState)
    {
        //if (isPlaying) return;
        firstStateChange = true;

        State = wantedState;      
        PlayAnimation();
        OnChangeState?.Invoke(this, State);
    }


    public void PlayAnimation()
    {
        //Sound
        if ((sound != null) && firstStateChange)
            soundManager.PlaySound(sound, transform.position);

        if (anim == null) return;

        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");

        //isPlaying = true;
        //StartCoroutine("WaitAnimationOverAndDoThings");
    }

    private IEnumerator WaitAnimationOverAndDoThings()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);        
        isPlaying = false;
       
    }

}

