using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Translater : MonoBehaviour
{
    public float maxVelocity = 5f;
    public float speed = 1f;

    private Rigidbody rb;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Move(JoystickEvent jEvent) {
        if (rb.velocity.magnitude < maxVelocity)
            rb.AddForce(jEvent.direction * speed);
    }

    public void MoveTo(Vector3 pos) {
        if (rb.velocity.magnitude < maxVelocity)
            rb.AddForce((pos - transform.position).normalized * 3.5f);
    }
}
