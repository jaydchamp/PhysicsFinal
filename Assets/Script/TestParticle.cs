/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle
{
    public Transform transform; // Reference to the particle's transform
    public Transform center;    //transform of the center sphere
    public Vector3 velocity;   // Velocity of the particle
    public Vector3 acceleration;  // Acceleration of the particle
    public Vector3 accumulatedForces; // Forces applied to the particle
    //public float inverseMass;  // Inverse of the particle's mass (0 if mass is infinite
    public float mass; //not used maybe needed
    public float planetRadius;
    public float gravConstant = 200f;
    public float tempSunMass = 47256000;
    public float semiMajorAxis; //the value of the semiMajorAxis of the 
    //public float orbitSpeed;// = 30f;  // Speed of the orbit (degrees per second)
    public float orbitRadius;// = 5f;  // Radius of the orbit
    public float eccentricity;// = 0f; // 0 = circle 1 = straight line

    public TestParticle(Transform transform)
    {
        this.transform = transform;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        accumulatedForces = Vector3.zero;
    }
}
*/