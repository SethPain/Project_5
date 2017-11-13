using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour {
    Vector3 newPosition;
    void Start()
    {
        newPosition = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                transform.position = new Vector3(newPosition.x, 1, newPosition.z);
            }
        }
    }
    /*
    Vector3 current;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(current != transform.position)
        {
            transform.Translate(current.x, 0, current.z);
        }
	}

    void OnMouseDown()
    {
        current = Input.mousePosition;
    }
    */
}
