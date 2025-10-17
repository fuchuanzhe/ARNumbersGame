using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;
    public TMP_Text buttonAText;
    public TMP_Text buttonBText;
    public TMP_Text feedbackText;

    NumberManager numberManager;
    AudioSource audioSource;

    SimpleScoreManager scoreManager;

    public AudioClip countOutLoud;
    public AudioClip congrats;
    public AudioClip tryAgain;

    readonly string[] numberWords = {
        "", "One","Two","Three","Four","Five",
        "Six","Seven","Eight","Nine","Ten"
    };

    string correctWord;

    void Start()
    {
        numberManager = FindFirstObjectByType<NumberManager>();
        audioSource = FindFirstObjectByType<AudioSource>();
        scoreManager = FindFirstObjectByType<SimpleScoreManager>();

        buttonA.onClick.AddListener(() => OnClick(buttonA, buttonAText.text));
        buttonB.onClick.AddListener(() => OnClick(buttonB, buttonBText.text));
        feedbackText.text = "";

        audioSource.PlayOneShot(countOutLoud);
        numberManager.SpawnNext();
        BuildQuestionFromCurrentNumber();
    }

    private void BuildQuestionFromCurrentNumber()
    {
        buttonA.gameObject.SetActive(true);
        buttonB.gameObject.SetActive(true);
        
        int n = numberManager.CurrentNumber;
        numberManager.SpawnNext();
        n = numberManager.CurrentNumber;
        correctWord = numberWords[n];

        int wrongN;
        do wrongN = UnityEngine.Random.Range(1, 11);
        while (wrongN == n);
        string wrongWord = numberWords[wrongN];

        bool placeCorrectOnA = UnityEngine.Random.value < 0.5f;
        if (placeCorrectOnA)
        {
            buttonAText.text = correctWord;
            buttonBText.text = wrongWord;
        }
        else
        {
            buttonAText.text = wrongWord;
            buttonBText.text = correctWord;
        }
    }

    void OnClick(Button clickedButton, string chosen)
    {
        if (String.Equals(chosen, correctWord))
        {
            audioSource.PlayOneShot(congrats);
            feedbackText.text = "Correct!";
            scoreManager.IncreaseAttempts();
            if (numberManager.hasNextToSpawn())
            {
                BuildQuestionFromCurrentNumber();
            }
            else
            {
                EndGame();
            }
        }
        else
        {
            audioSource.PlayOneShot(tryAgain);
            feedbackText.text = "Try again!";
            scoreManager.IncreaseAttempts();
            clickedButton.gameObject.SetActive(false);
        }
    }

    private void EndGame()
    {
        numberManager.Clear();
        feedbackText.text = $"Congratulations!\nYou got {scoreManager.GetFinalScore()} questions right!";
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);
    }
}
