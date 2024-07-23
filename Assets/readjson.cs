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
    void Start()
    {
        #if UNITY_EDITOR 
        
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
        
        
        
        
        foreach (Children item in m_tree.children)
        {
            
            
            GameObject temp = Instantiate(prefab, new Vector3(0,0,Mathf.Pow(-1,i+1)*180.0f),Quaternion.identity);
            temp.GetComponent<SpawnObject>().level = 1;
            temp.GetComponent<SpawnObject>().side = i;
            temp.GetComponent<SpawnObject>().c_member = item;
            Debug.Log(item.name);
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
        string fileUrl =  Application.streamingAssetsPath+   "/recordsReal.json";
        Debug.Log(fileUrl);
        using (StreamReader sr = File.OpenText(fileUrl))
        {
           
            readData = sr.ReadToEnd();
            
            sr.Close();
        }

        return readData;
    }

    public bool readDataFromString(String json)
    {
        jsonData = json;
        Debug.Log(jsonData);
        Data m_tree = JsonUtility.FromJson<Data>(jsonData);
        GameObject prefab = (GameObject)Resources.Load("Sphere");
        int i = 0;
        GameObject temp1 = Instantiate(prefab, new Vector3(0,40,0),Quaternion.identity);
        temp1.GetComponent<SpawnObject>().level = 1;
        temp1.GetComponent<SpawnObject>().side = 0;
        temp1.name = m_tree.name;
        
        
        
        foreach (Children item in m_tree.children)
        {
            
            
            GameObject temp = Instantiate(prefab, new Vector3(0,0,Mathf.Pow(-1,i+1)*180.0f),Quaternion.identity);
            temp.GetComponent<SpawnObject>().level = 1;
            temp.GetComponent<SpawnObject>().side = i;
            temp.GetComponent<SpawnObject>().c_member = item;
            Debug.Log(item.name);
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
}
