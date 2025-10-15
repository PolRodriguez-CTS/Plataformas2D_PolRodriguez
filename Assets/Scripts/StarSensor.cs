using System;
using JetBrains.Annotations;
using UnityEngine;

public class StarSensor : MonoBehaviour
{

    public static StarSensor instance { get; private set; }
    //Sensor de estrellas
    [SerializeField] private Transform _starSensor;
    [SerializeField] private Vector2 _starSensorArea = new Vector2(115, 30);
    [SerializeField] private int starsInLevel;
    [SerializeField] private LayerMask _starMask;

    void Awake()
    {
        //busca si instance esta rellenado, si ya esta relleno comprueba si lo que está dentro es este objeto u otro
        if (instance != null && instance != this)
        {
            //si se cumple se destruye
            Destroy(gameObject);
        }
        else
        {
            //si no se cumple se rellena
            instance = this;
        }

        //cuando cargas escenas, todo lo que había en la anterior desaparecía
        // esto hace que cuando cambies de escena no se destruya
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        starsInLevel = StarsRemaining();
    }

    //stars in level se asigna en el start a las estrellas que detecta el sensor, en el gamemanager. También asignamos que cada vez que recojamos una estrella se resta 1 a starsinlevel
    //int starsInLevel;

    /*void Start()
    {
        starsInLevel = StarsRemaining();
    }*/

    void Update()
    {
        //StarsRemaining();
        Debug.Log("Hay " + starsInLevel + "en la escena");
        Debug.Log("StarsRemaining" + StarsRemaining());
    }

    public int StarsRemaining()
    {
        Collider2D[] stars = Physics2D.OverlapBoxAll(_starSensor.position, _starSensorArea, 0, _starMask);
        return stars.Length;
    }
}