using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 delta = new Vector3(0, -Input.GetAxis("Mouse Y")*10, -Input.GetAxis("Mouse X")*10);
                        Debug.Log(delta);
                        this.transform.position += delta;
                    }
            
        }

        Vector3 back = new Vector3(-Input.GetAxis("Mouse ScrollWheel")*10, 0, 0);
        this.transform.position += back;
    }
}
