using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    
    // Use this for initialization
    private void Start()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Traffic cars gets destroyed as they hit the wall in their direction.
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Exit2")
        {

            Destroy(this.gameObject);


        }
    }

    }