using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject m_blockPrefab;
    private List<GameObject> m_blocks = new List<GameObject>();
    private GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_blockPrefab = Resources.Load<GameObject>("Block");

        // ブロックで道を作る
        for (int i = 0; i < 10; ++i)
        {
            GameObject block = Instantiate(m_blockPrefab);
            Transform t = block.transform;
            block.transform.position = new Vector3(0, 0, 10 * i);
            block.transform.localScale = new Vector3(10, 1, 10);

            m_blocks.Add(block);
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
                AddBlocks();
            }
        }

        // プレイヤーが一定距離落ちたらゲームオーバー
        {
            const float kGameOverHeight = 25;
            float th = GetFirstBlockPos().y - kGameOverHeight;

            if (m_player.transform.position.y <= th)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    GameObject GetFirstBlock()
    {
        if (m_blocks.Count == 0)
        {
            throw new System.Exception("Blocks count is zero.");
        }

        return m_blocks[m_blocks.Count - 1];
    }

    Vector3 GetFirstBlockPos()
    {
        return GetFirstBlock().transform.position;
    }

    void AddBlocks()
    {
        float whichType = Random.value;

        // 山なり
        if (whichType <= 0.3)
        {
            for (int i = 0; i < 10; ++i)
            {
                GameObject block = Instantiate(m_blockPrefab);

                block.transform.localScale = new Vector3(10, 1, 10);

                Vector3 diff = new Vector3(
                    Random.Range(-2f, 2f),
                    0.3f,
                    GetFirstBlock().transform.localScale.z / 2 + block.transform.localScale.z / 2
                    );
                block.transform.position = GetFirstBlockPos() + diff;


                m_blocks.Add(block);
            }
        }
        // 連続的
        else
        {
            for (int i = 0; i < 10; ++i)
            {
                GameObject block = Instantiate(m_blockPrefab);

                // 位置
                Vector3 diff = new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 0f), Random.Range(30f, 60f));
                block.transform.position = GetFirstBlockPos() + diff;

                // 大きさ
                block.transform.localScale = new Vector3(10, 1, 30);

                m_blocks.Add(block);
            }
        }
    }
}