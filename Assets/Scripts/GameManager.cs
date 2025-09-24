using UnityEngine;

public class GameManager : MonoBehaviour
{
    //--> variable estatica, es global se puede acceder desde cualquier script y compartida entre todas las copias
    //get; private set; (con esto hacemos que cualquiera pueda acceder pero que solo se pueda modificar desde aquí)
    public static GameManager instance {get; private set; }

    int _stars = 0;

    void Awake()
    {
        //busca si instance esta rellenado, si ya esta relleno comprueba si lo que está dentro es este objeto u otro
        if(instance != null && instance != this)
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

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellas recogidas = " + _stars);
    }
}
