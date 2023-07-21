using System.Collections.Generic;
using UnityEngine;

public class RigidbodyAccessor2D : MonoBehaviour, IRigidbodyAccessor
{
    private float _gravityScale;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public GameObject GameObject => gameObject;

    public float Mass
    {
        get => _rigidbody2D.mass;
        set => _rigidbody2D.mass = value;
    }

    public Vector3 Position
    {
        get => _rigidbody2D.position;
        set => _rigidbody2D.position = value;
    }

    public Quaternion Rotation
    {
        get => Quaternion.AngleAxis(_rigidbody2D.rotation, Vector3.forward);
        set => _rigidbody2D.rotation = value.eulerAngles.z;
    }

    public Vector3 Velocity
    {
        get => _rigidbody2D.velocity;
        set => _rigidbody2D.velocity = value;
    }

    public Vector3 AngularVelocity
    {
        get => Vector3.forward * _rigidbody2D.angularVelocity;
        set => _rigidbody2D.angularVelocity = value.z;
    }

    public float Drag
    {
        get => _rigidbody2D.drag;
        set => _rigidbody2D.drag = value;
    }

    public float AngularDrag
    {
        get => _rigidbody2D.angularDrag;
        set => _rigidbody2D.angularDrag = value;
    }

    public bool UseGravity
    {
        get => _rigidbody2D.gravityScale > Mathf.Epsilon;
        set
        {
            _gravityScale = UseGravity ? _rigidbody2D.gravityScale : _gravityScale;
            _rigidbody2D.gravityScale = value ? _gravityScale : 0.0f;
        }
    }

    public bool IsKinematic
    {
        get => _rigidbody2D.isKinematic;
        set => _rigidbody2D.isKinematic = value;
    }

    public RigidbodyConstraints Constraints
    {
        get => RigidbodyAcessorExtensions.ToRigidbodyConstraints3D(_rigidbody2D.constraints);
        set => _rigidbody2D.constraints = RigidbodyAcessorExtensions.ToRigidbodyContraints2D(value);
    }

    public void AddForce(Vector3 force)
    {
        _rigidbody2D.AddForce(force);
    }

    public void AddForceAtPosition(Vector3 force, Vector3 position)
    {
        _rigidbody2D.AddForceAtPosition(force, position);
    }

    public void AddRelativeForce(Vector3 force)
    {
        _rigidbody2D.AddRelativeForce(force);
    }

    public void AddTorque(Vector3 torque)
    {
        _rigidbody2D.AddTorque(torque.z);
    }

    public void MovePosition(Vector3 position)
    {
        _rigidbody2D.MovePosition(position);
    }

    public void MoveRotation(Quaternion rotation)
    {
        _rigidbody2D.MoveRotation(rotation.eulerAngles.z);
    }

    public int GetAttachedColliders(out IColliderAccessor[] attachedColliders)
    {
        return (attachedColliders = GetComponentsInChildren<IColliderAccessor>()).Length;
    }

    public int GetAttachedColliders(out List<IColliderAccessor> attachedColliders)
    {
        return (attachedColliders = new List<IColliderAccessor>(GetComponentsInChildren<IColliderAccessor>())).Count;
    }
}
