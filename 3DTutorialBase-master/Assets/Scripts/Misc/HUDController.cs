using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{

    #region Editor Variables

    [SerializeField]

    [Tooltip("The part of the health that decreases")]
    private RectTransform m_HealthBar;

    [SerializeField]
    [Tooltip("Enemy counter")]
    private TMP_Text m_EnemyCounterUI;

    [SerializeField]
    [Tooltip("Score Counter")]
    private TMP_Text m_ScoreUI;

    [SerializeField]
    [Tooltip("powerup activation fire rate")]
    private TMP_Text m_POWERUP;

    [SerializeField]
    [Tooltip("powerup activation invincibility")]
    private TMP_Text m_POWERUPInvincibility;

    [SerializeField]
    private PlayerController player;

    #endregion

    #region Private Variables
    private float p_HealthBarOrigWidth;
    private int m_EnemiesKilled;

    private static HUDController instance;
    #endregion

    #region Initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        p_HealthBarOrigWidth = m_HealthBar.sizeDelta.x;
        m_EnemyCounterUI.text = "Enemies Killed: 0";
        m_ScoreUI.text = "Score: 0";
        m_EnemiesKilled = 0;
    }
    #endregion

    #region Update Health Bar
    public void UpdateHealth(float percent)
    {

        m_HealthBar.sizeDelta = new Vector2(p_HealthBarOrigWidth * percent, m_HealthBar.sizeDelta.y);
    }

    #endregion

    #region Update UI elements

    public static int CurrentScore = 0;

    public void UpdateScore(int score)
    {
        CurrentScore = score;
        m_ScoreUI.text = "Score: " + score;
        if (score >= 50)
        {
            StartCoroutine(LoadNextArena());
        }

    }

    public void UpdateEnemyCounter()
    {
        m_EnemiesKilled++;
        m_EnemyCounterUI.text = "Enemies Killed: " + m_EnemiesKilled;
        if (m_EnemiesKilled %10 == 0)
        {
            StartPowerup();
        }

        if (m_EnemiesKilled % 13 == 0)
        {
            StartPowerUpInvincibility();
        } 
    }

    public void StartPowerup()
    {
        StartCoroutine(ActivatePOWERUP());
    }

    public void StartPowerUpInvincibility()
    {
        StartCoroutine(ActivatePOWERUPInvincibility());
    }

    public IEnumerator ActivatePOWERUP()
    {
        m_POWERUP.gameObject.SetActive(true);
        player.POWERUP();
        yield return new WaitForSeconds(5);
        m_POWERUP.gameObject.SetActive(false);
        Debug.Log("Gameobject set to false.");
        player.POWERDOWN();
    }

    public IEnumerator ActivatePOWERUPInvincibility()
    {
        m_POWERUPInvincibility.gameObject.SetActive(true);
        player.m_IsInvincible = true;
        yield return new WaitForSeconds(5);
        m_POWERUPInvincibility.gameObject.SetActive(false);
        player.m_IsInvincible = false;

    }
    #endregion

    private IEnumerator LoadNextArena()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Arena2");
    }
}
