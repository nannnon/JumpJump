using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            const float kJumpForce = 400;
            m_rigidBody.AddForce(new Vector3(0, kJumpForce, 0));
        }
    }
}
