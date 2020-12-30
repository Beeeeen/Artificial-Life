using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_control : MonoBehaviour
{
	public GameObject virus;
	public GameObject apple;
	public GameObject kiwi;
	public GameObject pineapple;
	public GameObject tree;
	public GameObject pheromone;
	public GameObject Nerd;
	public GameObject Ben;
	float speed=0;	
	public int know_kiwi=0;// if =1 know position of kiwi
	public Vector3 kiwi_pos = new Vector3(0,0,0);
	int can_detroy=0;
	int n=0;
    // Start is called before the first frame update	
    void Start()
    {
        Instantiate(virus,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
		
        speed=UnityEngine.Random.Range(1,11)*0.01f;
		MoveByKeyboard();
		
		if(n<=0)
		{
			produce();
		}
		else
		{
			n-=1;
		}
    }
	void produce()
	{
		if(Input.GetKey(KeyCode.V))
        { 	
			Instantiate(virus,transform.position,transform.rotation);
			n=10;
        }
		else if(Input.GetKey(KeyCode.A))
        { 	
			Instantiate(apple,transform.position,transform.rotation);
			n=10;			
        }
		else if(Input.GetKey(KeyCode.P))
        { 	
			Instantiate(pineapple,transform.position,transform.rotation);            	
			n=10;
        }
		else if(Input.GetKey(KeyCode.K))
        { 	
			Instantiate(kiwi,transform.position,transform.rotation);            	
			n=10;
        }
		else if(Input.GetKey(KeyCode.T))
        { 	
			Instantiate(tree,transform.position,transform.rotation);            	
			n=10;
        }
		else if(Input.GetKey(KeyCode.C))
        { 				
			can_detroy=1;
        }
		else if(Input.GetKey(KeyCode.H))
        { 	
			Instantiate(pheromone,transform.position,transform.rotation);            	
			n=10;
        }
		else if(Input.GetKey(KeyCode.L))
		{
			Destroy(GameObject.FindGameObjectsWithTag("tree")[0]);
			n=10;
		}
		else if(Input.GetKey(KeyCode.B))
		{
			Instantiate(Ben,transform.position,transform.rotation);            	
			n=10;
		}
		else if(Input.GetKey(KeyCode.N))
		{
			Instantiate(Nerd,transform.position,transform.rotation);            	
			n=10;
		}
	}
	void OnCollisionEnter2D(Collision2D col)//物體碰撞觸發
	{
		if(can_detroy==1)
		{
			Destroy(col.gameObject); //消滅碰撞的物件
			can_detroy=0;
		}
	}
	
	
	void MoveByKeyboard()
	{
		if(Input.GetKey(KeyCode.RightArrow))
        { 	
			gameObject.transform.rotation = Quaternion.Euler (0f, 0f, 0);
            gameObject.transform.position += new Vector3(speed, 0, 0);			
        }
		if(Input.GetKey(KeyCode.LeftArrow))
        { 				
			gameObject.transform.rotation = Quaternion.Euler (0f, 180f, 0);
			
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
