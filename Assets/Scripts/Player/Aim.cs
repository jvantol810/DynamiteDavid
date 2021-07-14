using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public Camera cam;
    public GameObject reticle;
    public Vector2 reticlePos;
    [Header("Distance From Player")]
    public Vector2 reticleDistance;
    public float aimAngle;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Vector2 reticleDir = mousePos.normalized;
        //reticlePos = m_rigidbody.position + reticleDir * reticleDistance;
        //reticle.transform.position = reticlePos;

        //Vector2 lookDirection = reticlePos;
        //float aimAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        //m_rigidbody.rotation = aimAngle;

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public float getAimAngle()
    {
        return aimAngle;
    }

}
