using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much health the enemy has")]
    private int m_MaxHealth;

    [SerializeField]
    [Tooltip("How fast the enemy can move")]
    private int m_Speed;

    [SerializeField]
    [Tooltip("How much damage the enemy does")]
    private int m_Damage;

    [SerializeField]
    [Tooltip("Jump force for Enemy2")]
    private float m_JumpForce = 5f;

    [SerializeField]
    [Tooltip("Time between jumps")]
    private float m_JumpCooldown = 2f;

    private float p_JumpTimer = 0f;

    [SerializeField]
    [Tooltip("Explosion that occurs when enemy is damaged.")]
    private ParticleSystem m_DeathExplosion;

    [SerializeField]
    [Tooltip("The probability that this enemy drops a health pill")]
    private float m_HealthPillDropRate;

    [SerializeField]
    [Tooltip("The type of health pill this enemy drops")]
    private GameObject m_HealthPill;

    [SerializeField]
    [Tooltip("How many points a player recieves after killing an enemy")]
    private int m_Score;

    #endregion

    #region Private Variables
    // Player's current health
    private float p_CurHealth;
    #endregion

    #region Cached Components
    private Rigidbody cc_Rb;
    #endregion

    #region Cached References
    private Transform cr_Player;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_CurHealth = m_MaxHealth;

        cc_Rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        cr_Player = FindObjectOfType<PlayerController>().transform;

    }
    #endregion

    #region Main Updates
    private void FixedUpdate()
    {
        Vector3 dir = cr_Player.position - transform.position;
        dir.Normalize();
        cc_Rb.MovePosition(cc_Rb.position + dir * m_Speed * Time.fixedDeltaTime);

        if (CompareTag("Enemy2"))
        {
            p_JumpTimer -= Time.fixedDeltaTime;
            if (p_JumpTimer <= 0 && IsGrounded())
            {
                cc_Rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
                p_JumpTimer = m_JumpCooldown;
            }
        }
    }
    #endregion

    #region Collision Methods
    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.DecreaseHealth(m_Damage);
            }
        }
    }
    #endregion

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    #region Health Methods
    public void DecreaseHealth(float amount)
    {
        p_CurHealth -= amount;
        if (p_CurHealth <= 0)
        {
            ScoreManager.singleton.IncreaseScore(m_Score);
            Debug.Log(ScoreManager.singleton.m_CurScore.ToString());
            if (Random.value < m_HealthPillDropRate)

            {
                Instantiate(m_HealthPill, transform.position, Quaternion.identity);
            }

            Instantiate(m_HealthPill, transform.position, Quaternion.identity);
            if (gameObject.CompareTag("Enemy"))
            {
                Instantiate(m_DeathExplosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    #endregion

}
