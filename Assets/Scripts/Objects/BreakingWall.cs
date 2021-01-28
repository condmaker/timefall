using UnityEngine;

/// <summary>
/// Class responsible for handling breaking the breakeble wall enity
/// </summary>
public class BreakingWall : MonoBehaviour
{
    /// <summary>
    /// Animatior component
    /// </summary>
    private Animator wallAnim;

    /// <summary>
    /// Sound the entity makes
    /// </summary>
    [SerializeField]
    private AudioClip sound = default;

    /// <summary>
    /// Sound Manager component responsible for playing the audio of the game
    /// </summary>
    [SerializeField]
    private SoundMng soundManager;



    /// <summary>
    /// Method called before the first frame update
    /// </summary>
    private void Start()
    {
        wallAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// Method responsible for breaking the entity
    /// </summary>
    public void Break()
    {
        soundManager.PlaySound(sound, transform.position);
        wallAnim.SetTrigger("break");
    }
}
