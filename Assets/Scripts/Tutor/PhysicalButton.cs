using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    
    private bool _isPressed;
    private Vector3 _initialPosition;
    private ConfigurableJoint _joint;
        
    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1) Pressed();
        if(_isPressed && GetValue() - threshold <= 0) Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_initialPosition, transform.localPosition) / _joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone) value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }
    
    [ContextMenu("Pressed")]
    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    [ContextMenu("Released")]
    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
