using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public Rigidbody ball;

    public Transform barrelBase;
    public Transform barrel;
    public GameObject marker;
    public float maxDistance;
    public float angle;
    public float powerRate = 10f;
    private float power = 0f;
    float prevMag = Mathf.Infinity;
    private float currentAngle;
    private float currentSpeed;

    public void Aim(JoystickEvent jEvent) {
        float mag = jEvent.magnitude;
        if (mag > 0.3f) {
            power = mag * Time.deltaTime * powerRate;
            prevMag = mag;
            marker.transform.position += jEvent.normalizedDirection * power;
        }
    }

    [ContextMenu("Fire")]
    public void Fire()
    {
        ball.transform.position = barrel.transform.position;
        ball.velocity = Vector3.zero;

        SetTargetWithAngle(marker.transform.position, angle);
        ball.velocity = barrel.transform.up * currentSpeed;
    }

    public void SetTargetWithAngle(Vector3 point, float angle)
    {
        currentAngle = angle;

        Vector3 direction = point - barrel.transform.position;
        float yOffset = -direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        currentSpeed = ProjectileMath.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, angle * Mathf.Deg2Rad);

        // projectileArc.UpdateArc(currentSpeed, distance, Physics.gravity.magnitude, currentAngle * Mathf.Deg2Rad, direction, true);
        SetTurret(direction, currentAngle);

        // currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle * Mathf.Deg2Rad, yOffset, Physics.gravity.magnitude);
    }

    private void SetTurret(Vector3 planarDirection, float turretAngle)
    {
        barrelBase.rotation =  Quaternion.LookRotation(planarDirection) * Quaternion.Euler(-90, -90, 0);
        barrel.transform.localRotation = Quaternion.Euler(90, 90, 0) * Quaternion.AngleAxis(turretAngle, Vector3.forward);        
    }
}
