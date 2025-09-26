using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttackInfo
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("Name for attack")]
    private string m_AttackName;
    public string AttackName
    {
        get { return m_AttackName; }
    }

    [SerializeField]
    [Tooltip("Button to use attack")]
    private string m_Button;
    public string Button
    {
        get { return m_Button; }
    }

    [SerializeField]
    [Tooltip("The trigger string to use the attack in the animator")]
    private string m_TriggerName;
    public string TriggerName
    {
        get { return m_TriggerName; }
    }

    [SerializeField]
    [Tooltip("The prefab of the game object representing the ability")]
    private GameObject m_AbilityGO;
    public GameObject AbilityGO
    {
        get { return m_AbilityGO; }
    }

    [SerializeField]
    [Tooltip("Where to spawn the ability in respect to the player")]
    private Vector3 m_Offset;
    public Vector3 Offset
    {
        get { return m_Offset; }

    }

    [SerializeField]
    [Tooltip("How long to wait before the attack can be used again")]
    public float m_WindUpTime;
    public float WindUpTime
    {
        get { return m_WindUpTime; }
        set { m_WindUpTime = value; }
    }

    [SerializeField]
    [Tooltip("How long to wait before the player can do anything again")]
    public float m_FrozenTime;
    public float FrozenTime
    {
        get { return m_FrozenTime; }
        set { m_FrozenTime = value; }
    }

    [SerializeField]
    [Tooltip("How long to wait before ability can be used again")]
    private float m_Cooldown;

    [SerializeField]
    [Tooltip("OG frozen time")]
    private float m_OGFrozenTime;

    [SerializeField]
    [Tooltip("OG Wimd up time")]
    private float m_OGWindUpTime;

    [SerializeField]
    [Tooltip("The amount of health this ability costs")]
    private int m_HealthCost;
    public int HealthCost
    {
        get { return m_HealthCost; }
    }

    [SerializeField]
    [Tooltip("The color to change when using the ability")]
    private Color m_Color;
    public Color AbilityColor
    {
        get { return m_Color; }
    }

    #endregion

    #region Public Variables

    public float Cooldown
    {
        get;
        set;
    }

    #endregion

    #region Cooldown Methods

    public void ResetCooldown()
    {
        Cooldown = m_Cooldown;

    }

    public bool IsReady()
    {
        return Cooldown <= 0;
    }

    public void ResetWindUpTime()
    {
        WindUpTime = m_OGWindUpTime;
    }

    public void ResetFrozenTime()
    {
        FrozenTime = m_OGFrozenTime;
    }
    #endregion

}
