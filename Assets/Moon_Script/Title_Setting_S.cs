using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_Setting_S : MonoBehaviour {
	public AudioSource title_song;
	GameObject c;
	bool k;
	void Start () {
		c = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
		k = true;
	}
	
	void Update () {
		if (c.transform.localScale.x <= 2) {
			c.transform.localScale += new Vector3(1.1f, 1.1f, 0) * Time.deltaTime;
		}
		else if(c.transform.localScale.x >= 2)
        {
			c.gameObject.SetActive(false);
			GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        }
		
		if (GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.localScale.x >= 2)
		{
			GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
			GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);

			
			if (k)
			{
				title_song.Play();
				StartCoroutine(Delay());
				k = false;
			}
			if (Input.GetMouseButtonDown(0))
            {
				SceneManager.LoadScene("Lobby");
			}
		}
		else if (GameObject.Find("Canvas").transform.GetChild(1).gameObject.activeSelf == true)
		{
			GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.localScale += new Vector3(1.1f, 1.1f, 0) * Time.deltaTime;
			
		}

	}

	IEnumerator Delay()
	{
		int count = 0;
		while (count < 30)
		{
			GameObject.Find("Image3").transform.GetChild(0).gameObject.SetActive(true);
			yield return new WaitForSecondsRealtime(1f);
			GameObject.Find("Image3").transform.GetChild(0).gameObject.SetActive(false);
			yield return new WaitForSecondsRealtime(1f);
			count++;
		}
	}


	



}
