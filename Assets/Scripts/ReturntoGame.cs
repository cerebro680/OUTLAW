using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturntoGame : MonoBehaviour {

    //GameManager managermenu;
    //Use this for initialization

   // public int highscore;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Return of the main game scene
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("OUTLAW");
        }
		
	}

   /* public void OnGUI()
    {
        GUI.color = Color.red;
        GUI.skin.box.fontSize = 24;
        highscore = managermenu.GetComponent<GameManager>().money;

        GUI.Box(new Rect(0, 0, 150, 100), "High Score:" + highscore);


    } */
}
