using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{

    public List<String> list;
    public int level = 0;
    public bool isSelected = false;
    public float tradius = 40.0f;

    private Material thisLevel;

    private Material hightlight;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.gameObject.GetComponentsInChildren<Transform>().GetValue(3).ConvertTo<Transform>().name);
        //Debug.Log(40.0f/tradius);
        this.gameObject.GetComponentsInChildren<Transform>().GetValue(3).ConvertTo<Transform>().transform.localScale = new Vector3(4.0f/tradius,4.0f/tradius,4.0f/tradius);
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        GameObject cy = (GameObject)Resources.Load("Cylinder");
        
        hightlight = Resources.Load("Hilight", typeof(Material)) as Material;
        thisLevel = Resources.Load(level.ToString(), typeof(Material)) as Material;
        
        if (list.Count == 0)
        {

            gameObject.GetComponentsInChildren<MeshRenderer>().GetValue(1).ConvertTo<MeshRenderer>().enabled = false;
        }
        else
        {
            float angle = 2.0f*Mathf.PI / (float)list.Count;
            gameObject.GetComponentsInChildren<MeshRenderer>().GetValue(1).ConvertTo<MeshRenderer>().enabled = true;
            for (int i = 0; i < list.Count; i++)
            {
                GameObject temp = Instantiate(prefab, new Vector3(tradius*Mathf.Cos(angle*(float)i),0,tradius*Mathf.Sin(angle*(float)i)),Quaternion.identity);
                temp.name = list[i];
                temp.transform.parent = this.gameObject.GetComponentsInChildren<Transform>().GetValue(3)
                    .ConvertTo<Transform>();
                temp.GetComponent<SpawnObject>().level = this.level+1;
                float length = (temp.transform.position - this.transform.position).magnitude;
                GameObject cy1 = Instantiate(cy, (temp.transform.position + this.transform.position) / 2,
                    Quaternion.identity);
                cy1.transform.localScale = new Vector3(1,length/2,1);
                cy1.transform.up = -(temp.transform.position - this.transform.position);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected == false)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = thisLevel;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = hightlight;
        }
    }

}
