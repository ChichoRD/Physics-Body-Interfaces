using UnityEngine;

public static class RigidbodyAcessorExtensions
{
    public static int EnableRigidbodyCollisions(this IRigidbodyAccessor rigidbodyAccessor)
    {
        rigidbodyAccessor.GetAttachedColliders(out IColliderAccessor[] attachedColliders);
        foreach (IColliderAccessor attachedCollider in attachedColliders)
            attachedCollider.Enabled = true;
        return attachedColliders.Length;
    }

    public static int DisableRigidbodyCollisions(this IRigidbodyAccessor rigidbodyAccessor)
    {
        rigidbodyAccessor.GetAttachedColliders(out IColliderAccessor[] attachedColliders);
        foreach (IColliderAccessor attachedCollider in attachedColliders)
            attachedCollider.Enabled = false;
        return attachedColliders.Length;
    }

    // m * dv = F * dt
    // a = F / m
    // F = m * a
    public static void AddAcceleration(this IRigidbodyAccessor rigidbodyAccessor, Vector3 acceleration)
    {
        rigidbodyAccessor.AddForce(acceleration * rigidbodyAccessor.Mass);
    }

    // m * dv = F * dt
    // I = F * dt
    // F = I / dt
    public static void AddImpulse(this IRigidbodyAccessor rigidbodyAccessor, Vector3 impulse)
    {
        rigidbodyAccessor.AddForce(impulse / Time.fixedDeltaTime);
    }

    // m * dv = F * dt
    // dv = F * dt / m
    // F = m * dv / dt
    public static void AddVelocity(this IRigidbodyAccessor rigidbodyAccessor, Vector3 velocity)
    {
        rigidbodyAccessor.AddForce(rigidbodyAccessor.Mass * velocity / Time.fixedDeltaTime);
    }

    public static RigidbodyConstraints ToRigidbodyConstraints3D(RigidbodyConstraints2D constraints)
    {
        RigidbodyConstraints rigidbodyConstraints = RigidbodyConstraints.None;

        if ((constraints & RigidbodyConstraints2D.FreezePositionX) != 0)
            rigidbodyConstraints |= RigidbodyConstraints.FreezePositionX;
        if ((constraints & RigidbodyConstraints2D.FreezePositionY) != 0)
            rigidbodyConstraints |= RigidbodyConstraints.FreezePositionY;
        rigidbodyConstraints |= RigidbodyConstraints.FreezePositionZ;

        rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationX;
        rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationY;
        if ((constraints & RigidbodyConstraints2D.FreezeRotation) != 0)
            rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationZ;

        return rigidbodyConstraints;
    }

    public static RigidbodyConstraints2D ToRigidbodyContraints2D(RigidbodyConstraints constraints)
    {
        RigidbodyConstraints2D rigidbodyConstraints = RigidbodyConstraints2D.None;

        if ((constraints & RigidbodyConstraints.FreezePositionX) != 0)
            rigidbodyConstraints |= RigidbodyConstraints2D.FreezePositionX;
        if ((constraints & RigidbodyConstraints.FreezePositionY) != 0)
            rigidbodyConstraints |= RigidbodyConstraints2D.FreezePositionY;

        if ((constraints & RigidbodyConstraints.FreezeRotationZ) != 0)
            rigidbodyConstraints |= RigidbodyConstraints2D.FreezeRotation;

        return rigidbodyConstraints;
    }
}