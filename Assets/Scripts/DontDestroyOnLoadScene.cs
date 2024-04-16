using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;
    void Awake()
    {
        // Add player tag to the player object in the hierarchy
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(player);

        foreach (GameObject obj in objects) {
            DontDestroyOnLoad(obj);
        }
    }

}
