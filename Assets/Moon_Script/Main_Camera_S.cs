using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_S : MonoBehaviour {

	Vector3 initial_pos;
	public float time;
	public float power;
	public bool moving;
	GameObject Player;
	GameObject Moving_Camera; //Camera at the Player if) player move -> change camera
	void Start () {

		//this.transform.position = new Vector3(36.6f, 52.6f, -34.1f);
		//this.transform.rotation = new Vector3(39.86f, -45f, 0f);
		time = 3f;
		initial_pos = this.transform.position;
		Player = GameObject.Find("Player_1");
	}
	
	void Update () {
		Camera_Shake();
		Moving_Camera_F();
  
	}

	void Moving_Camera_F()
    {
		if (moving == true)
		{
			GameObject.Find("Main_Camera").transform.GetChild(0).gameObject.SetActive(false);
			GameObject.Find("Player_1").transform.GetChild(0).gameObject.SetActive(true);
		}
		else if (moving ==false)
        {
			GameObject.Find("Main_Camera").transform.GetChild(0).gameObject.SetActive(true);
			GameObject.Find("Player_1").transform.GetChild(0).gameObject.SetActive(false);
		}

    }

	void Camera_Shake()
    {
        if (time > 0f)
        {
			this.transform.position = Random.insideUnitSphere * power + initial_pos;
			time -= Time.deltaTime;
        }
        else
        {
			time = 0f;
			this.transform.position = initial_pos;
        }
    }

}
