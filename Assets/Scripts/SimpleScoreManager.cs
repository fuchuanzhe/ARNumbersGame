using UnityEngine;

public class SimpleScoreManager : MonoBehaviour
{
    private int noOfAttempts = 0;

    public void IncreaseAttempts()
    {
        noOfAttempts++;
    }

    public int GetFinalScore()
    {
        return 20 - noOfAttempts; // no of correct selections
    }
}
