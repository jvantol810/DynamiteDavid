using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityStats
{
    public float health { get; set; }
    public float maxHealth { get; set; }
    public void takeDamage(float damage);
    public void heal(float increase);
    public void die();
}
