using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateFace: MonoBehaviour
{
    private TextMeshProUGUI ObjectText;
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent;
        ObjectText = this.GetComponentInChildren<TextMeshProUGUI>();
        if (parentTransform != null)
        {
            ObjectText.text = parentTransform.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = new Vector3(transform.position.x, 0, transform.position.z) -
        //new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        transform.forward = new Vector3(-100,0,0);
    }
}
