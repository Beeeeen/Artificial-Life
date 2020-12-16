using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour
{
	public Slider slider;
	
    // Start is called before the first frame update
    void Start()
    {
        slider.value=1;
    }

    // Update is called once per frame
    void Update()
    {
		
        if(Input.GetKey(KeyCode.RightArrow))
        { 
            gameObject.transform.position += new Vector3(0.03f, 0, 0);
        }
		if(Input.GetKey(KeyCode.LeftArrow))
        { 
            gameObject.transform.position += new Vector3(-0.03f, 0, 0);
        }
		if(Input.GetKey(KeyCode.UpArrow))
        { 
            gameObject.transform.position += new Vector3(0, 0.03f, 0);
        }
		if(Input.GetKey(KeyCode.DownArrow))
        { 
            gameObject.transform.position += new Vector3(0, -0.03f, 0);
        }
    }
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
}
