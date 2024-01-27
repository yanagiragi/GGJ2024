using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position.x = Mathf.Sin(Time.frameCount / 3.14f) * 300 + Screen.width / 2f;
            transform.position = position;

    }
}
