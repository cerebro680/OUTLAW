using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using System;
//The following struct lets job iterate across multiple items and also moves those items using transforms
public struct TrafficJob : IJobParallelForTransform
{

    public float speed ;
    public float deltatime;
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 seekForce;
    public float mass;



    //Job Definition of the traffic cars

    public void Execute(int index, TransformAccess transform)
    {


        position = transform.position;
     
        mass = 1.0f;
        //Add Acceleration to Velocity * Time

        velocity += acceleration * deltatime;

        // Add vel to position * Time

        position += velocity * deltatime;

        

        acceleration = new Vector3 (10.0f, 0, 0);

        //Calculate direction (to know where we are facing)

        direction = velocity.normalized;

        direction.y = 0;



        // setting the transform to the new position



        transform.position = new Vector3( transform.position.x , 1.12f, transform.position.z);
        //Face the direction of the new position

        Quaternion rot = Quaternion.LookRotation(direction);

        transform.rotation = rot;
        transform.position = position;



    }

}