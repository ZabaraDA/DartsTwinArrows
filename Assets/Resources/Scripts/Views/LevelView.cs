using System.Collections;
using UnityEngine;

public class LevelView : MonoBehaviour, ILevelView
{
    public void StartEnemyCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
