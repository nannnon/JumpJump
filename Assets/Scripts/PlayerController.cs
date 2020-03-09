using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        this.m_rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.m_rigidBody.MovePosition(this.m_rigidBody.position + new Vector3(0, 0, 0.1f));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.m_rigidBody.MovePosition(this.m_rigidBody.position + new Vector3(-0.1f, 0, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.m_rigidBody.MovePosition(this.m_rigidBody.position + new Vector3(0.1f, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            const float kJumpForce = 400;
            this.m_rigidBody.AddForce(new Vector3(0, kJumpForce, 0));
        }
    }
}
