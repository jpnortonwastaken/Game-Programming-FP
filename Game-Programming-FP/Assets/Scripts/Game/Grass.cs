using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    GameObject Player;
    GameObject Pivot;

    public float distance_x;
    public float distance_y;
    public float distance_z;
    public float angle;
    public float angleOfRotation;
    public float angleOfRotation2;

    public Transform targetRotation;
    public float lerpSpeed;
    public bool useSlerp = true;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Pivot = gameObject;
        lerpSpeed = 10.0f;
    }

    void Update()
    {
        distance_x = Player.transform.position.x - Pivot.transform.position.x;
        distance_y = Mathf.Abs(Player.transform.position.y - Pivot.transform.position.y);
        distance_z = Player.transform.position.z - Pivot.transform.position.z;

        angle = Mathf.Atan2(distance_z, distance_x) * Mathf.Rad2Deg;

        Vector2 stickInput = new Vector2(distance_x, distance_z);
        Vector3 stickInput3 = new Vector3(stickInput.x, 0f, stickInput.y);

        Vector3 axisOfRotation = Vector3.Cross(Vector3.up, stickInput3);

        angleOfRotation = Mathf.Clamp((-10 / stickInput.magnitude), -45f, -15f);
        angleOfRotation2 = (0f + (angleOfRotation - (-15f)) * (-45f - 0f) / (-45f - (-15f)));

        float rotationScaleFactor = Mathf.Clamp(1 - (distance_y / 2), 0.1f, 1.0f);
        angleOfRotation2 *= rotationScaleFactor;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(angleOfRotation2, axisOfRotation);

        if (useSlerp)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, Time.deltaTime * lerpSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
