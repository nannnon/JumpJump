using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    private Slider m_jumpingGauge;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_jumpingGauge = GameObject.Find("JumpingGauge").GetComponent<Slider>();
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

        const float kHitRadius = 0.5f;
        Vector3 belowFoot = this.transform.position + new Vector3(0, -0.1f - kHitRadius, 0);
        bool isGrounded = Physics.CheckSphere(belowFoot, kHitRadius);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            const float kJumpForce = 400;
            m_rigidBody.AddForce(new Vector3(0, kJumpForce, 0));
        }
    }
}
