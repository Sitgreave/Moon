using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] [Range(0,10)] private float _movementSpeed;

    private void FixedUpdate()
    {
        _rigidbody.velocity = Direction();

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }


    private Vector3 Direction()
    {
        return new Vector3(
            x: _joystick.Horizontal * _movementSpeed,
            y: _rigidbody.velocity.y,
            z: _joystick.Vertical * _movementSpeed 
            );
    }
}
