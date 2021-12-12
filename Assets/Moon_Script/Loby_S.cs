using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Loby_S : MonoBehaviour {

	[SerializeField]
	public Sprite[] Setting_images;
	public Image Background_image;
	public Image Background_image_Box;
	public Image Explain_images;
	public Slider backVolume;
	public Slider backVolume2;
	public AudioSource audio_;
	public AudioSource audio_2;
	private float backVol = 1f;
	private float backVol2 = 1f;
	int spring_num, summer_sum, fall_num, winter_num;
	int explain_page;
	bool spring_b, summer_b, fall_b, winter_b;
	private IEnumerator box_cor;
	AudioSource click_audio;
	AudioSource page_audio;
	void Start () {

		click_audio = GameObject.Find("Audio_Setting").GetComponent<AudioSource>();
		page_audio = GameObject.Find("Audio_page").GetComponent<AudioSource>();
		backVol = PlayerPrefs.GetFloat("backvol", 1f);
		backVolume.value = backVol;
		audio_.volume = backVolume.value;

		backVol2 = PlayerPrefs.GetFloat("backvol2", 2f);
		backVolume2.value = backVol2;
		audio_2.volume = backVolume2.value;

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
		Loaby_Message();
	}
	
	void Update () {
		SoundSlider();
		SoundSlider2();
	}
	public void SoundSlider()
    {
		audio_.volume = backVolume.value;
		backVol = backVolume.value;
		PlayerPrefs.SetFloat("backvol", backVol);
    }
	public void SoundSlider2()
	{
		audio_2.volume = backVolume2.value;
		backVol2 = backVolume2.value;
		PlayerPrefs.SetFloat("backvol2", backVol2);
	}

	public void Loaby_Message()
    {
		
		GameObject.Find("Background").GetComponent<Image>().color =new Color (80f/255f, 80f/255f, 80f/255f);
		for(int i=0; i<=7; i++)
        {
			GameObject.Find("Canvas2").transform.GetChild(i).GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		}
		GameObject.Find("Loby_explain").transform.GetChild(0).gameObject.SetActive(true);
		GameObject.Find("Explain").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
	}

	public void OK_1()
    {
		click_audio.Play();
		GameObject.Find("Explain").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Loby_explain").transform.GetChild(0).gameObject.SetActive(false);

		GameObject.Find("Loby_explain").transform.GetChild(1).gameObject.SetActive(true);
		GameObject.Find("Start").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
	}
	public void OK_2()
	{
		click_audio.Play();
		GameObject.Find("Start").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Loby_explain").transform.GetChild(1).gameObject.SetActive(false);

		GameObject.Find("Loby_explain").transform.GetChild(2).gameObject.SetActive(true);

		GameObject.Find("Spring").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		GameObject.Find("Summer").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		GameObject.Find("Fall").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		GameObject.Find("Winter").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
	}
	public void OK_3()
	{
		click_audio.Play();
		GameObject.Find("Spring").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Summer").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Fall").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Winter").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Loby_explain").transform.GetChild(2).gameObject.SetActive(false);

		GameObject.Find("Loby_explain").transform.GetChild(3).gameObject.SetActive(true);
		GameObject.Find("Exit").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
	}
	public void OK_4()
	{
		click_audio.Play();
		GameObject.Find("Exit").GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
		GameObject.Find("Loby_explain").transform.GetChild(3).gameObject.SetActive(false);

		GameObject.Find("Setting").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		GameObject.Find("Loby_explain").transform.GetChild(4).gameObject.SetActive(true);

		
	}
	public void OK_5()
	{
		click_audio.Play();
		GameObject.Find("Loby_explain").transform.GetChild(4).gameObject.SetActive(false);
		GameObject.Find("Background").GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		for (int i = 0; i < 7; i++)
		{
			GameObject.Find("Canvas2").transform.GetChild(i).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
		}
	}


	void Child_Layer_Chage(Transform parent, int layer_number) //자식 오브젝트의 모든 layer 변경
	{
		parent.gameObject.layer = layer_number;
		foreach (Transform child in parent)
		{
			Child_Layer_Chage(child, layer_number);
		}
	}

	// 배경 바꾸기
	public void Spring_Change()
    {
		click_audio.Play();
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
		click_audio.Play();
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
		click_audio.Play();
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
		click_audio.Play();
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

			if (Background_image_Box.gameObject.GetComponent<RectTransform>().localScale.x >= 0.29f)
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
		click_audio.Play();
		GameObject.Find("Canvas3").transform.GetChild(0).gameObject.SetActive(true);
		Explain_images.sprite = Setting_images[20];
		
    }
	public void Explain_Off()
	{
		click_audio.Play();
		GameObject.Find("Canvas3").transform.GetChild(0).gameObject.SetActive(false);
	}
	public void Explain_next()
	{
		page_audio.Play();
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
		page_audio.Play();
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
		click_audio.Play();
		GameObject.Find("Canvas3").transform.GetChild(1).gameObject.SetActive(true);
	}

	public void Game_Exit_On()
	{
		click_audio.Play();
		Application.Quit(); //다시 확인해야함---------------------------------------------------------------------------------
	}
	public void Game_Exit_Off()
	{
		click_audio.Play();
		GameObject.Find("Canvas3").transform.GetChild(1).gameObject.SetActive(false);
	}
	public void setting_On()
	{
		GameObject.Find("Loby_setting").transform.GetChild(0).gameObject.SetActive(true);
	}
	public void setting_X()
    {
		GameObject.Find("Loby_setting").transform.GetChild(0).gameObject.SetActive(false);
	}
	public void Start_to_Main()
    {
		SceneManager.LoadScene("Main");
	}

}
