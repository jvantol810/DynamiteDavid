using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public Camera cam;
    public GameObject reticle;
    [Header("Distance From Player")]
    public Vector2 reticleDistance;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 reticleDir = mousePos.normalized;
        Vector2 reticlePos = m_rigidbody.position + reticleDir * reticleDistance;
        reticle.transform.position = reticlePos;
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePos - m_rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        m_rigidbody.rotation = angle;
    }
}
