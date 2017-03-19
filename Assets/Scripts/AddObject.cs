using UnityEngine;
using System.Collections;

public class AddObject : MonoBehaviour {
    public UnityEngine.Object myObject;
    UnityEngine.Object my;
    // Use this for initialization
    void Start () {
       my = Instantiate(myObject, transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Destroy(my);
        }
    }
}
