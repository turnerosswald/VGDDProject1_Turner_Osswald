using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    #region Private Variables 
    public int m_CurScore;
    #endregion

    [SerializeField]
    [Tooltip("HUD REFERENCE")]
    private HUDController hud;

    #region Initialization
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        } else if (singleton != this) {
            Destroy(gameObject);
        }
        m_CurScore = 0;
    }
    #endregion

    #region Score Methods

    public void IncreaseScore(int amount)
    {
        m_CurScore += amount;
        hud.UpdateScore(m_CurScore);
        hud.UpdateEnemyCounter();
    }

    private void UpdateHighScore()
    {
        int highscore = PlayerPrefs.GetInt("HS", 0);
        Debug.Log("High score text being set to: " + highscore);
        if (!PlayerPrefs.HasKey("HS"))
        {
            PlayerPrefs.SetInt("HS", m_CurScore);
            return;
        }
        int hs = PlayerPrefs.GetInt("HS");
        if (hs < m_CurScore)
        {
            PlayerPrefs.SetInt("HS", m_CurScore);
        }
        PlayerPrefs.Save();
    }
    #endregion

    #region Destruction
    private void OnDisable()
    {
        UpdateHighScore();
    }
    #endregion
}
