using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour
{
    // 木の耐久
    public int treeHP = 3;

    // ドロップする木材Prefab
    public GameObject woodPrefab;

    // 木材ドロップ数
    public int dropCount = 3;

    // 倒れ済み判定
    private bool isFallen = false;

    public speech_text villagerSpeech;

    private void OnTriggerEnter(Collider other)
    {
        // 1. まず相手のタグをチェック（Axeじゃなければ即座に無視する）
        if (!other.CompareTag("Axe"))
            return;

        // 🛠️ テスト用ログ（おのが当たったときだけ表示されるようになります）
        Debug.Log("★おのが木に触れました！相手の名前: " + other.name);

        if (isFallen)
            return;

        AxeAttack attack =
            other.GetComponentInParent<AxeAttack>();

        if (attack == null ||
            !attack.isAttacking)
        {
            // 💡 もしおのは当たっているのにHPが減らない場合は、ここが原因です
            Debug.Log("おのは当たったが、攻撃中(isAttacking)になっていません");
            return;
        }

        treeHP--;
        Debug.Log("木HP : " + treeHP);

        if (treeHP <= 0)
        {
            StartCoroutine(FallTree());
        }
    }

    IEnumerator FallTree()
    {
        isFallen = true;

        // 左右ランダム
        int direction =
            Random.value > 0.5f ? 1 : -1;

        float rotate = 0;

        // 横に倒れる
        while (rotate < 90f)
        {
            float speed =
                Mathf.Lerp(
                    20f,
                    120f,
                    rotate / 90f
                ) * Time.deltaTime;

            transform.Rotate(
                speed * direction,
                0,
                0
            );

            rotate += speed;

            yield return null;
        }

        // 少し前に倒れる
        float front = 0;

        while (front < 20f)
        {
            float speed =
                50f *
                Time.deltaTime;

            transform.Rotate(
                0,
                0,
                speed
            );

            front += speed;

            yield return null;
        }

        // 木材生成
        DropWood();

        // 村人のスクリプトに通知
        if (villagerSpeech != null)
        {
            villagerSpeech.OnTreeCut();
        }

        // 少し待つ
        yield return new WaitForSeconds(0.5f);

        // 木を削除
        Destroy(gameObject);
    }

    void DropWood()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector3 pos =
                transform.position +
                Vector3.up;

            GameObject wood =
                Instantiate(
                    woodPrefab,
                    pos,
                    Random.rotation
                );

            Rigidbody rb =
                wood.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 force =
                    new Vector3(
                        Random.Range(-2f, 2f),
                        Random.Range(2f, 4f),
                        Random.Range(-2f, 2f)
                    );

                rb.AddForce(
                    force,
                    ForceMode.Impulse
                );
            }
        }
    }
}