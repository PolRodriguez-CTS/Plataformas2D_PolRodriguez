using System.Collections.Generic;
using UnityEngine;

public class Repaso : MonoBehaviour
{
    [SerializeField]private int variableInt = 5;
    public float variableFloat = 6.0f;
    public string variableString = "Hola mundo";
    public bool variableBool = true;
    public int[] arrayInt = new int[5] {12, 4, 8, 9, 0};
    public List<int> listInt = new List<int>(9) {8, 9, 0};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
