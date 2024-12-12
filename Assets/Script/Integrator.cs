using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
//using static UnityEngine.GraphicsBuffer;
//using static UnityEngine.ParticleSystem;

public static class Integrator
{
    public static void Integrate(Planet particle, float dt)
    {
        //keplers 3rd law -> T = 2π * sqrt(a^3 / (G * M))
        //the further the planet from the sn, the slower its orbitalperiod
        float gravityConstant = Planet.gravConstant;    // gravity constant
        float a = particle.semiMajorAxis;               // semi-major axis
        float orbitalPeriod = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(a, 3) / (gravityConstant * particle.sunMass));

        //calculate angular velocity -> how fast planet moves on its orbit
        float angularVelocity = 2 * Mathf.PI / orbitalPeriod;

        //caclulate current angle -> velocity * current time for constantly updating system
        float currentAngle = angularVelocity * Time.time;
        //make sure angle stays within 360 degrees (2pi)
        currentAngle %= 2 * Mathf.PI;

        float scaledEccen;
        //load scaling value to calculate semi-minor axis
        if (particle.eccentricity * Planet.eccScaler == 0)
        {
            scaledEccen = particle.eccentricity;
            Debug.Log("eccenctricity too small to scale");
        }
        else
        {
            scaledEccen = particle.eccentricity * Planet.eccScaler;
        }
        float b = a * (1 - scaledEccen);

        //calc position on the ellipse
        //a = semi-major axis | b = semi-minor axis
        //Keplers 1st law -> keeping planets on elliptical orbits around the sun
        float x = a * Mathf.Cos(currentAngle);
        float y = b * Mathf.Sin(currentAngle);
        Vector3 desiredPosition = 
        new Vector3(
            particle.target.transform.position.x + x,
            particle.target.transform.position.y + y,
            particle.transform.position.z
        );

        //calculate current radius from sun
        float r = Mathf.Sqrt(Mathf.Pow(a * Mathf.Cos(currentAngle), 2) + Mathf.Pow(b * Mathf.Sin(currentAngle), 2));

        //calc velocity magnitude using vis-viva equation
        //v = sqrt(G * M * (2/r - 1/a))
        //Keplers 2nd law -> varying velocity on the ellipse 
        //  = faster when closer to sun, slower when further YET same area convered
        float velocityMagnitude = Mathf.Sqrt(gravityConstant * particle.sunMass * (2 / r - 1 / a));

        //calculate tangent vector to determine current velocity direction
        Vector3 tangent = new Vector3(-Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0).normalized;

        //update particle velocity according to magnitude and direction
        particle.velocity = tangent * velocityMagnitude;

        //update particle position according to desired
        particle.transform.position = desiredPosition;

        particle.positionCount++;
        particle.lineRenderer.positionCount = particle.positionCount;
        particle.lineRenderer.SetPosition(particle.positionCount - 1, particle.transform.position);
    }


    /* Old Integrate function
    public static void Integrate(TestParticle particle, float dt)
    {
        particle.transform.position += particle.velocity * dt;
        particle.acceleration = particle.accumulatedForces * particle.inverseMass;
        particle.velocity += particle.acceleration * dt;
    }*/
}
