using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigid;
    private CapsuleCollider _col;
    private Vector3 _targetVector;

    public Transform ModelTransform;
    public float RotationSpeed = 8;
    [Header("Movement Settings")]
    public float Acceleration;
    public float Speed;
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // collect inputs
        _targetVector.x = Input.GetAxis("Horizontal");
        _targetVector.z = Input.GetAxis("Vertical");

        // point model in direction of velocity
        // sqrMagnitude is a less expensive calculation than magnitude
        if (_rigid.velocity.sqrMagnitude > 0.1f) ModelTransform.rotation = Quaternion.Lerp(ModelTransform.rotation, Quaternion.LookRotation(_rigid.velocity), RotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Lerps the velocity to the clamped target, prevents the player from moving faster diagonally
        _rigid.velocity = Vector3.Lerp(_rigid.velocity, Vector3.ClampMagnitude(_targetVector, 1) * Speed, Acceleration * Time.fixedDeltaTime);
    }
}
