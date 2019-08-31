using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider slider;
    private HealthSystem target;

    public void Init(HealthSystem target)
    {
        this.target = target;
        target.OnHealthChanged += ChangeValue;
        ChangeValue(target.GetHealthPercent());
    }

    private void ChangeValue(float value)
    {
        slider.value = value;
    }
}
