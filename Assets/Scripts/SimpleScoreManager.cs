using UnityEngine;

public class SimpleScoreManager : MonoBehaviour
{
    private int noOfWrongAttempts = 0;

    public void IncreaseWrongAttempts()
    {
        noOfWrongAttempts++;
    }

    public int GetFinalScore()
    {
        return 10 - noOfWrongAttempts; // no of correct selections
    }
}
