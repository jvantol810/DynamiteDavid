using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this way of making damage zones is very scuffed and I am rushed for time. sorry for the pain viewing my quick solution has caused you
public class DamageZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        PlayerStats stats = other.GetComponent<PlayerStats>();

        if (stats != null) {
            stats.takeDamage(5);
        }
    }
}
