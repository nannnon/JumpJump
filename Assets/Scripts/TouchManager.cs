using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private bool m_leftButton = false;
    private bool m_rightButton = false;
    private bool m_jumpButton = false;

    public bool LeftButton  { get { return m_leftButton; } }
    public bool RightButton { get { return m_rightButton; } }
    public bool JumpButton  { get { return m_jumpButton; } }

    public void lPushDown()
    {
        m_leftButton = true;
    }

    public void lPushUp()
    {
        m_leftButton = false;
    }

    public void rPushDown()
    {
        m_rightButton = true;
    }

    public void rPushUp()
    {
        m_rightButton = false;
    }

    public void jPushDown()
    {
        m_jumpButton = true;
    }

    public void jPushUp()
    {
        m_jumpButton = false;
    }
}
