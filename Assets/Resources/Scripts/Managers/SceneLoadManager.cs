using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoadManager : MonoBehaviour
{
  

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

       
    }

    public void ResetLevel()
    {
        
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
