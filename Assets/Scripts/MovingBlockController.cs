using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    Vector3 m_initialPos;
    float m_theta = 0;

    Rigidbody m_rb;


    // Start is called before the first frame update
    void Start()
    {
        m_initialPos = transform.position;
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = m_initialPos;
        pos.x += Mathf.Sin(m_theta) * 10;
        m_theta += 0.01f;

        m_rb.MovePosition(pos);
    }
}
