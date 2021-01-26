using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


/// <summary>
/// Method responsible for leaving the cutscene Scene when the video
/// has stop playing
/// </summary>
public class LeaveCutSceneScene : MonoBehaviour
{
    /// <summary>
    /// The VideoPlayer component of the object
    /// </summary>
    private VideoPlayer videoPlayer;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    public void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += LoadNextScene;
    }

    private void LoadNextScene(VideoPlayer source)
    {
        SceneManager.LoadScene("First Stratum");
    }
}
