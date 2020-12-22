using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_auto : MonoBehaviour
{
	
    // Start is called before the first frame update
	int n;
	int n_destroy;
    void Start()
    {
        n=0;
		if(gameObject.tag=="ball_3")
		{
			n_destroy=150;
		}
		else if(gameObject.tag=="tree")
		{
			n_destroy=10000;
		}
		else
		{
			n_destroy=2000;
		}
    }

    // Update is called once per frame
    void Update()
    {
        n+=1;
		if(n>n_destroy)
		{
			Destroy(gameObject);
			n=0;
		}
    }
}
