using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using System.Threading.Tasks;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;
using System.Threading;
using System.IO;



public class GameManager : MonoBehaviour
{


  
    GameObject player;
    GameObject exit;
    GameObject exit2;
    GameObject exit3;
    GameObject exit4;
    GameObject winner;
    public int copsspawncount;
    public int trafficspawncount;
    public float maxspeed;
    public bool exittext;
    //public bool exittext2=false;
    //public bool exittext3=false;
    //public bool exittext4=false;
    //public string exitDescription;
    public Vector3 seekForce1;
    public Vector3 position1;
    public Vector3 direction1;
    public Vector3 velocity1;
    public Vector3 acceleration1;
    public float mass1;
    public GameObject[] copcars;
    public GameObject[] trafficcars;
    public GameObject copcar;
    public GameObject trafficcar;
    public int trafficspawnincrement = 1;
    public GameObject[] trafficcarseparate;





    int key;
    int health;
    public int money;
    //List of all the cop cars to pass into the job
    TransformAccessArray transforms;
    TrafficJob traffic;
    JobHandle handle;

    int count;


    private void OnDisable()
    {
        handle.Complete();
        transforms.Dispose();
    }


    void Start()
    {
       
       copcars = new GameObject[copsspawncount];

       for (int i = 0; i < copsspawncount; i++)
        {

          copcars[i] = copcar;

        }

       trafficcars = new GameObject[copsspawncount];

       for (int i = 0; i < trafficspawncount; i++)
        {
            trafficcars[i] = trafficcar;
          }
        
        transforms = new TransformAccessArray(0, -1);
        Addtrafficcars(trafficspawncount);
        Addcopcars(copsspawncount);
         //reserved for future purpose in order to make the text appear every time 3 keys are picked up.
        //Invoke("Togglelabel", 5);
    }


    
    void Update()
    {
        //Adds traffic 
        handle.Complete();

        Addtrafficcars(trafficspawnincrement);
        

        

        player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<Player>().money;
        health = player.GetComponent<Player>().health;
        key = player.GetComponent<Player>().key;
        winner = player.GetComponent<Player>().winner;
        trafficcarseparate = GameObject.FindGameObjectsWithTag("traffic");
       


        if (key == 3)
        {
            exit = GameObject.FindGameObjectWithTag("Exit");

            Destroy(exit);
            exittext = true;
           
        }

        if (key == 6)
        {
            exit2 = GameObject.FindGameObjectWithTag("Exit1");

            Destroy(exit2);
          //  exittext2 = true;
            

        }

        if (key == 9)
        {
            exit3 = GameObject.FindGameObjectWithTag("Exit2");

            Destroy(exit3);
         //   exittext3 = true;
            

        }

        if (key == 12)
        {
            exit4 = GameObject.FindGameObjectWithTag("Exit3");

            Destroy(exit4);
            //exittext4 = true;
            

        }

        if (health == 0)
        {

            SceneManager.LoadScene("GAMEOVER");



        }

        





        traffic = new TrafficJob()
        {
            speed = maxspeed,
            deltatime = Time.deltaTime,
            seekForce = seekForce1,
            position = position1,
            direction = direction1,
            velocity = velocity1,
            acceleration = acceleration1,
            mass = mass1

        };
       
        handle = traffic.Schedule(transforms);
        //process the jobs
        JobHandle.ScheduleBatchedJobs();
        if (player != null)
        {
            traffic.seekForce = Seek(player.transform.position);
        }

        traffic.seekForce += Separate(7);
        ApplyForce(traffic.seekForce);
        traffic.position = transform.position;
    }
    //Add cops cars randomly
    public void Addcopcars(int amount)
    {
       
        //Parallel.For(0, amount, i =>
        for (int i = 0; i < amount; i++)
        {
            float xvalue = Random.Range(-130f, 130f);
            float zvalue = Random.Range(-130f, 130f);
            Vector3 position = new Vector3(xvalue, 2.0f, zvalue + 10f);
            Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

            GameObject cops = Instantiate(copcars[i], position, rotation) as GameObject;

          


        }
        count += amount;
    }
    //Add traffic  cars randomly
    public void Addtrafficcars(int amount)
    {
        handle.Complete();
        transforms.capacity = transforms.length + amount;
        //Parallel.For(0, amount, i =>
        for (int i = 0; i < amount; i++)
        {
            float xvalue = Random.Range(-130f, 130f);
            float zvalue = Random.Range(-130f, 130f);
            Vector3 position = new Vector3(xvalue, 2.0f, zvalue + 10f);
            Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

           GameObject traff = Instantiate(trafficcars[i], position, rotation) as GameObject;

            transforms.Add(traff.transform);


        }
        count += amount;
    }
    //randomly spawns powerups on the map
    public void addpowerup(GameObject power)
    {

        float xvalue = Random.Range(-100f, 100f);
        float zvalue = Random.Range(-100f, 100f);
        Vector3 position = new Vector3(xvalue, 2.0f, zvalue + 10f);
        Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

        GameObject powerups = Instantiate(power, position, rotation) as GameObject;


    }


    //Displays on screen, the numbers of keys, health and money. Also displays a message when the gate is open.

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.skin.box.fontSize = 24;
        GUI.skin.label.fontSize = 24;



        GUI.Box(new Rect(0, 0, 150, 100), "Keys: " + key + "\n" + "Health: " + health + "\n" + "Money: " + money);

        string line;
        using (StreamReader reader = new StreamReader("Exit.txt"))
        {

            line = reader.ReadLine();

        }



        if (exittext == true)
        {

            if (Time.deltaTime < 5)

                GUI.Label(new Rect(0, 440, 300, 300), line);
        }
        //reserved for future purposed so that the text appears every time a gate is open.

        /*  if (exittext2==true)
          {
              if (Time.deltaTime < 30)


                  GUI.Label(new Rect(0, 480, 300, 300), line);
          }

          if (exittext3==true)
          {
              if (Time.deltaTime < 30)


                  GUI.Label(new Rect(0, 480, 300, 300), line);
          }

          if (exittext4==true)
          {
              if (Time.deltaTime < 30)


                  GUI.Label(new Rect(0, 480, 300, 300), line);
          }

      } 

      public void Togglelabel()
      {
          exittext = !exittext;
          exittext2 = !exittext2;
          exittext3 = !exittext3;
          exittext4 = !exittext4;
      }  */
    }
    private void ApplyForce(Vector3 force)

    {

        traffic.acceleration += force / traffic.mass;

    }



    private Vector3 Seek(Vector3 targetPosition)

    {

        //Calculate the desired unclamped velocity

        //which is from this vehicle to target's position

        Vector3 desiredVelocity = targetPosition - traffic.position;



        //Calculate maximum speed

        //so the vehicle does not move faster than it should

        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, traffic.speed);



        //Calculate steering force

        Vector3 steeringForce = desiredVelocity - traffic.velocity;



        //return the force so it can be applied to this vehicle

        return steeringForce;

    }
    //Prevents the cop cars from overlapping
    public Vector3 Separate(float distance)
    {

        List<GameObject> separatefrom = new List<GameObject>();
        for (int i = 0; i < trafficcarseparate.Length; i++)
        {

            if (gameObject == trafficcarseparate[i])
                continue;
            else
            {
                Vector3 check = trafficcarseparate[i].transform.position - gameObject.transform.position;
                if (check.sqrMagnitude < distance * distance)
                {
                    separatefrom.Add(trafficcarseparate[i]);

                }
            }
        }

        Vector3 SteeringForce = Vector3.zero;
        for (int i = 0; i < separatefrom.Count; i++)
        {
            SteeringForce += Flee(separatefrom[i].transform.position);
        }
        return SteeringForce;

    }


    Vector3 Flee(Vector3 targetPosition)

    {

        //Calculate the desired unclamped velocity

        //which is from this vehicle to target's position

        Vector3 desiredVelocity = traffic.position - targetPosition;



        //Calculate maximum speed

        //so the vehicle does not move faster than it should

        desiredVelocity.Normalize();

        desiredVelocity *= traffic.speed;



        //Calculate steering force

        Vector3 steeringForce = desiredVelocity - traffic.velocity;



        //return the force so it can be applied to this vehicle

        return steeringForce;

    }



}





