using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

public class Planet : MonoBehaviour
{
    [HideInInspector] public Vector3 velocity;              // Velocity of the particle
    [HideInInspector] public Vector3 acceleration;          // Acceleration of the particle
    [HideInInspector] public Vector3 accumulatedForces;     // Forces applied to the particle
    [HideInInspector] public float sunMass;
    [HideInInspector] public static float gravConstant = 200f;
    [HideInInspector] public static float eccScaler = 1f;   //

    public GameObject target;                               //transform of the center sphere
    public float mass;                                      //not used maybe needed
    public float planetRadius;
    public float semiMajorAxis;                              //the value of the semiMajorAxis of the 
    public float eccentricity;                               // 0 = circle 1 = straight line
    public LineRenderer lineRenderer;
    [HideInInspector] public int positionCount = 0;

    void Start()
    {
        //set values if not sun
        if (!transform.CompareTag("sun"))
        {
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
            accumulatedForces = Vector3.zero;
            sunMass = target.GetComponent<Planet>().mass;

            lineRenderer.positionCount = 0;
            lineRenderer.useWorldSpace = true;
        }
    }

    public void FixedUpdate()
    {
        //constantly update with integrator if not sun
        if (!transform.CompareTag("sun"))
        {
            DoFixedUpdate(Time.fixedDeltaTime);
        }
    }

    public void DoFixedUpdate(float dt)
    {
        if (target != null)
        {
            Integrator.Integrate(this, dt);
            ClearForces();
        }
    }
    public void ClearForces()
    {
        accumulatedForces = Vector3.zero;
    }

    /* This logic was moved into the Integrator
    * public void UpdateOrbit()
    {
        // Calculate the angle for the orbit based on time
        float currentAngle = Time.time * orbitSpeed;  // Use Time.time to make it continuous
        currentAngle %= 360f; // Ensure the angle stays within 0-360 degrees

        // Convert angle to radians
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calculate the semi-major and semi-minor axes
        float a = orbitRadius;                     // Semi-major axis
        float b = orbitRadius * (1 - eccentricity); // Semi-minor axis

        // Calculate the position on the ellipse
        float x = a * Mathf.Cos(radians);
        float y = b * Mathf.Sin(radians);
        Vector3 desiredPosition = new Vector3(target.position.x + x, target.position.y + y, particle.transform.position.z);

        // Calculate the current radius (distance from center to point on the ellipse)
        float r = Mathf.Sqrt(Mathf.Pow(a * Mathf.Cos(radians), 2) + Mathf.Pow(b * Mathf.Sin(radians), 2));

        // Calculate velocity magnitude based on Kepler's laws (velocity is inversely proportional to the square root of distance)
        float velocityMagnitude = orbitSpeed * Mathf.Sqrt((1 + eccentricity) / r);

        // Calculate the direction of motion (tangent to the ellipse)
        Vector3 tangent = new Vector3(-Mathf.Sin(radians), Mathf.Cos(radians), 0).normalized;

        // Set velocity as tangent to the elliptical path, scaled by the velocity magnitude
        particle.velocity = tangent * velocityMagnitude;

        // Update the particle's position
        particle.transform.position = desiredPosition;
    }*/
}
