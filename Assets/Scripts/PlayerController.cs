using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    private Slider m_jumpingGauge;
    private bool m_previousJumpButton;
    private float m_jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_jumpingGauge = GameObject.Find("JumpingGauge").GetComponent<Slider>();
        m_previousJumpButton = false;
        m_jumpPower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        const float kPlayerSpeed = 0.5f;
        m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(0, 0, kPlayerSpeed));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(-0.1f, 0, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(0.1f, 0, 0));
        }

        {
            const float kHitRadius = 0.5f;
            Vector3 belowFoot = this.transform.position + new Vector3(0, -0.1f - kHitRadius, 0);
            bool isGrounded = Physics.CheckSphere(belowFoot, kHitRadius);

            if (isGrounded)
            {
                bool jumpButton = Input.GetKey(KeyCode.Space);

                if (jumpButton)
                {
                    const float kJumpPowerUnit = 0.03f;
                    m_jumpPower += kJumpPowerUnit;
                    if (m_jumpPower >= 1)
                    {
                        m_jumpPower = 0;
                    }

                    m_jumpingGauge.value = m_jumpPower;
                }
                else if (m_previousJumpButton)
                {
                    const float kMaxJumpForce = 400;
                    float jumpForce = kMaxJumpForce * m_jumpPower;
                    m_rigidBody.AddForce(new Vector3(0, jumpForce, 0));
                }

                m_previousJumpButton = jumpButton;
            }
            else
            {
                m_jumpPower = 0;
            }
        }
    }
}
