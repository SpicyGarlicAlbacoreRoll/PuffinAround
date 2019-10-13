using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class featherScript : MonoBehaviour
{
    public int numFeathers = 0;
    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFeathers(int num) {
        numFeathers = num;
        print("number of feathers: " + numFeathers);
    }

    public int GetFeathers() {
        return numFeathers;
    }
}
