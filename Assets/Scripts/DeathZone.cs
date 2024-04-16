using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;

    public Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        playerSpawn = GameObject.FindWithTag("Respawn").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision.transform));
        }
    }

    /// <summary>
    /// Respawn the player at the spawn point with a fade in and out effect
    /// </summary>
    /// <param name="transform"> The transform of the player </param>
    /// <returns></returns>
    public IEnumerator ReplacePlayer(Transform transform)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        transform.position = playerSpawn.position;
        fadeSystem.SetTrigger("FadeOut");
    }
}
