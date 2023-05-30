using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIXPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text levelText;

    public void UpdateXPBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    public void UpdateLevel(int level)
    {
        levelText.text = level.ToString();
    }
}
