/*using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;        // The object the sphere will orbit around
    //public float setOrbitSpeed;  // Speed of the orbit (degrees per second)
    public float setOrbitRadius;  // Radius of the orbit
    public float setEccentricity; // 0 = circle 1 = straight line
    public float setPlanetRadius;
    public float setMass;

    public TestParticle particle;     // The particle that will orbit
    public LineRenderer lineRenderer;
    private int positionCount = 0;

    void Start()
    {
        // Initialize the Particle3D for the orbiting sphere
        particle = new TestParticle(transform);

        particle.center = target;
        //particle.orbitSpeed = setOrbitSpeed;
        particle.orbitRadius = setOrbitRadius;
        particle.eccentricity = setEccentricity;
        particle.mass = setMass;
        particle.planetRadius = setPlanetRadius;

        //sun is at 0,0,0 therefore no matter eccentrcitiy,
        //orbitRadius = furthest point on ellipse
        particle.semiMajorAxis = setOrbitRadius;

        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
    }

    public void FixedUpdate()
    {
        positionCount++;
        lineRenderer.positionCount = positionCount;
        lineRenderer.SetPosition(positionCount - 1, transform.position);
        DoFixedUpdate(Time.fixedDeltaTime);
        //DrawFoci();
    }

    public void DoFixedUpdate(float dt)
    {
        if (target != null)
        {
            //Integrator.Integrate(particle, dt);
            ClearForces();
        }
    }
    public void ClearForces()
    {
        particle.accumulatedForces = Vector3.zero;
    }

   *//* This logic was moved into the Integrator
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

    /*public void DrawFoci()
    {
        // Calculate semi-major (a) and semi-minor (b) axes
        float a = orbitRadius;
        float b = orbitRadius * (1 - eccentricity);

        // Calculate the distance of the foci from the center
        float aSquare = a * a;
        float bSquare = b * b;
        float minus = aSquare - bSquare;
        float c = Mathf.Sqrt(minus);
        Debug.Log("A " + a);
        Debug.Log("B " + b);
        Debug.Log("A Square " + aSquare);
        Debug.Log("B Square " + bSquare); 
        Debug.Log("Minus " + minus);
        Debug.Log("C " + c);
        // Foci positions relative to the target position
        Vector3 focus1 = target.position + new Vector3(c, 0, 0);
        Vector3 focus2 = target.position - new Vector3(c, 0, 0);

        // Draw the foci as small spheres or markers
        Debug.DrawLine(focus1, focus1 + Vector3.up * 100f, Color.red, 0.1f); // Focus 1 marker
        Debug.DrawLine(focus2, focus2 + Vector3.up * 5f, Color.blue, 0.1f); // Focus 2 marker
    }*//*

}
*/