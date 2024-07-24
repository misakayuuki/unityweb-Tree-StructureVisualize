using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ReadJson : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Children
    {
        public string id;
        public string name;
        public string type;
        public int dsId;
        public Children[] children;
    }

    [System.Serializable]
    public class Data
    {
        public int id;
        public string name;
        public string type;
        public int dsId;
        public Children[] children;
    }
    public float tradius = 480.0f;
    public string jsonData;

    public int[] levelmaxnum ;
    public float[] r ;
    public float[] r1 ;
    public int depth;
    void Start()
    {
        depth = 0;

        #if UNITY_EDITOR 
        levelmaxnum = new int[7] { 2, 0, 0, 0, 0, 0, 0 };
        r = new float[7] { 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        r1 = new float[7] { 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        jsonData = ReadData();
        Debug.Log(jsonData);
        Data m_tree = JsonUtility.FromJson<Data>(jsonData);
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        int i = 0;
        GameObject temp1 = Instantiate(prefab, new Vector3(0,40,0),Quaternion.identity);
        temp1.GetComponent<SpawnObject>().level = 0;
        temp1.GetComponent<SpawnObject>().side = 0;
        
        temp1.GetComponent<SpawnObject>().c_member = new Children()
        {
            id = "0",
            name = m_tree.name,
            children = {}

        };
        
        foreach (Children item1 in m_tree.children)
        {
            getmaxnum(item1,1);
        }
        for (int j = depth - 1; j >= 0; j--)
        {
            if (r1[j + 1] < 0.1f)
            {
                r1[j + 1] = 10.0f;
            }
            r[j] = (r1[j + 1]) / Mathf.Sin(Mathf.PI / levelmaxnum[j]);
            r1[j] = r[j] + r1[j + 1];

        }
        
        
        foreach (Children item in m_tree.children)
        {
            
            
            GameObject temp = Instantiate(prefab, new Vector3(0,0,Mathf.Pow(-1,i+1)*r[0]),Quaternion.identity);
            temp.GetComponent<SpawnObject>().level = 1;
            temp.GetComponent<SpawnObject>().side = i;
            temp.GetComponent<SpawnObject>().c_member = item;
            temp.GetComponent<SpawnObject>().r = this.r;
            //Debug.Log(item.name);
            i++;
            float length = (temp.transform.position - temp1.transform.position).magnitude;
            GameObject cy = (GameObject)Resources.Load("Cylinder");
            GameObject cy1 = Instantiate(cy, (temp.transform.position + temp1.transform.position) / 2,
                Quaternion.identity);
            cy1.transform.localScale = new Vector3(1,length/2,1);
            cy1.transform.up = -(temp.transform.position - temp1.transform.position);
            cy1.transform.parent = temp1.transform;
            
            
        }
        
        #endif
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReadData()
    {
        string readData;
        string fileUrl =  Application.streamingAssetsPath+   "/tree.json";
        //Debug.Log(fileUrl);
        using (StreamReader sr = File.OpenText(fileUrl))
        {
           
            readData = sr.ReadToEnd();
            
            sr.Close();
        }
        

        return readData;
    }

    public bool readDataFromString(String json)
    {
        depth = 0;
        levelmaxnum = new int[7] { 2, 0, 0, 0, 0, 0, 0 };
        r = new float[7] { 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        r1 = new float[7] { 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        jsonData = json;
        Debug.Log(jsonData);
        Data m_tree = JsonUtility.FromJson<Data>(jsonData);
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        int i = 0;
        GameObject temp1 = Instantiate(prefab, new Vector3(0,40,0),Quaternion.identity);
        temp1.GetComponent<SpawnObject>().level = 0;
        temp1.GetComponent<SpawnObject>().side = 0;
        
        temp1.GetComponent<SpawnObject>().c_member = new Children()
        {
            id = "0",
            name = m_tree.name,
            children = {}

        };
        
        foreach (Children item1 in m_tree.children)
        {
            getmaxnum(item1,1);
        }
        for (int j = depth - 1; j >= 0; j--)
        {
            if (r1[j + 1] < 0.1f)
            {
                r1[j + 1] = 10.0f;
            }
            r[j] = (r1[j + 1]) / Mathf.Sin(Mathf.PI / levelmaxnum[j]);
            r1[j] = r[j] + r1[j + 1];

        }
        
        
        foreach (Children item in m_tree.children)
        {
            
            
            GameObject temp = Instantiate(prefab, new Vector3(0,0,Mathf.Pow(-1,i+1)*r[0]),Quaternion.identity);
            temp.GetComponent<SpawnObject>().level = 1;
            temp.GetComponent<SpawnObject>().side = i;
            temp.GetComponent<SpawnObject>().c_member = item;
            temp.GetComponent<SpawnObject>().r = this.r;
            //Debug.Log(item.name);
            i++;
            float length = (temp.transform.position - temp1.transform.position).magnitude;
            GameObject cy = (GameObject)Resources.Load("Cylinder");
            GameObject cy1 = Instantiate(cy, (temp.transform.position + temp1.transform.position) / 2,
                Quaternion.identity);
            cy1.transform.localScale = new Vector3(1,length/2,1);
            cy1.transform.up = -(temp.transform.position - temp1.transform.position);
            cy1.transform.parent = temp1.transform;
            
            
        }



        

        return true;
    }

    void getmaxnum(Children child, int level)
    {
        if ((level + 1) > levelmaxnum.Length)
        {
            return;
        }

        depth = level;
        //Debug.Log(level);
        if (child.children.Length > levelmaxnum[level])
        {
            levelmaxnum[level] = child.children.Length;
        }

        if (child.children.Length > 0)
        {
            foreach(Children item in child.children)
            {
                getmaxnum(item,level+1);
            }
        }
    }
}
