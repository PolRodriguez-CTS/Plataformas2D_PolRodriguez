using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneLoader.instance.ChangeScene(sceneName);   
    }
}
