using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHeal
{
    float GetHealth();

    void Heal(float value);

    void Damage(float value);
}
