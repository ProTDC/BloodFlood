using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockHealthUI : MonoBehaviour
{
    private Slider slider;
    public void AddHardDamage(int amount)
    {
        StartCoroutine(HardDamage(amount));
    }

    private IEnumerator HardDamage(int amount)
    {
        slider.value += amount;

        yield return new WaitForSeconds(2.5f);

        slider.value -= 0.5f * Time.deltaTime;
    }
}
