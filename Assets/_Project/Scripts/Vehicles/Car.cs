using UnityEngine;

public class Car : MonoBehaviour
{
    public CarInputState Input;
    public LayerMask GroundLayer;

    [Header("References")]
    public Rigidbody Rigidbody;
    public SphereCollider[] Wheels;
    public SphereCollider[] SteeringWheels;
    public Transform CG;

    [Header("Physics")]
    public float Acceleration = 10f;
    public float TurnForce = 10f;
    public float AirDrag = 5f;
    public float MaxSteerAngle = 15f;

    private bool[] _grounded;
    private bool _allWheelsGrounded;

    void Start()
    {
        Rigidbody.centerOfMass = CG.localPosition;
        _grounded = new bool[Wheels.Length];
    }

    void Update()
    {
        _allWheelsGrounded = true;
        for (var i = 0; i < Wheels.Length; i++)
        {
            var wheel = Wheels[i];
            _grounded[i] = Physics.CheckSphere(wheel.transform.position, wheel.radius * 1.2f, GroundLayer, QueryTriggerInteraction.Ignore);
            if (!_grounded[i])
            {
                _allWheelsGrounded = false;
            }
        }
    }

    void FixedUpdate()
    {
        var accPerWheel = Acceleration / (float)Wheels.Length;

        for (var i = 0; i < Wheels.Length; i++)
        {
            if (!_grounded[i]) continue;

            var point = Wheels[i].transform.position;
            var forward = Wheels[i].transform.forward;

            if (Input.Brake > 0f)
            {
                Rigidbody.AddForceAtPosition(
                    -forward * accPerWheel * Input.Brake * Rigidbody.mass,
                    point);
            }
            else
            {
                Rigidbody.AddForceAtPosition(
                    forward * accPerWheel * Input.Gas * Rigidbody.mass,
                    point);
            }
        }

        foreach (var steeringWheel in SteeringWheels)
        {
            steeringWheel.transform.localRotation = Quaternion.Euler(
                0f,
                MaxSteerAngle * Input.Steering,
                0f
            );
        }

        Rigidbody.AddForce(-Rigidbody.velocity * AirDrag * Rigidbody.mass);

        // Lateral force
        for (var i = 0; i < Wheels.Length; i++)
        {
            var wheel = Wheels[i];
            if (!_grounded[i]) continue;

            // TODO: handbrake

            var right = wheel.transform.right;
            var point = wheel.transform.position;
            var pointVelocity = Rigidbody.GetPointVelocity(point);
            var dot = Vector3.Dot(right, pointVelocity.normalized);
            var force = -right * dot * pointVelocity.sqrMagnitude * Rigidbody.mass;

            Rigidbody.AddForceAtPosition(
                force,
                point,
                ForceMode.Force);
        }
    }
}
