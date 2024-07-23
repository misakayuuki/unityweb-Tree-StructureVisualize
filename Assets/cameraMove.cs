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
        Vector3 delta = new Vector3(-Input.GetAxis("Vertical") * 5, Input.GetAxis("UpDown") * 5,
            Input.GetAxis("Horizontal") * 5);
        this.transform.position += delta;
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Quaternion dr =Quaternion.Euler(this.transform.rotation.eulerAngles - new Vector3(-Input.GetAxis("Mouse Y") * 3, Input.GetAxis("Mouse X") * 3,0));

            this.transform.rotation = dr;
        }
        
    }
}
