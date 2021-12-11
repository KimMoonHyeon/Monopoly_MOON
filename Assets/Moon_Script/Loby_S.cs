using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Loby_S : MonoBehaviour {

	[SerializeField]
	public Sprite[] Setting_images;
	public Image Background_image;
	public Image Background_image_Box;
	public Image Explain_images;
	int spring_num, summer_sum, fall_num, winter_num;
	int explain_page;
	bool spring_b, summer_b, fall_b, winter_b;
	private IEnumerator box_cor;

	void Start () {
		box_cor = Box_cor();

		explain_page = 0;
		spring_num = 0;
		summer_sum = 5;
		fall_num = 10; 
		winter_num = 15;
		spring_b = false;
		summer_b = false;
		fall_b = false;
		winter_b = false;
	}
	
	void Update () {
		
	}

	// 배경 바꾸기
	public void Spring_Change()
    {
		if (!spring_b)
		{
			Background_image_Box.transform.localScale = new Vector3(0f, 0f, 0f);
			StartCoroutine(box_cor);
			spring_b = true;
		}
		Background_image_Box.sprite = Setting_images[35];
		Background_image.sprite = Setting_images[spring_num];
		spring_num++;
		
        if (spring_num >= 5)
        {
			spring_num -= 5;
		}
	}
	public void Summer_Change()
	{
		if (!summer_b)
		{
			Background_image_Box.transform.localScale = new Vector3(0f, 0f, 0f);
			StartCoroutine(box_cor);
			summer_b = true;
		}
		Background_image_Box.sprite = Setting_images[36];
		Background_image.sprite = Setting_images[summer_sum];
		summer_sum++;
		if (summer_sum >= 10)
		{
			summer_sum -= 5;
		}
	}
	public void Fall_Change()
	{
		if (!fall_b)
		{
			Background_image_Box.transform.localScale = new Vector3(0f, 0f, 0f);
			StartCoroutine(box_cor);
			fall_b = true;
		}
		Background_image_Box.sprite = Setting_images[37];
		Background_image.sprite = Setting_images[fall_num];
		fall_num++;
		if (fall_num >= 15)
		{
			fall_num -= 5;
		}
	}
	public void Winter_Change()
	{
		if (!winter_b)
		{
			Background_image_Box.transform.localScale = new Vector3(0f, 0f, 0f);
			StartCoroutine(box_cor);
			winter_b = true;
		}
		Background_image_Box.sprite = Setting_images[38];
		Background_image.sprite = Setting_images[winter_num];
		winter_num++;
		if (winter_num >= 20)
		{
			winter_num -= 5;
		}
	}

	IEnumerator Box_cor()
	{
		while (true)
		{
			Background_image_Box.transform.localScale += new Vector3(0.04f, 0.06f, 0.0f);
			Debug.Log(Background_image_Box.gameObject.GetComponent<RectTransform>().localScale.x);

			if (Background_image_Box.gameObject.GetComponent<RectTransform>().localScale.x >= 0.39f)
			{
				StopCoroutine(box_cor);
				Background_image.gameObject.SetActive(true);
			}

			yield return new WaitForSecondsRealtime(0.05f);

		}

	}

	//게임 설명
	public void Explain_On()
    {
		GameObject.Find("Canvas3").transform.GetChild(0).gameObject.SetActive(true);
		Explain_images.sprite = Setting_images[20];
		
    }
	public void Explain_Off()
	{
		GameObject.Find("Canvas3").transform.GetChild(0).gameObject.SetActive(false);
	}
	public void Explain_next()
	{
	
		explain_page++;
		Debug.Log(explain_page);
		Explain_images.sprite = Setting_images[20 + explain_page];
		if (explain_page >= 1)
		{
			GameObject.Find("Game_explain").transform.GetChild(2).gameObject.SetActive(true);
		}
		if (explain_page >= 10)
        {
			GameObject.Find("Game_explain").transform.GetChild(1).gameObject.SetActive(false);
        }
	}
	public void Explain_before()
	{
		if (explain_page <= 10)
		{
			GameObject.Find("Game_explain").transform.GetChild(1).gameObject.SetActive(true);
		}
		explain_page--;
		Debug.Log(explain_page);
		Explain_images.sprite = Setting_images[20 + explain_page];
		if (explain_page <= 0)
		{
			GameObject.Find("Game_explain").transform.GetChild(2).gameObject.SetActive(false);
		}
	}
	//게임종료
	public void Game_Exit()
	{
		GameObject.Find("Canvas3").transform.GetChild(1).gameObject.SetActive(true);
	}

	public void Game_Exit_On()
	{
        Application.Quit(); //다시 확인해야함---------------------------------------------------------------------------------
	}
	public void Game_Exit_Off()
	{
		GameObject.Find("Canvas3").transform.GetChild(1).gameObject.SetActive(false);
	}



}
