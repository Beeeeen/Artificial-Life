using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce_object : MonoBehaviour
{
	public GameObject virus;
	public GameObject apple;
	public float time; //宣告浮點數，名稱time
    private float dir_y = 0;
	private float dir_x = 0;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //時間增加
		if(time>2f) //如果時間大於0.5(秒)
		{
			dir_y = Random.Range(-5, 5);//random number
			dir_x = Random.Range(-7, 7);//random number
			Vector3 pos = new Vector3(dir_x,dir_y,0);
			Instantiate(virus,pos,transform.rotation);
			
			dir_y = Random.Range(-5, 5);//random number
			dir_x = Random.Range(-7, 7);//random number
			pos = new Vector3(dir_x,dir_y,0);
			Instantiate(apple,pos,transform.rotation);
			time = 0f; 
		}
    }
}
