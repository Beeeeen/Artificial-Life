using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class init_control : MonoBehaviour
{	
	public Slider slider;//health bar
	public float time=0;
	public int randomNum=0;//the direction
	public GameObject pheromone;
	public GameObject ben;
	public GameObject nerd;
	GameObject child ;
	GameObject image ;//view for change color
	int n=0;//frame
	//int pheromoneNum=20;
	int with_virus=0;
	float speed=0;	
	float speed2=0;
	int radiusDistance=1;//range of view
	string id;
	int stop=0;//if =1 will stop;
	int DoNotFollow=0;
	int Ageing=1;
	public int know_kiwi=0;// if =1 know position of kiwi
	public Vector3 kiwi_pos = new Vector3(0,0,0);
	Color32 MyRed=new Color32(255,27,35,22);
	Color32 MyColor=new Color32(27,255,239,33);
	
    // Start is called before the first frame update
    void Start()
    {
		with_virus=0;
		child = gameObject.transform.GetChild(0).gameObject;//health bar for not inverse 
		image = child.transform.GetChild(1).gameObject;//view for change color
        slider.value=1;//initial health bar 100%
		id=gameObject.GetInstanceID().ToString();//get the id of object	
		randomNum = UnityEngine.Random.Range(1,9);// creates a number between 1 and 8		
    }

    // Update is called once per frame
    void Update()
    {		
		if (gameObject == null)
		{  
			return;
		}
		speed=UnityEngine.Random.Range(1,11)*0.007f;
		speed2=UnityEngine.Random.Range(1,11)*0.005f;
		if(with_virus>=1)//感染變慢
		{
			speed=speed*0.8f;
			speed2=speed*0.8f;
			image.GetComponent<Image>().color=new Color32(255,27,35,44);
		}
		else if(with_virus==0)
		{
			image.GetComponent<Image>().color=MyColor;//變色
		}
		slider.value-=0.00025f*with_virus; //感染
		slider.value-=0.00015f*Ageing; //老化
		if(slider.value<=0)
		{						
			Destroy(gameObject);
			return;
		}
		
		if(n%50==0)//random run
		{
			randomNum = UnityEngine.Random.Range(1,9);// creates a number between 1 and 8			
		}
		/*產生費洛蒙
		if (n%pheromoneNum==0)
		{			
			
			//GameObject copy=Instantiate(pheromone,gameObject.transform.position,transform.rotation);				
			copy.name=id;						
		}
		*/
		
		
		
		if(n>1000000)//reset
		{
			n=0;
		}		
		FindInRange("ball_3");
		FindInRange("apple");
		FindInRange("kiwi");
        //MoveByKeyboard();	
		MoveRandom();
		n++;
		child.transform.rotation = Quaternion.Euler (0f, 0, 0);//don't rotate the health bar
		
    }
	public void setkiwi(Vector3 kiwi_pos_in)
	{
		know_kiwi=1;
		kiwi_pos=kiwi_pos_in;
		//Debug.Log(kiwi_pos);
	}
	void FindInRange(string find_obj)
	{
         GameObject[] obj_inView;
         obj_inView = GameObject.FindGameObjectsWithTag(find_obj); 
        
         
         var position = transform.position; 
        
         // Iterate through them and find the objects within range 
        foreach(GameObject obj in obj_inView)
		{ 
            var diff = (obj.transform.position - position);
			
            var curDistance = diff.sqrMagnitude;
			if(find_obj=="apple"||find_obj=="kiwi"||find_obj=="pineapple")
			{
				if (curDistance > radiusDistance) 
				{
					continue;
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
			}			
			else if(find_obj=="ball_3")
			{
				if (curDistance < radiusDistance) 
				{     
					//Debug.Log(diff.y);
					
					if(DoNotFollow==0)//follow pheromone
					{
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
					}
					else if(DoNotFollow>0){
						DoNotFollow-=1;	
					}
					//stop=1;
					//Debug.Log(id+"find"+obj.tag);				
				}
			}
            
        } 
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log("."+col.gameObject.name+"."+col.gameObject.tag);
		
		if (col.gameObject.name == "virus(Clone)") //如果標籤是virus
		{
			with_virus+=1;
			image.GetComponent<Image>().color=new Color32(255,27,35,44);
			slider.value-=0.3f;
			//slider.value-=0.1f;//扣血
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.gameObject.name == "Apple(Clone)") //如果標籤是apple
		{
			slider.value+=0.15f;
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.gameObject.name == "Kiwi(Clone)") 
		{			
			Instantiate(nerd,transform.position,transform.rotation);
			Destroy(col.gameObject); //消滅碰撞的物件
			slider.value=0;
		}
		else if (col.gameObject.tag=="ball_3") 
		{			
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.gameObject.tag=="ben") 
		{						
			//DoNotFollow=500;
			if(with_virus>=1)
			{
				col.gameObject.SendMessage("setVirus");				
			}
		}
		else if (col.gameObject.tag=="nerd") 
		{			
			DoNotFollow=500;
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
		}		
	}
	void setVirus()
	{
		if(with_virus<=3)
		{
			with_virus+=1;
		}		
	}
	void MoveRandom()
	{
		if(know_kiwi==1)
		{
			var diff = (kiwi_pos - transform.position);
			if(diff.y<=0.001 &&diff.x<=0.001)
			{
				know_kiwi=0;
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
		}
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
	/*
	void OnTriggerEnter2D(Collider2D col) //碰撞事件
	{
		if (col.tag == "virus") //如果標籤是virus
		{
			slider.value-=0.1f;
			Destroy(col.gameObject); //消滅碰撞的物件
		}
		else if (col.tag == "apple") //如果標籤是apple
		{
			slider.value+=0.1f;
			Destroy(col.gameObject); //消滅碰撞的物件
		}		
	}
	void OnTriggerExit2D(Collider2D col) //碰撞結束
	{
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
	}*/
}
