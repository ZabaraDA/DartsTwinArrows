using System.Collections;
using UnityEngine;

public interface ILevelView
{
    void StartEnemyCoroutine(IEnumerator coroutine);
}
