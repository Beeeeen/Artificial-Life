using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nerd_control : MonoBehaviour
{
	public Slider slider;//health bar
	public float time=0;
	public int randomNum=0;//the direction
	public GameObject pheromone;
	public GameObject ben;
	public GameObject nerd;
	public GameObject tree;
	GameObject child ;//health bar for not inverse
	GameObject image ;//view for change color
	int n=0;//frame
	int pheromoneNum=10; 
	int with_virus=0; // if >=1 with virus
	float speed=0;	
	float speed2=0;
	int radiusDistance=4;//range of view
	string id;
	int stop=0;//if =1 will stop;
	int DoNotFollow=0;//if  =1 will not random run
	int find_virus=0;//if  =1 遠離
	float Ageing=0.8f;
	public int know_kiwi=0;// if =1 know position of kiwi
	public Vector3 kiwi_pos = new Vector3(0,0,0);
	int apple_count=0;
	int pine_count=0;
	Color32 MyRed=new Color32(255,27,35,22);
	Color32 MyColor=new Color32(255,192,27,15);
    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;//health bar for not inverse 
		image = child.transform.GetChild(1).gameObject;//view for change color
    }

    // Update is called once per frame
    void Update()
    {
		speed=UnityEngine.Random.Range(1,11)*0.009f;
		speed2=UnityEngine.Random.Range(1,11)*0.007f;
		if(pine_count>=10)
		{
			with_virus=0;
			MyColor=new Color32(255,192,27,35);
		}
		if(with_virus>=1)//感染,速度變慢
		{
			speed=speed*0.8f;
			speed2=speed*0.8f;
			image.GetComponent<Image>().color=MyRed;//變色
		}
		else if(with_virus==0)
		{
			image.GetComponent<Image>().color=MyColor;//變色
		}
		//died
		if(slider.value<=0)
		{			
			
			Destroy(gameObject);
			return;
		}
		slider.value-=0.00025f*with_virus; //感染
		slider.value-=0.00012f*Ageing; //老化
		
		if(n%30==0)
		{
			randomNum = UnityEngine.Random.Range(1,9);// creates a number between 1 and 8			
		}
		//produce pheromone
		if (n%pheromoneNum==0)
		{						
			GameObject copy=Instantiate(pheromone,gameObject.transform.position,transform.rotation);								
		}
		
		MoveRandom();
		//MoveByKeyboard();
		n++;
		
		if(n>=100000000)//reset 
		{
			n=0;
		}
        FindInRange("virus");;//virus 優先閃躲
		
		if(find_virus<=0)
		{
			FindInRange("apple");
			FindInRange("pineapple");
		}
		else
		{
			find_virus-=1;
		}
		
		if(DoNotFollow==0)
		{				
			FindInRange("kiwi");
		}
		else
		{
			DoNotFollow-=1;
		}		
		if(apple_count>=15)//produce tree;
		{
			Instantiate(tree,gameObject.transform.position,transform.rotation);
			apple_count=0;
		}
		
		child.transform.rotation = Quaternion.Euler (0f, 0, 0);//don't rotate the health bar
    }
	void OnCollisionEnter2D(Collision2D col)//物體碰撞觸發
	{
		
		if (col.gameObject.name == "virus(Clone)") //如果標籤是virus
		{			
			slider.value-=0.3f;	//扣血
			with_virus+=1;//持續扣血 緩速
			image.GetComponent<Image>().color=MyRed;//改變視野顏色
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.gameObject.name == "Apple(Clone)") //如果標籤是apple
		{
			slider.value+=0.15f;
			apple_count+=1;
			Debug.Log(apple_count);
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.gameObject.name == "Kiwi(Clone)") 
		{			
			//Instantiate(nerd,transform.position,transform.rotation);
			//Destroy(col.gameObject); //消滅碰撞的物件
			//slider.value=0;
			DoNotFollow=400;
			know_kiwi+=1;
			kiwi_pos=col.transform.position;
		}
		else if (col.gameObject.tag == "ben") 
		{	
			if(know_kiwi!=0)
			{
				col.gameObject.SendMessage("setkiwi", kiwi_pos);			
				know_kiwi=0;	
			}
			if(with_virus>=1)
			{
				col.gameObject.SendMessage("setVirus");				
			}
		}	
		else if (col.gameObject.tag=="nerd") 
		{
			if(with_virus>=1)
			{
				col.gameObject.SendMessage("setVirus");				
			}
		}
		else if (col.gameObject.tag=="pineapple") 
		{
			
			Destroy(col.gameObject); //消滅碰撞的物件
			with_virus=0;
			slider.value+=0.2f;
			pine_count+=1;
		}
			
	}
	void setVirus()
	{
		if(with_virus<=3)
		{
			with_virus+=1;
		}
	}
	int FindInRange(string find_obj)
	{
		int return_value=0;
         GameObject[] obj_inView;
         obj_inView = GameObject.FindGameObjectsWithTag(find_obj); 
        
         
         var position = transform.position; 
        
         // Iterate through them and find the objects within range 
        foreach(GameObject obj in obj_inView)
		{ 
            var diff = (obj.transform.position - position);
			
            var curDistance = diff.sqrMagnitude;
			if (curDistance > radiusDistance) 
			{
				continue;
			}
			if(find_obj=="virus")
			{
				return_value=3;	
				find_virus=300;
				if(diff.y>0)
				{
					if(diff.x>0)
					{
						randomNum=6;
					}
					else
					{
						randomNum=8;
					}
				}
				if(diff.y<0)
				{
					if(diff.x>0)
					{
						randomNum=5;
					}
					else
					{
						randomNum=7;
					}
				}				
				DoNotFollow=300;
				return return_value;				
			}
			if(find_obj=="apple"||find_obj=="kiwi"||find_obj=="pineapple")
			{
				if(find_obj=="apple")
				{
					return_value=1;
				}
				else
				{
					return_value=2;
				}
				if(diff.y>0)
				{
					if(diff.x>0)
					{
						randomNum=7;
					}
					else
					{
						randomNum=5;
					}
				}
				if(diff.y<0)
				{
					if(diff.x>0)
					{
						randomNum=8;
					}
					else
					{
						randomNum=6;
					}
				}
				return return_value;
			}
			
			
            
        } 
		return return_value;
    }	
	void MoveRandom()
	{
		
		
		if(stop==1)
		{
			return;
		}
		if(randomNum ==1)
        { 	
			gameObject.transform.rotation = Quaternion.Euler (0f, 0, 0);
            gameObject.transform.position += new Vector3(speed, 0, 0);			
        }
		if(randomNum ==2)
        { 				
			gameObject.transform.rotation = Quaternion.Euler (0f, 180f, 0);
			
            gameObject.transform.position += new Vector3(-speed, 0, 0);			
        }
		if(randomNum ==3)
        { 
            gameObject.transform.position += new Vector3(0, speed, 0);
        }
		if(randomNum ==4)
        { 
            gameObject.transform.position += new Vector3(0, -speed, 0);
        }
		
		if(randomNum ==5)//左上
        { 		
			gameObject.transform.rotation = Quaternion.Euler (0f, 180f, 0);
            gameObject.transform.position += new Vector3(-speed, speed2, 0);			
        }
		if(randomNum ==6)//左下
        { 								
            gameObject.transform.rotation = Quaternion.Euler (0f, 180f, 0);
            gameObject.transform.position += new Vector3(-speed, -speed2, 0);
        }
		if(randomNum ==7)//右上
        { 
            gameObject.transform.rotation = Quaternion.Euler (0f, 0, 0);
            gameObject.transform.position += new Vector3(speed, speed2, 0);	
        }
		if(randomNum ==8)//右下
        { 
            gameObject.transform.rotation = Quaternion.Euler (0f, 0, 0);
            gameObject.transform.position += new Vector3(speed, -speed2, 0);	
        }
	}
	void MoveByKeyboard()
	{
		if(Input.GetKey(KeyCode.RightArrow))
        { 	
			
            gameObject.transform.position += new Vector3(speed, 0, 0);			
        }
		if(Input.GetKey(KeyCode.LeftArrow))
        { 				
			
			
            gameObject.transform.position += new Vector3(-speed, 0, 0);			
        }
		if(Input.GetKey(KeyCode.UpArrow))
        { 
            gameObject.transform.position += new Vector3(0, speed, 0);
        }
		if(Input.GetKey(KeyCode.DownArrow))
        { 
            gameObject.transform.position += new Vector3(0, -speed, 0);
        }
	}
	
}
