using UnityEngine;
using System.Collections;

public class ChildColliderTrigger : MonoBehaviour {
    GameObject terrain;
    public bool hit = false;
    // Use this for initialization
    void Start()
    {
        terrain = GameObject.Find("Terrain");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider == terrain.GetComponent<TerrainCollider>())
        {
            //Debug.Log("Hi");
            hit = true;
        }
    }
}