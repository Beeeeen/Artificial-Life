using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        { 
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }
		if(Input.GetKey(KeyCode.LeftArrow))
        { 
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
		if(Input.GetKey(KeyCode.UpArrow))
        { 
            gameObject.transform.position += new Vector3(0, 0.1f, 0);
        }
		if(Input.GetKey(KeyCode.DownArrow))
        { 
            gameObject.transform.position += new Vector3(0, -0.1f, 0);
        }
    }
	void OnTriggerEnter2D(Collider2D col) //碰撞事件
	{
		if (col.tag == "virus") //如果標籤是virus
		{
			Destroy(col.gameObject); //消滅碰撞的物件
		}
	}
}
