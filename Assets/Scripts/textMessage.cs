using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textMessage : MonoBehaviour
{
    public string str;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = str;
    }

}
