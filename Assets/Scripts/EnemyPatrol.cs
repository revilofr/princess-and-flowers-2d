using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision=20;

    public SpriteRenderer graphics;

    private Transform target;
    private int destPoint = 0; // index du point de destination courant

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f) {
            destPoint = (destPoint + 1) % waypoints.Length; // passe au point suivant
            target = waypoints[destPoint];
            graphics.flipX = target.position.x < transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
