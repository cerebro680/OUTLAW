using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cops : MonoBehaviour
{

    // Use this for initialization
        public GameObject[] copcars;
        Vector3 seekForce;

        [SerializeField]

        private Vector3 position;

        [SerializeField]

        private Vector3 direction;

        [SerializeField]

        private Vector3 velocity;

        [SerializeField]

        private Vector3 acceleration;

        [SerializeField]

        private GameObject player;

        private float mass;

        private float maxSpeed;



        // Use this for initialization

        private void Start()
        {

            position = transform.position;

            mass = 1.0f;

            maxSpeed=20.0f;



            player = GameObject.Find("Player");

        }



        // Update is called once per frame

        private void Update()
        {
            copcars = GameObject.FindGameObjectsWithTag("seeker");

            UpdatePosition();

        }



        private void UpdatePosition()
        {

            position = transform.position;



         
               if(player!=null)
        {
                seekForce = Seek(player.transform.position);
        }
              
                seekForce += Separate(6);
                ApplyForce(seekForce);

            

           



            //Add Acceleration to Velocity * Time

            velocity += acceleration * Time.deltaTime;

            // Add vel to position * Time

            position += velocity * Time.deltaTime;

            //Reset Acceleration vector

            acceleration = Vector3.zero;

            //Calculate direction (to know where we are facing)

            direction = velocity.normalized;

            direction.y = 0;



            // setting the transform to the new position

            transform.position = position;

            transform.position= new Vector3( transform.position.x, 1.12f ,  transform.position.z);
            

            //Face the direction of the new position

            Quaternion rot = Quaternion.LookRotation(direction);
           
            transform.rotation = rot;
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

        }



        private void ApplyForce(Vector3 force)

        {

            acceleration += force / mass;

        }



        private Vector3 Seek(Vector3 targetPosition)

        {

            //Calculate the desired unclamped velocity

            //which is from this vehicle to target's position

            Vector3 desiredVelocity = targetPosition - position;



            //Calculate maximum speed

            //so the vehicle does not move faster than it should

            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);



            //Calculate steering force

            Vector3 steeringForce = desiredVelocity - velocity;



            //return the force so it can be applied to this vehicle

            return steeringForce;

        }
           //Prevents the cop cars from overlapping
        public Vector3 Separate ( float distance)
    {

        List<GameObject> separatefrom = new List<GameObject>();
        for( int i=0; i < copcars.Length; i++)
        {
            if (gameObject == copcars[i])
                continue;
            else
            {
                Vector3 check = copcars[i].transform.position - gameObject.transform.position;
                if (check.sqrMagnitude < distance * distance) 
                {
                    separatefrom.Add(copcars[i]);
                    
                }
            }
        }

        Vector3 SteeringForce = Vector3.zero;
        for( int i=0; i< separatefrom.Count; i++)
        {
            SteeringForce += Flee(separatefrom[i].transform.position);
        }
        return SteeringForce;

    }


    Vector3 Flee(Vector3 targetPosition)

    {

        //Calculate the desired unclamped velocity

        //which is from this vehicle to target's position

        Vector3 desiredVelocity = position - targetPosition;



        //Calculate maximum speed

        //so the vehicle does not move faster than it should

        desiredVelocity.Normalize();

        desiredVelocity *= maxSpeed;



        //Calculate steering force

        Vector3 steeringForce = desiredVelocity - velocity;



        //return the force so it can be applied to this vehicle

        return steeringForce;

    }



}
