using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus : MonoBehaviour
{
	public float speed = 1;
    private float timer = 0;
    private float dir_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
        if (timer > 4)
        {
            dir_y = Random.Range(-1, 1f);//取随机数，参数为浮点型，取得随机数就是浮点型
            timer = 0;//当timer>4秒置空，重新生成随机数即改变运动方向
			gameObject.transform.position += new Vector3(dir_y, 0, 0);
         
        }
    
        
    }
	
	
}
