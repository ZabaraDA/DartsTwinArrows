using System.Collections;
using UnityEngine;

public interface ILevelView
{
    void SetText(string text);
    void StartEnemyCoroutine(IEnumerator coroutine);
}
