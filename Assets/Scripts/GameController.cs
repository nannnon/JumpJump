using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject m_blockPrefab = default;
    List<GameObject> m_blocks = new List<GameObject>();
    GameObject m_player;

    int m_score = 0;
    Text m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // ブロックで道を作る
        for (int i = 0; i < 10; ++i)
        {
            GameObject block = Instantiate(m_blockPrefab);
            block.transform.position = new Vector3(0, 0, 10 * i);
            block.transform.localScale = new Vector3(10, 1, 10);

            m_blocks.Add(block);
        }

        m_player = GameObject.Find("Player");

        m_scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 一定距離離れた最後尾のブロックを破棄する
        if (m_blocks.Count > 0)
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

        // スコア更新
        m_score = (int)m_player.transform.position.z;
        m_scoreText.text = "Score : " + m_score;

        // プレイヤーが一定距離落ちたらゲームオーバー
        {
            const float kGameOverHeight = 25;
            float firstBlockPosY = GetFirstBlockPos().y;
            float lastBlockPosY = m_blocks[0].transform.position.y;
            float lowerBlockPosY = firstBlockPosY < lastBlockPosY ? firstBlockPosY : lastBlockPosY;
            float th = lowerBlockPosY - kGameOverHeight;

            if (m_player.transform.position.y <= th)
            {
                SceneManager.sceneLoaded += GameOverSceneLoaded;
                SceneManager.LoadScene("GameOverScene");
            }
        }   
    }

    void GameOverSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var controller = GameObject.Find("Game Over Controller").GetComponent<GameOverController>();
        controller.SetScore(m_score);

        SceneManager.sceneLoaded -= GameOverSceneLoaded;
    }

    GameObject GetFirstBlock()
    {
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
        if (whichType <= 0.2f)
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
        // ハイジャンプ
        else if (whichType <= 0.4f)
        {
            {
                GameObject block = Instantiate(m_blockPrefab);

                block.transform.localScale = new Vector3(3, 1, 60);

                Vector3 diff = new Vector3(
                    0,
                    0,
                    GetFirstBlock().transform.localScale.z / 2 + block.transform.localScale.z / 2
                    );
                block.transform.position = GetFirstBlockPos() + diff;

                m_blocks.Add(block);
            }

            {
                GameObject block = Instantiate(m_blockPrefab);

                block.transform.localScale = new Vector3(10, 1, 30);

                Vector3 diff = new Vector3(0, -25, 150);
                block.transform.position = GetFirstBlockPos() + diff;

                m_blocks.Add(block);
            }
        }
        // 斜め回転
        else if (whichType <= 0.6f)
        {
            for (int i = 0; i < 5; ++i)
            {
                GameObject block = Instantiate(m_blockPrefab);

                block.transform.localScale = new Vector3(5, 1, 20);

                const float kXRot = -10;
                block.transform.rotation = Quaternion.Euler(kXRot, 0, 0);

                Vector3 diff = new Vector3(
                    Random.Range(-2f, 2f),
                    2f,
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

                block.transform.localScale = new Vector3(10, 1, 30);

                float step = GetFirstBlock().transform.localScale.z / 2 + block.transform.localScale.z / 2;

                float level = m_score * 30f / 2000f;
                const float kMaxLevel = 4000 * 30f / 2000f;
                if (level > kMaxLevel) { level = kMaxLevel; }

                Vector3 diff = new Vector3(
                    Random.Range(-5f, 5f),
                    Random.Range(-6f, 0f),
                    Random.Range(step, step + 30f + level)
                    );
                block.transform.position = GetFirstBlockPos() + diff;

                m_blocks.Add(block);
            }
        }
    }
}