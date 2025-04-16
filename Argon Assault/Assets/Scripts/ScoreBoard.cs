using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    private void Start() 
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score: 0";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;

        scoreText.text = $"Score: {score}"; // You can also use the ToString() method.
    }
}
