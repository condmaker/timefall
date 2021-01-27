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

    [SerializeField]
    private string sceneName;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    public void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += LoadNextScene;
    }

    /// <summary>
    /// Method responsible for leaving the scene and loading the next one
    /// </summary>
    /// <param name="source">source of the VideoPlayer component</param>
    private void LoadNextScene(VideoPlayer source)
    {
        SceneManager.LoadScene(sceneName);
    }
}
