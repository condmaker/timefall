using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for pausing and skipping the currently playing video
/// </summary>
public class PauseVideo : MonoBehaviour
{
    /// <summary>
    /// Scene to sckip to when the video is skipped
    /// </summary>
    [SerializeField]
    private string sceneName = default;

    /// <summary>
    /// Pause menu displayed when the video is paused
    /// </summary>
    [SerializeField]
    private GameObject pauseMenu = default;

    /// <summary>
    /// Object that is controlling the video
    /// </summary>
    [SerializeField]
    private PlayableDirector timeline = default;

    /// <summary>
    /// Video that is currently playing
    /// </summary>
    [SerializeField]
    private VideoPlayer video = null;

    /// <summary>
    /// Update is called once per frame
    /// </summary> 
    void Update()
    {
        if (pauseMenu.activeSelf)
        {
            //Video is paused
            if (Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                video.Play();
                timeline.Resume();
                pauseMenu.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(sceneName);
            }

        }
        else
        {
            //Video is playing
            if (Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Return))
            {
                video.Pause();
                timeline.Pause();
                pauseMenu.SetActive(true);
            }
        }
    }
    
}
