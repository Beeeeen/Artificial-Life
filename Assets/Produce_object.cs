using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce_object : MonoBehaviour
{
	public GameObject virus;
	public GameObject apple;
	public GameObject kiwi;
	public GameObject pineapple;
	public GameObject ben;
	//public GameObject nerd;
	int apple_max = 10;
	int virus_max = 5;
	int kiwi_max = 3;
	int pine_max = 2;
	int ben_max = 5;
	int nerd_min = 2;
	
	public float time; //宣告浮點數，名稱time
    private double dir_y = 0;
	private double dir_x = 0;
	private double dir_float = 0;
	int n=0;
	// Start is called before the first frame update
    void Start()
    {
		//produce("apple");
		//produce("virus");
		//produce("kiwi");		
		//Time.timeScale = 10*Time.timeScale;//speed up
    }

    // Update is called once per frame
    void Update()
    {
        n+=1;
		if(n%10==0)
		{
			produce("apple");
			produce("virus");
			produce("kiwi");
			produce("pineapple");
					
		}
		if(n%500==0)
		{
			produce("ben");
		}
		if(n==10000000)
		{
			n=0;
		}
		apple_max=10+GameObject.FindGameObjectsWithTag("tree").Length*2;
		
    }
	public void produce(string obj)
	{
		dir_float = Random.Range(-10,11)/10.12345;
		dir_y = Random.Range(-5, 6)+dir_float;//random number
		dir_float = Random.Range(-10,11)/10.12345;
		dir_x = Random.Range(-14, 14)+dir_float;//random number
		Vector3 pos = new Vector3((float)dir_x,(float)dir_y,0);
		if(obj=="apple")
		{
			if(GameObject.FindGameObjectsWithTag(obj).Length <apple_max)
			{			
				Instantiate(apple,pos,transform.rotation);		
			}
		}
		else if(obj=="virus")
		{
			if(GameObject.FindGameObjectsWithTag(obj).Length <virus_max)
			{			
				Instantiate(virus,pos,transform.rotation);		
			}
		}
		else if(obj=="pineapple")
		{
			if(GameObject.FindGameObjectsWithTag(obj).Length <pine_max)
			{			
				Instantiate(pineapple,pos,transform.rotation);		
			}
		}
		else if(obj=="kiwi")
		{
			if(GameObject.FindGameObjectsWithTag(obj).Length <kiwi_max)
			{			
				Instantiate(kiwi,pos,transform.rotation);		
			}
		}
		else if(obj=="ben")
		{
			if(GameObject.FindGameObjectsWithTag("nerd").Length >=nerd_min
			//&&GameObject.FindGameObjectsWithTag("nerd").Length <=nerd_max
			&&GameObject.FindGameObjectsWithTag("ben").Length <ben_max)
			{						
				pos = new Vector3(0,0,0);
				Instantiate(ben,pos,transform.rotation);		
			}
		}		
		
	}
}
