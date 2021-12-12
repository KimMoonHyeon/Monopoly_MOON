using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Money_s : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player1")
		{
			Debug.Log("와따~!");
			GameObject.Find("UI_Setting").GetComponent<UI_Setting_S>().Start_UI();
		}
	}
}
