using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityStats
{
    public float health { get; set; }

    public void takeDamage(float damage);
    public void heal(float increase);
}
