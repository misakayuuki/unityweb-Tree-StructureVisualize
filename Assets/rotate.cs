using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
public class rotate : MonoBehaviour
{
    private float timer;
    private LayerMask mask;
    public TextMeshProUGUI show;
    private bool isRotate;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion interpolatedRotation;
    private Transform t;
    
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    #endif
    private static extern void PostScore(string id,int dsId,string type,string name);
    
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Sphere");
        isRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.collider.gameObject.transform.parent)
                {
                    t = hitInfo.collider.gameObject.transform.parent;
                    timer = 0.0f;
                    isRotate = true;
                    startRotation = t.transform.rotation;
                    Vector3 frontPoint = new Vector3(40, 0, 0);
                    Vector3 backPoint = new Vector3(hitInfo.collider.gameObject.transform.position.x-hitInfo.collider.gameObject.transform.parent.transform.position.x, 0, hitInfo.collider.gameObject.transform.position.z-hitInfo.collider.gameObject.transform.parent.transform.position.z);
                    float angle = Vector3.Angle(frontPoint, backPoint);
                    //Debug.Log(hitInfo.collider.gameObject.transform.parent.transform.position);
                    if (hitInfo.collider.gameObject.transform.position.z-hitInfo.collider.gameObject.transform.parent.transform.position.z < 0)
                    {
                        angle = -angle;
                    }
                    Debug.Log(angle);
                    Vector3 r = new Vector3(0, -angle, 0);
                    endRotation.eulerAngles = t.transform.eulerAngles - r;
                    show.text = hitInfo.collider.gameObject.name;
                    GameObject[] a =GameObject.FindGameObjectsWithTag("Sphere");
                    for (int i = 0; i < a.Length; i++)
                    {
                        a[i].GetComponent<SpawnObject>().isSelected = false;
                    }
                    
                    hitInfo.collider.gameObject.GetComponent<SpawnObject>().isSelected = true;
                    #if UNITY_WEBGL && !UNITY_EDITOR
                        PostScore(hitInfo.collider.gameObject.GetComponent<SpawnObject>().id,hitInfo.collider.gameObject.GetComponent<SpawnObject>().dsId,hitInfo.collider.gameObject.GetComponent<SpawnObject>().type,hitInfo.collider.gameObject.GetComponent<SpawnObject>().name);
                    #endif
                    

                    Camera.main.transform.position =
                        new Vector3(hitInfo.collider.gameObject.transform.parent.transform.position.x+150, hitInfo.collider.gameObject.transform.parent.transform.position.y+80, hitInfo.collider.gameObject.transform.parent.transform.position.z);
                    

                }

            }
            
            //Debug.DrawLine(Camera.main.transform.position,hitInfo.point);
        }

        if (isRotate)
        {
            timer += Time.deltaTime;
            interpolatedRotation = Quaternion.Slerp(startRotation, endRotation, timer);
            t.transform.rotation = interpolatedRotation;
            if (timer >= 1)
            {
                isRotate = false;
            }
        }
    }

    public void inputTest(string s)
    {
        show.text = s;
    }

   
}
