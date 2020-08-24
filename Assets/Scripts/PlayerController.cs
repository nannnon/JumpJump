using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;

    private Slider m_jumpingGauge;
    private bool m_previousJumpButton = false;
    private float m_jumpPower = 0;

    private Slider m_hoveringGauge;
    private float m_hoveringPower = 1;

    private bool m_moveLeft = false;
    private bool m_moveRight = false;
    private bool m_jump = false;
    private bool m_hovering = false;

    private ParticleSystem m_jetPS;

    private TouchManager m_tm;

    private AudioSource m_as4jump;
    [SerializeField] private AudioClip m_jumpSound;

    private AudioSource m_as4hovering;
    [SerializeField] private AudioClip m_hoveringSound;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_jumpingGauge = GameObject.Find("JumpingGauge").GetComponent<Slider>();
        m_hoveringGauge = GameObject.Find("HoveringGauge").GetComponent<Slider>();
        m_jetPS = GameObject.Find("Jet").GetComponent<ParticleSystem>();
        m_tm = GameObject.Find("TouchManager").GetComponent<TouchManager>();

        {
            AudioSource[] ass = GetComponents<AudioSource>();

            m_as4jump = ass[0];

            m_as4hovering = ass[1];
            m_as4hovering.clip = m_hoveringSound;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        // 左右移動
        if (Input.GetKey(KeyCode.LeftArrow) || m_tm.LeftButton)
        {
            m_moveLeft = true;
        }
        else
        {
            m_moveLeft = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) || m_tm.RightButton)
        {
            m_moveRight = true;
        }
        else
        {
            m_moveRight = false;
        }

        {
            const float kHitRadius = 0.5f;
            Vector3 belowFoot = this.transform.position + new Vector3(0, -0.1f - kHitRadius, 0);
            bool isGrounded = Physics.CheckSphere(belowFoot, kHitRadius);

            bool jumpButton = Input.GetKey(KeyCode.Space) || m_tm.JumpButton;

            // ためジャンプ
            if (!m_jump)
            {
                if (isGrounded)
                {
                    if (jumpButton)
                    {
                        const float kJumpPowerUnit = 1.5f;
                        m_jumpPower += kJumpPowerUnit * Time.deltaTime;
                        if (m_jumpPower > 1)
                        {
                            m_jumpPower = 0;
                        }

                        m_jumpingGauge.value = m_jumpPower;
                    }
                    else if (m_previousJumpButton)
                    {
                        m_jump = true;
                    }
                }
                else
                {
                    m_jumpPower = 0;
                }
            }

            // ホバリング
            m_hovering = false;
            if (isGrounded)
            {
                if (m_hoveringPower != 1)
                {
                    m_hoveringGauge.value = 1;
                }
                m_hoveringPower = 1;
            }
            else if (jumpButton && m_hoveringPower > 0)
            {
                const float kHoveringPowerUnit = 0.9f;
                m_hoveringPower -= kHoveringPowerUnit * Time.deltaTime;
                m_hoveringGauge.value = m_hoveringPower;

                m_hovering = true;
            }

            m_previousJumpButton = jumpButton;
        }
    }

    private void FixedUpdate()
    {
        // 前進
        const float kPlayerSpeed = 0.5f;
        m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(0, 0, kPlayerSpeed));

        // 左右移動
        const float kPlayerLeftRightSpeed = 0.1f;
        if (m_moveLeft)
        {
            m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(-kPlayerLeftRightSpeed, 0, 0));
        }
        if (m_moveRight)
        {
            m_rigidBody.MovePosition(m_rigidBody.position + new Vector3(kPlayerLeftRightSpeed, 0, 0));
        }

        // ためジャンプ
        if (m_jump)
        {
            const float kMaxJumpForce = 400;
            float jumpForce = kMaxJumpForce * m_jumpPower;
            m_rigidBody.AddForce(new Vector3(0, jumpForce, 0));

            m_jump = false;
            m_jumpPower = 0;

            m_as4jump.PlayOneShot(m_jumpSound);
        }

        // ホバリング
        if (m_hovering)
        {
            Vector3 vel = m_rigidBody.velocity;
            vel.y = 0;
            m_rigidBody.velocity = vel;

            m_jetPS.Play();

            if (!m_as4hovering.isPlaying)
            {
                m_as4hovering.Play();// OneShot(m_hoveringSound);
            }
        }
        else
        {
            m_jetPS.Stop();

            m_as4hovering.Stop();
        }
    }
}