using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    


    public ReadJson.Children c_member;
    public int level = 0;
    public bool isSelected = false;
    public float tradius = 40.0f;

    private Material thisLevel;
    private Material thisLevelleft;

    private Material hightlight;

    public string id;

    public int dsId;

    public string type;

    public int side;

    public float[] r;
    // Start is called before the first frame update
    void Start()
    {
        tradius = 120.0f / (level + 1);
        switch (level)
        {
            case 0 :
                tradius = 0.0f;
                break;
            case 1:
                tradius = r[1];
                break;
            case 2:
                tradius = r[2];
                break;
            case 3:
                tradius = r[3];
                break;
            case 4:
                tradius = r[4];
                break;
        }
        //Debug.Log(this.gameObject.GetComponentsInChildren<Transform>().GetValue(3).ConvertTo<Transform>().name);
        //Debug.Log(40.0f/tradius);
        this.gameObject.GetComponentsInChildren<Transform>().GetValue(4).ConvertTo<Transform>().transform.localScale = new Vector3(0.01f*tradius/4.0f,0.01f*tradius/4.0f,0.01f*tradius/4.0f);
         
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        GameObject cy = (GameObject)Resources.Load("Cylinder");
        this.name = c_member.name;
        this.id = c_member.id;
        this.dsId = c_member.dsId;
        this.type = c_member.type;
        hightlight = Resources.Load("Hilight", typeof(Material)) as Material;
        
        thisLevel = Resources.Load(level.ToString()+".1", typeof(Material)) as Material;
        thisLevelleft = Resources.Load(level.ToString(), typeof(Material)) as Material;
        
        if (c_member.children.Length == 0)
        {

            gameObject.GetComponentsInChildren<MeshRenderer>().GetValue(1).ConvertTo<MeshRenderer>().enabled = false;
        }
        else
        {
            float angle = 2.0f*Mathf.PI / (float)c_member.children.Length;
            gameObject.GetComponentsInChildren<MeshRenderer>().GetValue(1).ConvertTo<MeshRenderer>().enabled = true;
            for (int i = 0; i < c_member.children.Length; i++)
            {
                GameObject temp = Instantiate(prefab, new Vector3(this.transform.position.x+tradius*Mathf.Cos(angle*(float)i),this.transform.position.y-40,this.transform.position.z+tradius*Mathf.Sin(angle*(float)i)),Quaternion.identity);
                
                temp.transform.parent = this.gameObject.GetComponentsInChildren<Transform>().GetValue(4)
                    .ConvertTo<Transform>();
                temp.GetComponent<SpawnObject>().level = this.level+1;
                temp.GetComponent<SpawnObject>().side = this.side;
                temp.GetComponent<SpawnObject>().c_member = this.c_member.children[i];
                temp.GetComponent<SpawnObject>().r = this.r;
                float length = (temp.transform.position - this.transform.position).magnitude;
                GameObject cy1 = Instantiate(cy, (temp.transform.position + this.transform.position) / 2,
                    Quaternion.identity);
                cy1.transform.localScale = new Vector3(1,length/2,1);
                cy1.transform.up = -(temp.transform.position - this.transform.position);
                cy1.transform.parent = this.gameObject.GetComponentsInChildren<Transform>().GetValue(4)
                    .ConvertTo<Transform>();
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected == false)
        {
            if (side == 0)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = thisLevelleft;
                
            }else if (side == 1)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = thisLevel;
            }
            this.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.1f);
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = hightlight;
            this.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        }
    }

    public void SetRadius(float radius)
    {
        this.gameObject.GetComponentsInChildren<Transform>().GetValue(3).ConvertTo<Transform>().transform.localScale = new Vector3(0.01f*radius/4.0f,0.01f*radius/4.0f,0.01f*radius/4.0f);
    }

}
