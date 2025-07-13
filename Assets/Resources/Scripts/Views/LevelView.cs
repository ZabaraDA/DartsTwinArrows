using System.Collections;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour, ILevelView
{
    [SerializeField]
    private TMP_Text _text;

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void StartEnemyCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
