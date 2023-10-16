using UnityEngine;

public static class ScoreManager
{
    // This is a static variable that stores the score
    public static int score = 0;
    public static GameObject itemSlot ;

    // This is a static method that adds points to the score
    public static void AddScore(int points)
    {
        score += points;
    }

    // This is a static method that resets the score to zero
    public static void ResetScore()
    {
        score = 0;
    }
    public static int GetScore()
    {
        return score;
    }

    public static void InsertItem(GameObject item)
    {
        if(itemSlot!=null)
            return;
        itemSlot = item;
    }

    public static void DropItem()
    {
        
        if (itemSlot!=null)
            itemSlot = null;
    }

    public static GameObject ItemRecall()
    {
        if (itemSlot != null)
        {
            return itemSlot;
        }
        return null;
    }
}