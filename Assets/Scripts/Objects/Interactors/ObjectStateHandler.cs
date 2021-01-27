using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class responsible for managing an object with multiple states
/// </summary>
public class ObjectStateHandler : MonoBehaviour
{
    /// <summary>
    /// List of Interactors that affect this class
    /// </summary>
    private IEnumerable<Interactor> interactor;

    /// <summary>
    /// Max state that the obejct can reach
    /// </summary>
    [SerializeField]
    private short maxStates = 2;

    /// <summary>
    /// Property that defines the max state the object can reach
    /// </summary>
    public short MaxStates => maxStates;

    /// <summary>
    /// Current state of the object
    /// </summary>
    [SerializeField]
    private short state = 0;
   
    /// <summary>
    /// Property that defines the current state of the object
    /// </summary>
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

    /// <summary>
    /// Sound to be played when the object changes states
    /// </summary>
    [SerializeField]
    private AudioClip sound = null;

    /// <summary>
    /// Sound manager responsible for playing the audio of the game
    /// </summary>
    [SerializeField]
    private SoundMng soundManager = null;

    public event Action<ObjectStateHandler,short> OnChangeState;

    /// <summary>
    /// Animator component of this object
    /// </summary>
    private Animator anim;


    /// <summary>
    /// Variable that defines if its the first time the object has changed states
    /// </summary>
    private bool firstStateChange;

    /// <summary>
    /// Method called when tehe scene starts
    /// </summary>
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
                i.OnGoTo += ChangeToState;
            }
        }

        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Method called before the first frame of the Update
    /// </summary>
    public void Start()
    {
        PlayAnimation();
    }

   
    /// <summary>
    /// Method responsible for changing to the next state following 
    /// the iteration
    /// </summary>
    /// <param name="iteration">Iteration Type to follow</param>
    private void ChangeToNext(IterationType iteration)
    {
        short newState = (short)(State + (short)iteration);
        ChangeToState(newState);
    }


    /// <summary>
    /// Method responsible for changing to the next last state 
    /// </summary>
    private void ChangeToLast()
    {
        short newState = (short)(maxStates - 1);
        ChangeToState(newState);
    }


    /// <summary>
    /// Method responsible for changing to a specific state
    /// </summary>
    /// <param name="wantedState">Specific state to change to</param>
    public void ChangeToState(short wantedState)
    {
        //if (isPlaying) return;
        firstStateChange = true;

        State = wantedState;      
        PlayAnimation();
        OnChangeState?.Invoke(this, State);
    }


    /// <summary>
    /// Method responsible for playing the animation and the sound 
    /// when the object changes state
    /// </summary>
    public void PlayAnimation()
    {
        //Sound
        if ((sound != null) && firstStateChange)
            soundManager.PlaySound(sound, transform.position);

        if (anim == null) return;

        anim.SetFloat("State", State);
        anim.SetTrigger("ChangeState");
    }

}

