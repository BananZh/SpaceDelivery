using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int collectedLoot = 0;
    public int targetLootCount = 5;
    public void AddLoot(int lootCost)
    {
        collectedLoot += lootCost;
        if (collectedLoot >= targetLootCount) GameOver();
    }

    void GameOver()
    {
        Application.Quit();
    }
}
