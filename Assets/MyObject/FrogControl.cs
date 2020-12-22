using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogControl : MonoBehaviour
{
	public int find_appl=0;
	int stop=0;//if =1 will stop;
	int n=0;//frame
	public int randomNum=0;//the direction
	public GameObject pheromone;
	int pheromoneNum=15;
	float speed=0;	
	float speed2=0;	
	string id="" ; //id of this object
	//int DoNotFollow=0;
    // Start is called before the first frame update
    void Start()
    {
        id=gameObject.GetInstanceID().ToString();//get the id of object	
		//slider.value=1;
    }

    // Update is called once per frame
    void Update()
    {
		speed=UnityEngine.Random.Range(1,10)*0.005f;
		speed2=UnityEngine.Random.Range(1,10)*0.005f;
		if (n%pheromoneNum==0)
		{			
			//產生費洛蒙
			GameObject copy=Instantiate(pheromone,gameObject.transform.position,transform.rotation);				
			copy.name=id;						
		}
		if(n%100==0)
		{
			randomNum = UnityEngine.Random.Range(1,9);// creates a number between 1 and 8			
		}
        if(n>1000000)//reset
		{
			n=0;
		}
		
		n++;
		FindInRange("apple");
		MoveRandom();
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		
		if (col.gameObject.name == "Apple(Clone)") //如果標籤是apple
		{			
			find_appl+=1;		
			Debug.Log("find");
		}
			
	}
	void FindInRange(string find_obj)
	{
         GameObject[] obj_inView;
         obj_inView = GameObject.FindGameObjectsWithTag(find_obj); 
        
         var radiusDistance = 4; // the range of distance
         var position = transform.position;      
         // Iterate through them and find the objects within range 
        foreach(GameObject obj in obj_inView)
		{ 
            var diff = (obj.transform.position - position);
			
            var curDistance = diff.sqrMagnitude;
			if(find_obj=="apple")
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
            
        } 
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
	
}
