using JetBrains.Annotations;
using UnityEngine;

public class StarSensor : MonoBehaviour
{

    public static StarSensor instance { get; private set; }
    //Sensor de estrellas
    [SerializeField] private Transform _starSensor;
    [SerializeField] private Vector2 _starSensorArea = new Vector2(115, 30);

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
    
    public bool StarsRemaining()
    {
        Collider2D[] stars = Physics2D.OverlapBoxAll(_starSensor.position, _starSensorArea, 0);
        foreach (Collider2D item in stars)
        {
            if (item.gameObject.tag == "Star")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        return false;
    }
}
