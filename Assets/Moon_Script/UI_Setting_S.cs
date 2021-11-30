using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//UI 스크립트를 따로 만들어서 GameObject.Find("UI_Setting").GetComponent<UI_Setting_S>().Buy_UI();로 꺼내쓸 수 있게 

public class UI_Setting_S : MonoBehaviour
{

	int stop_land_number;

	float state_time;
	bool state;
	public bool HighPassPoint;
	public bool HighPassOK;
	public bool IncreasePoint;
	public bool IncreaseOK;
	public int Golden_Key_number;
	GameObject UI_Ob;
	GameObject Player_1;
	[SerializeField]
	private Sprite[] Buy_images;
	public int[] land_money;
	public Image Buy_Image;
	public Image Festival_Image;
	public Image Golend_Key_Image;
	public Text Buy_text;
	public Text Player_money;
	public int buy_sum;
	public int my_money;

	void Start()
	{
		my_money = 3000000;
		land_money = new int[3] { 1, 2, 3 };

		UI_Ob = GameObject.Find("UI");
		Player_1 = GameObject.Find("Player_1");

		buy_sum = 0;
		state_time = 0f;

		HighPassPoint = false;
		IncreasePoint = false;
	}

	void Update()
	{
		if (state) { 
			State();
		}
	}

	//땅에 도착했을 때 UI 등장까지 잠시 state
	void State()
    {
		state_time += Time.deltaTime;
		//Debug.Log("시간:" + state_time);
    }

	//구매 UI
	public void Normal_Land_Buy_UI()
	{
		//state_time = 0f;
		state = true;
		if (state_time > 1f)
		{
			Debug.Log("In_Buy");
			stop_land_number = Player_1.GetComponent<Player_S>().stop_land_number;
			Buy_Image.sprite = Buy_images[stop_land_number];
			UI_Ob.transform.GetChild(0).gameObject.SetActive(true);
			state = false;
		}

	}

	//구매UI X버튼 눌렀을 때
	public void Normal_Buy_Button_X()
    {
		state = false;
		state_time = 0f;
		//집 버튼 눌렀을 때 체크 버튼 false로, 인수할 때도 마찬가지로
		GameObject.Find("house_1").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_2").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_3").transform.GetChild(0).gameObject.SetActive(false);

		//Buy_Image false
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

	}

	public void Festival_Buy_Button_X()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(1).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

	}
	public void Accident_Button_X()
	{
		for (int i = 0; i < 3; i++)
		{
			Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 0);

		}

		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(3).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

		//이제 상대 턴으로 넘겨주는 함수?

	}
	public void HighPass_Button()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(5).gameObject.SetActive(false);
		HighPassPoint = true;

	}
	public void Increase_Button()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(4).gameObject.SetActive(false);
		IncreasePoint = true;

	}
	public void Take_Button_Yes()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(7).gameObject.SetActive(false);

	}
	public void Take_Button_No()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(7).gameObject.SetActive(false);

	}
	//구매 UI에서 집 클리깃 체크표시
	public void Button_house()
    {
		Debug.Log("?");
		if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.activeSelf == false)
		{
			Debug.Log("!");
			EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(true);
			if(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_1")
            {
				buy_sum += land_money[0];
				Debug.Log(buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_2")
			{
				buy_sum += land_money[1];
				Debug.Log(buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_3")
			{
				buy_sum += land_money[2];
				Debug.Log(buy_sum);
			}
		}
		else if(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.activeSelf == true)
		{
			EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(false);
			if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_1")
			{
				buy_sum -= land_money[0];
				Debug.Log(-buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_2")
			{
				buy_sum -= land_money[1];
				Debug.Log(-buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_3")
			{
				buy_sum -= land_money[2];
				Debug.Log(-buy_sum);
			}
		}

		
		Buy_text.GetComponent<Text>().text = buy_sum.ToString();
		

	}

	public void Buy_Butoon()
    {
		state = false;
		state_time = 0f;

		//Buy_Image false
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

		my_money -= buy_sum;
		buy_sum = 0;
		Player_money.GetComponent<Text>().text = my_money.ToString();
		
	}

	public void Start_UI()
	{
		
	}


	public void Accident_UI() //8번 지역에 도착하면 차가 달려와서 교통사고 나고 layer 바뀌어서 조명 8번에만 비추고 UI 출력
	{
		GameObject Accident_car = GameObject.Find("Accident_car_parent").transform.GetChild(0).gameObject;
		Accident_car.SetActive(true);
		Accident_car.transform.position = Vector3.MoveTowards(Accident_car.transform.position, Player_1.transform.position, Time.deltaTime * 8f);
		if(Accident_car.transform.position == Player_1.transform.position)
        {
			Debug.Log("꽝!");
			state = true;
			if (state_time > 0.5f)
			{
				for(int i=0; i<3; i++)
                {
					Accident_car.SetActive(false);
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

                }
				GameObject.Find("8").gameObject.layer = 0;
				UI_Ob.transform.GetChild(3).gameObject.SetActive(true);

			}
			/*
			 if 횟수 2번이 끝나면
			  layer = 0으로 다 돌려놓을 것.
			 * 
			 */
			
			Accident_car.SetActive(false);
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

	public void Increase_UI()
	{
		state = true;
		if (state_time > 0.5f)
		{
			for (int i = 0; i < 3; i++)
			{
				Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

			}
			GameObject.Find("16").gameObject.layer = 0;
			UI_Ob.transform.GetChild(4).gameObject.SetActive(true);
		}

	}

	public void Increase_Point()
	{
		if (IncreasePoint == true && Input.GetMouseButtonDown(0)) 
		{

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{

				Debug.Log(hit.transform.gameObject.name);
				hit.transform.gameObject.layer = 0; // + 현재 자신이 가지고 있는 땅 오브젝트.layer = 0f;

				//hit.transform.gameObject.땅값 *=2;
				//hit.transform.gameObject.이펙트 -> 땅값 증가 이펙트. 샤랄라 + 방향키 위쪽 표시
				

				Debug.Log(hit.transform.gameObject.transform.name + " 땅값 상승");
				IncreasePoint = false;

				for (int i = 0; i < 3; i++)
				{
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 0);
				}

			}
		}
	}

	public void HighPass_UI()
    {
		state = true;
		if (state_time > 0.5f)
		{
			for (int i = 0; i < 3; i++)
			{
				Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

			}
			GameObject.Find("24").gameObject.layer = 0;
			UI_Ob.transform.GetChild(5).gameObject.SetActive(true);
		}
	}
	
	public void High_Pass_Point()
    {
		if (HighPassPoint == true && Input.GetMouseButtonDown(0)) //선택한 지역 이동
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{

				Debug.Log(hit.transform.gameObject.name);
				hit.transform.gameObject.layer = 0;

				Player_1.GetComponent<Player_S>().stop_land_number = int.Parse(hit.transform.gameObject.name);
				HighPassPoint = false;



				for (int i = 0; i < 3; i++)
				{
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 0);
				}
				
				
			}
		}
	}

	public void Festival_Land_Buy_UI()
	{
		//state_time = 0f;
		state = true;
		if (state_time > 1f)
		{
			Debug.Log("In_Festival");
			stop_land_number = Player_1.GetComponent<Player_S>().stop_land_number;
			Festival_Image.sprite = Buy_images[stop_land_number];
			UI_Ob.transform.GetChild(1).gameObject.SetActive(true);
			state = false;
		}

	}

	public void GoldenKey_UI()
	{
		state = true;
		if (state_time > 1f)
		{
			Debug.Log("In_GolendKey");
			int card_number = Random.Range(32, 37);
			stop_land_number = Player_1.GetComponent<Player_S>().stop_land_number;
			Golend_Key_Image.sprite = Buy_images[stop_land_number];
			UI_Ob.transform.GetChild(2).gameObject.SetActive(true);
			state = false;
		}

	}
	public void GoldenKey_UI_Clcik()
	{
		state_time = 0;
		Golden_Key_number = Random.Range(32, 37);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;
		Golend_Key_Image.sprite = Buy_images[Golden_Key_number];
		state = true;
		if(state_time > 1.5f)
        {
			GameObject.Find("Golden_key_UI").GetComponent<Button>().interactable = false;
			GameObject.Find("Golden_key_UI").SetActive(false);
			state_time = 0;
			state = false;
			Player_1.GetComponent<Player_S>().UI_Buy_bool = true;
		}
	}
}
