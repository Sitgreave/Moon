using DG.Tweening;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] [Range(0,10)] public float MovementSpeed;


    private void FixedUpdate()
    {
        _rigidbody.velocity = Direction();

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(_rigidbody.velocity);
            Vector3 newRotation = rotation.eulerAngles;
            _rigidbody.DORotate(newRotation, .2f);
        }
    }
    private Vector3 Direction()
    {
        return new Vector3(
            x: _joystick.Horizontal * MovementSpeed,
            y: _rigidbody.velocity.y,
            z: _joystick.Vertical * MovementSpeed
            );
    }
}
