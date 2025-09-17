using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_scoreText;
    public int score=0;

    public void IncreaseScore()
    {
        score += 1;
        m_scoreText.text = score.ToString();
    }
}
