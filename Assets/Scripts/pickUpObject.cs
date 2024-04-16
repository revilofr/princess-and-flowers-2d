using UnityEngine;

public class pickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Inventory.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }


}
