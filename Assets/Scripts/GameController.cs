using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject m_block;
    private List<GameObject> m_blocks = new List<GameObject>();
    private GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_block = Resources.Load<GameObject>("Block");

        // ブロックで道を作る
        for (int i = 0; i < 10; ++i)
        {
            GameObject go = Instantiate(m_block);
            Transform t = go.transform;
            go.transform.position = new Vector3(0, 0, 10 * i);
            go.transform.localScale = new Vector3(10, 1, 10);

            m_blocks.Add(go);
        }

        m_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 一定距離離れた最後尾のブロックを破棄する
        {
            const float kExistDistance = 30;
            float distanceThreshold = m_player.transform.position.z - kExistDistance;
            GameObject lastBlock = m_blocks[0];
            if (lastBlock.transform.position.z <= distanceThreshold)
            {
                Destroy(lastBlock);
                m_blocks.RemoveAt(0);
            }
        }

        // ブロック数が一定量まで減ったら追加する
        {
            const int kExistingBlocksSize = 30;

            if (m_blocks.Count < kExistingBlocksSize)
            {
                GameObject go = Instantiate(m_block);

                // 位置
                {
                    Vector3 firstBlockPos = m_blocks[m_blocks.Count - 1].transform.position;
                    Vector3 diff = new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 0f), Random.Range(30f, 60f));
                    go.transform.position = firstBlockPos + diff;
                }

                // 大きさ
                go.transform.localScale = new Vector3(10, 1, 30);

                m_blocks.Add(go);
            }
        }

        // プレイヤーが一定距離落ちたらゲームオーバー
        {
            const float kGameOverHeight = 13;
            Vector3 firstBlockPos = m_blocks[m_blocks.Count - 1].transform.position;
            float th = firstBlockPos.y - kGameOverHeight;

            if (m_player.transform.position.y <= th)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}