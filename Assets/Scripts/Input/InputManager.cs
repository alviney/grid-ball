using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct JoystickEvent {
    public float h;
    public float v;

    public JoystickEvent (float h, float v) {
        this.h = h;
        this.v = v;
    }

    public Vector3 direction { get { return new Vector3(h, 0, v); } }
    public float magnitude { get { return Mathf.Clamp01(direction.magnitude); } }
    public Vector3 normalizedDirection { get { return direction.normalized; } }

}

public class InputManager : MonoBehaviour
{
    public UnityEvent onSwitch;
    public UnityEvent onActionOne;
    public UnityEvent<JoystickEvent> onLeftJoystick;
    public UnityEvent<JoystickEvent> onRightJoystickFixed;
    public UnityEvent<JoystickEvent> onRightJoystick;

    private JoystickEvent leftJEvent;
    private JoystickEvent rightJEvent;

    [SerializeField] bool m_logInput = false;

    private void Update() {
        // Debug
        if (m_logInput)
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(kcode))
                    Debug.Log("KeyCode down: " + kcode);

        // Buttons
        HandleSwitchButton();
        HandleActionOneButton();

        // Joysticks
        HandleLeftJoystick();
        HandleRightJoystick();

        if (rightJEvent.magnitude > 0) onRightJoystick.Invoke(rightJEvent);  
    }

    private void FixedUpdate() {
        if (leftJEvent.magnitude > 0) onLeftJoystick.Invoke(leftJEvent);
        if (rightJEvent.magnitude > 0) onRightJoystickFixed.Invoke(rightJEvent);   
    }

    private void HandleLeftJoystick() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        leftJEvent = new JoystickEvent(h,v);
    }

    private void HandleRightJoystick() {
        float h = Input.GetAxis("Horizontal 2");
        float v = Input.GetAxis("Vertical 2");
        rightJEvent = new JoystickEvent(h,v);
    }

    private void HandleSwitchButton() {
        if (Input.GetKeyDown("joystick 1 button 5")) onSwitch.Invoke();
    }
    
    private void HandleActionOneButton() {
        if (Input.GetKeyDown("joystick 1 button 1")) onActionOne.Invoke();
    }
}
