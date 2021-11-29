using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public float size = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GenerateColor();
    }

    public void GenerateColor()
    {
        GetComponent<Renderer>().sharedMaterial.color = Random.ColorHSV();
    }
    
    public void Reset()
    {
        GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }

}
