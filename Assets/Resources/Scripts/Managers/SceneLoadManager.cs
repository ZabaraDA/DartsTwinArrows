using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
