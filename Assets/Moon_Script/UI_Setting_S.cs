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
	public bool gone_land;
	public int Golden_Key_number;
	GameObject UI_Ob;
	GameObject Player_1;
	[SerializeField]
	private Sprite[] Buy_images;
	public int[] land_money;
	public Image Buy_Image;
	public Image Festival_Image;
	public Image Golend_Key_Image;
	public Image Victory_Image;
	public Text Buy_text;
	public Text Player_money;
	public int buy_sum;
	public int my_money;
	public GameObject[] Land_House;
	public GameObject house_prefabs;
	public GameObject festival_prefabs;
	GameObject Money;
	public bool[] Land_House_Buy;
	public bool[] Gone_Land;
	public int money_number;
	bool accident;
	private IEnumerator accident_cor;
	private GameObject Map_Obj;

	void Awake()
    {
		Map_Creation();
		Debug.Log("go");
		Invoke("Start", 4f);
		Invoke("Update", 4f);
	}

	void Start()
	{

		Money = GameObject.Find("Window").transform.GetChild(0).gameObject;
		Map_Obj = GameObject.Find("Map_Obj").gameObject;
		my_money = 3000000;
		land_money = new int[4] { 1, 2, 3, 4 };
		Land_House_Buy = new bool[4] { false, false, false, false};
		Land_House = new GameObject[32];
		Gone_Land = new bool[32];
		UI_Ob = GameObject.Find("UI");
		Player_1 = GameObject.Find("Player_1");
		buy_sum = 0;
		state_time = 0f;
		gone_land = false;
		HighPassPoint = false;
		IncreasePoint = false;
		accident = false;
		for (int i=0; i<32; i++)
        {
			Gone_Land[i] = false;
        }
		Debug.Log("to");
	}


	void Update()
	{
		if (state) { 
			State();
		}

	}

	public void Map_Creation()
    {
		StartCoroutine(Mao_cor());
    }

	IEnumerator Mao_cor()
    {
		GameObject Sky_Floor = GameObject.Find("Map_Obj").transform.GetChild(0).gameObject;
		GameObject Wall = GameObject.Find("Map_Obj").transform.GetChild(1).gameObject;
		GameObject Season_Land = GameObject.Find("Map_Obj").transform.GetChild(2).gameObject;
		GameObject Event_Land = GameObject.Find("Map_Obj").transform.GetChild(3).gameObject;
		GameObject Window = GameObject.Find("Window");
		yield return new WaitForSecondsRealtime(0.05f);
		Wall.SetActive(true);

		int k = 0;
		for (int i=0; i<32; i++)
        {
            if (i == 0)
            {
				Event_Land.transform.GetChild(0).gameObject.SetActive(true);
            }
			else if (i == 8)
            {
				Event_Land.transform.GetChild(1).gameObject.SetActive(true);
			}
			else if (i == 16)
            {
				Event_Land.transform.GetChild(2).gameObject.SetActive(true);
			}
			else if (i == 24)
            {
				Event_Land.transform.GetChild(3).gameObject.SetActive(true);
			}
            else
            {
				
				Debug.Log("Seson_Map: " + k);
				Season_Land.transform.GetChild(k).gameObject.SetActive(true);
				k++;
			}
			Debug.Log("Map: " + i);
			yield return new WaitForSecondsRealtime(0.05f);
			
        }

		yield return new WaitForSecondsRealtime(0.1f);

		while (true)
		{
			Sky_Floor.transform.localScale += new Vector3(1.7f, 0.001f, 1.7f);

			if (Sky_Floor.gameObject.transform.localScale.x >= 16.9f)
			{
				yield return new WaitForSecondsRealtime(0.1f);
				Window.transform.GetChild(0).gameObject.SetActive(true);
				GameObject.Find("Map_Obj").transform.GetChild(4).gameObject.SetActive(true);

				yield break;
			}

			yield return new WaitForSecondsRealtime(0.05f);
			
		}

	
    }


	//땅에 도착했을 때 UI 등장까지 잠시 state
	void State()
    {
		state_time += Time.deltaTime;
		//Debug.Log("시간:" + state_time);
    }

	//구매 UI
	public void Money_position(int p) // 1 = 구매(내 위치->가운데) , 2 = 인수(내 위치-> 상대 위치) ,3 = (가운데 -> 내 위치) , 4 = 통행료(가운데 -> 상대)
    {
	
		if (p == 1)
		{
			StartCoroutine(Money_Cor(1));

		}
		else if (p == 2)  //자리에 알맞게 Money_position(2)넣어야함
		{
			StartCoroutine(Money_Cor(2));
		}
		else if (p == 3) ////자리에 알맞게 Money_position(3)넣어야함
		{
			StartCoroutine(Money_Cor(3));
		}
		else if (p == 4) ////SetActive하는 Gameobject가 다름. 2번이랑 알고리즘은 똑같음. 셋엑티브만 변경. 자리에 알맞게 Money_position(4)넣어야함
		{
			StartCoroutine(Money_Cor(4));
		}
    }
	IEnumerator Money_Cor(int p)
    {
		GameObject money_obj = GameObject.Find("Player_Window").transform.GetChild(0).gameObject;
		GameObject take_obj = GameObject.Find("Player_Window").transform.GetChild(1).gameObject;
		bool take = false;
		if (p == 1 || p == 2 || p == 3)
		{
			money_obj.gameObject.SetActive(true);
		}
		else if (p == 4)
		{
			
			take_obj.gameObject.SetActive(true);
			take= true;
		}
		if (p == 1)
		{
			money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -200);
			float x1 = money_obj.GetComponent<RectTransform>().anchoredPosition.x;
			float y1 = money_obj.GetComponent<RectTransform>().anchoredPosition.y;
			while (true)
			{
				
				if (x1 <= 0)
				{
					money_obj.gameObject.SetActive(false);
					yield break;
				}
				else
				{
					x1 -= 8f;
					y1 += 4f;
					money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, y1);
				}
				yield return new WaitForSecondsRealtime(0.001f);
			}
		}
		else if (p == 2)
		{
			money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -200);
			float x1 = money_obj.GetComponent<RectTransform>().anchoredPosition.x;
			float y1 = money_obj.GetComponent<RectTransform>().anchoredPosition.y;
			while (true)
			{

				if (x1 <= -400)
				{
					money_obj.gameObject.SetActive(false);
					yield break;
				}
				else
				{
					x1 -= 14f;
					y1 += 7f;
					money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, y1);
				}
				yield return new WaitForSecondsRealtime(0.001f);
			}
		}
		else if (p == 3)
		{
			money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
			float x1 = money_obj.GetComponent<RectTransform>().anchoredPosition.x;
			float y1 = money_obj.GetComponent<RectTransform>().anchoredPosition.y;
  
			while (true)
			{

				if (x1 >= 400)
				{
					money_obj.gameObject.SetActive(false);
					yield break;
				}
				else
				{
					x1 += 8f;
					y1 -= 4f;
					money_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, y1);
				}
				yield return new WaitForSecondsRealtime(0.001f);
			}
		}

		else if (p == 4) //인수할 때 UI 1.5초 동안 보여주기.
		{

			take_obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
			float x1 = money_obj.GetComponent<RectTransform>().anchoredPosition.x;
			float y1 = money_obj.GetComponent<RectTransform>().anchoredPosition.y;
			if (take == true)
			{
				yield return new WaitForSecondsRealtime(1.5f);
				take = false;
			}
			while (true)
			{

				if (x1 <= -400)
				{
					take_obj.gameObject.SetActive(false);
					yield break;
				}
				else
				{
					x1 -= 8f;
					y1 += 4f;
					take_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, y1);
					
				}
				yield return new WaitForSecondsRealtime(0.001f);
			}
		}

	}

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
		if(GameObject.Find("house_1").transform.GetChild(0).gameObject.activeSelf == true)
			GameObject.Find("house_1").transform.GetChild(0).gameObject.SetActive(false);
		if (GameObject.Find("house_2").transform.GetChild(0).gameObject.activeSelf == true)
			GameObject.Find("house_2").transform.GetChild(0).gameObject.SetActive(false);
		if (GameObject.Find("house_3").transform.GetChild(0).gameObject.activeSelf == true)
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
		StopCoroutine(accident_cor);
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
		Money_position(2);
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
		if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.activeSelf == false)
		{

			EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(true);
			if(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_1")
            {
				buy_sum += land_money[0];
				Land_House_Buy[0] = true;
				Debug.Log(buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_2")
			{
				buy_sum += land_money[1];
				Land_House_Buy[1] = true;
				Debug.Log(buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_3")
			{
				buy_sum += land_money[2];
				Land_House_Buy[2] = true;
				Debug.Log(buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_4")
			{
				buy_sum += land_money[3];
				Land_House_Buy[3] = true;
				Debug.Log(buy_sum);
			}
		}
		else if(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.activeSelf == true)
		{
			EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(false);
			if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_1")
			{
				buy_sum -= land_money[0];
				Land_House_Buy[0] = false;
				Debug.Log(-buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_2")
			{
				buy_sum -= land_money[1];
				Land_House_Buy[1] = false;
				Debug.Log(-buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_3")
			{
				buy_sum -= land_money[2];
				Land_House_Buy[2] = false;
				Debug.Log(-buy_sum);
			}
			else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Check_4")
			{
				buy_sum -= land_money[3];
				Land_House_Buy[3] = false;
				Debug.Log(-buy_sum);
			}
		}
		Buy_text.GetComponent<Text>().text = buy_sum.ToString();
	}

	public void Buy_Button()
    {
		
		state = false;
		state_time = 0f;

		//Buy_Image false
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

		my_money -= buy_sum;
		buy_sum = 0;
		Player_money.GetComponent<Text>().text = my_money.ToString();
		House(1);

		money_number = 1;
		Debug.Log("삼");
		Money_position(4);
		//Victory(3);
	}

	public void Festival_Buy_Button()
    {
		state = false;
		state_time = 0f;

		UI_Ob.transform.GetChild(1).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

		my_money -= buy_sum;
		buy_sum = 0;
		Player_money.GetComponent<Text>().text = my_money.ToString();
		House(2);
		Season_Effect();
	}

	public void Start_UI()
    {
		Debug.Log("스타트");
		Money_position(3);
	}

	public void Accident_UI() //8번 지역에 도착하면 차가 달려와서 교통사고 나고 layer 바뀌어서 조명 8번에만 비추고 UI 출력
	{
		//if(!accident)
		accident_cor = Accident_cor();
		Debug.Log("코루틴");
		StartCoroutine(accident_cor);
		
	}
	
    IEnumerator Accident_cor()
    {

		Debug.Log("코루틴!");
		GameObject Accident_car = GameObject.Find("Accident_car_parent").transform.GetChild(0).gameObject;
		Accident_car.SetActive(true);

		while (true)
		{	

			if (Accident_car.transform.position == Player_1.transform.position)
			{
				for (int i = 0; i < 3; i++)
				{
					Accident_car.SetActive(false);
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

				}
				GameObject.Find("8").gameObject.layer = 0;
				UI_Ob.transform.GetChild(3).gameObject.SetActive(true);
				Debug.Log("옹");
				Accident_car.transform.position = new Vector3(-22,0,-7);
				Accident_car.SetActive(false);
				yield return null;
			}
            else
            {
				Accident_car.transform.position = Vector3.MoveTowards(Accident_car.transform.position, Player_1.transform.position, Time.deltaTime * 1f);
			}
			yield return new WaitForSecondsRealtime(0.0f);

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

	public void Fireworkss_On()
    {
		for (int i = 0; i < 11; i++)
		{
			GameObject.Find("Fireworks").gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
    }

	public void End_Button()
    {

    }
	
	public void House(int land_type) // 1: 일반땅, 2: 축제땅
    {
		//추가사항) 1. 현재 턴이 누구냐에 따라 색 변환하게. 2.시간 남으면 건물 올리기 애니메이션

		int land_number = GameObject.Find("Player_1").GetComponent<Player_S>().stop_land_number;

		if (land_type == 1) //house
		{
			Land_House[land_number] = Instantiate(house_prefabs);
			Land_House[land_number].transform.position = GameObject.Find(land_number.ToString()).transform.position;

			if (GameObject.Find(land_number.ToString()).tag == "SummerWinter")
			{
				Land_House[land_number].transform.position = new Vector3(Land_House[land_number].transform.position.x - 0.845f, 0.5f, Land_House[land_number].transform.position.z);
				Land_House[land_number].transform.Rotate(new Vector3(0, 270, 0)); // 봄가을과 건물이 바라보는 방향이 다름.
			}
			else if (GameObject.Find(land_number.ToString()).tag == "SpringFall")
			{
				Land_House[land_number].transform.position = new Vector3(Land_House[land_number].transform.position.x, 0.5f, Land_House[land_number].transform.position.z + 0.902f);
			}

			for (int i = 0; i < 3; i++) //3번재 건물까지는 그냥 올림
			{
				if (Land_House_Buy[i] == true)
				{
					Land_House[land_number].transform.GetChild(i).gameObject.SetActive(true);
				}
			}
			if (Land_House_Buy[3] == true) //4번째 건물은 나머지 건물 다 없어지고 등장
			{
				for (int i = 0; i < 3; i++)
				{
					Land_House[land_number].transform.GetChild(i).gameObject.SetActive(false);

				}
				Land_House[land_number].transform.GetChild(3).gameObject.SetActive(true);
			}
		}
		else if (land_type == 2) //flag
		{
			Land_House[land_number] = Instantiate(festival_prefabs);
			Land_House[land_number].transform.position = GameObject.Find(land_number.ToString()).transform.position;
			if (GameObject.Find(land_number.ToString()).tag == "Festival_SummerWinter")
			{
				Land_House[land_number].transform.position = new Vector3(Land_House[land_number].transform.position.x - 0.845f, 0.5f, Land_House[land_number].transform.position.z);
				Land_House[land_number].transform.Rotate(new Vector3(0, 270, 0));
			}
			else if (GameObject.Find(land_number.ToString()).tag == "Festival_SpringFall")
			{
				Land_House[land_number].transform.position = new Vector3(Land_House[land_number].transform.position.x, 0.5f, Land_House[land_number].transform.position.z + 0.902f);
			}

			Land_House[land_number].transform.GetChild(0).gameObject.SetActive(true);
			Land_House[land_number].transform.GetChild(1).gameObject.SetActive(true);
		}
	}
	
	public void Season_Effect()
    {
		int land_number = GameObject.Find("Player_1").GetComponent<Player_S>().stop_land_number;
		Debug.Log("들어옴?");
		if (state == false)
		{
			if (land_number == 3)
			{
				Debug.Log("들어옴?!!");
				GameObject.Find("SeasonEffect").transform.GetChild(0).gameObject.SetActive(true);
			}
			else if (land_number == 12)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(1).gameObject.SetActive(true);
			}
			else if (land_number == 23)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(2).gameObject.SetActive(true);
			}
			else if (land_number == 28)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(3).gameObject.SetActive(true);
			}
			Debug.Log("1f");
			StartCoroutine(Delay_2());
		}
		
		
		if (state == true) {
			Debug.Log("??");
			if (land_number == 3)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(0).gameObject.SetActive(false);
			}
			else if (land_number == 12)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(1).gameObject.SetActive(false);
			}
			else if (land_number == 23)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(2).gameObject.SetActive(false);
			}
			else if (land_number == 28)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(3).gameObject.SetActive(false);
			}
			Debug.Log("3f");
		}
	}

	public void Victory(int n)
    {
		GameObject.Find("End").transform.GetChild(0).gameObject.SetActive(true);
        if (n == 1) //파산승리
        {
			Victory_Image.sprite = Buy_images[37];
        }
		else if (n == 2) //계절독점승리
        {
			Victory_Image.sprite = Buy_images[38];
		}
		else if (n == 3) //전국여행승리
		{
			Victory_Image.sprite = Buy_images[39];
		}
		else if (n == 4) //턴승리
		{
			Victory_Image.sprite = Buy_images[40];
		}
		Fireworkss_On();
	}

	public void Gone_land_f() //하이패스에서도 잘 들어가는지 확인할 것.
	{
		if (gone_land == false) //gone_land가 false인 상태에서 버튼 클릭시 보여주기
		{
			for (int i = 0; i < 3; i++)
			{
				Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

			}
			for(int i=0; i< 32; i++)
            {
				if(Gone_Land[i] == true && i != 0 && i != 8 && i != 16 && i != 24 && i != 3 && i != 12 && i != 23 && i != 28
					&& i != 5 && i != 14 && i != 20 && i != 27)
                {
					GameObject.Find(i.ToString()).layer = 0;
				}
            }

			gone_land = true;
		}
		else if(gone_land == true)
        {
			for (int i = 0; i < 3; i++)
			{
				Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 0);

			}
			gone_land = false;
		}

	}

	//코루틴 지역

	IEnumerator Delay_2() //seasondeffect
	{

		yield return new WaitForSeconds(3f); // 해당 시간동안 기다림
		Debug.Log("코루틴");
		state = true;
		Season_Effect();
	}


}
