using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount = 0;
    public Text coinsCountText;

    public static Inventory instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    public void AddCoins(int count) {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
}
