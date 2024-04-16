using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform playerSpawn;

    public Animator animator;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            Debug.Log("Checkpoint activated");
            disableCheckpoint(); 
        }
    }

    private void disableCheckpoint()
    {
        // Two approaches to disable the checkpoint
        // Destroy(gameObject); // Destroy the checkpoint after it's been activated
        gameObject.GetComponent<BoxCollider2D>().enabled = false; // Disable the collider so it can't be triggered again
        Debug.Log("Checkpoint disabled");
        animator.SetTrigger("CheckpointActivated");
    }

    
}
