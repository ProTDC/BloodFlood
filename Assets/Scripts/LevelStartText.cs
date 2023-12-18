using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStartText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;
    public float textLinger;
    private bool textFinished = false;

    private int index;
    private void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (textFinished)
        {
            StopAllCoroutines();
            text.text = string.Empty;

        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);

        }

        yield return new WaitForSeconds(textLinger);
        textFinished = true;
    }
}
