using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("Power for ability")]
    public int m_Power;
    public int Power()
    {
        return m_Power;
    }

    [SerializeField]
    [Tooltip("How far the attack can go")]
    public int m_Range;
    public int Range()
    {
        return m_Range;
    }
    #endregion

}
