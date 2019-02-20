using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: MonoBehaviour { 
    public float speed = 1f;
    public float turn = 3f;
    public int money = 0;
    public int health = 0;
    public int key = 0;
    GameObject gamemanager;
    

    public GameObject healthpowerup;
    public GameObject moneypowerup;
    public GameObject keypowerup;
    public GameObject winner;
    public int spawnincrement;
    // Use this for initialization
    void Start () {
        //starts with an initial health of 300 HP
        health = 300;
	}


    private void OnCollisionEnter(Collision collision)
    {
        //Finish the game as soon as the player hits the colliders outside the gate
        if(collision.gameObject.tag == "LevelFinish")
        {
            
           SceneManager.LoadScene("LEVELCOMPLETED");

            
        }
        


        //Adds health/money/keys and destroys powerup objects after the player hits them

        if (collision.transform.tag == "money")   
        {
            money += 1000;

            Destroy(collision.gameObject);

            gamemanager.GetComponent<GameManager>().addpowerup(moneypowerup);

        //Adds more cops cars and traffic if you pick up more money
            gamemanager.GetComponent<GameManager>().Addcopcars(spawnincrement);
            gamemanager.GetComponent<GameManager>().Addtrafficcars(spawnincrement);

            
        }
        
        if (collision.transform.tag == "health")
        {
            health += 100;

            Destroy(collision.gameObject);
            gamemanager.GetComponent<GameManager>().addpowerup(healthpowerup);

        }

        if (collision.transform.tag == "key")
        {
            key += 1;

            Destroy(collision.gameObject);

            gamemanager.GetComponent<GameManager>().addpowerup(keypowerup);

        }




      if (collision.transform.tag=="seeker" || collision.transform.tag =="traffic" )
        {
            //decreases health upon collision
            health -= 1;
        }
    }



    // Update is called once per frame
    void Update () {


        gamemanager = GameObject.Find("GameManager");

        

        //Player Controls with Rotation

        if (Input.GetKey(KeyCode.W))
          
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turn * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turn * Time.deltaTime);



    }
}
