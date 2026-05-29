using UnityEngine;
using TMPro;

public class speech_text : MonoBehaviour
{
    public TextMeshProUGUI speechText;
    private int textStage = 0;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        // 最初（ステージ0〜2）だけ、スペースキーでもセリフが進むようにしておく
        if (Input.GetKeyDown(KeyCode.Space) && textStage < 2)
        {
            textStage++;
            UpdateText();
        }
    }

    void UpdateText()
    {
        if (speechText == null) return;

        switch (textStage)
        {
            case 0:
                speechText.text = "ようこそ";
                break;
            case 1:
                speechText.text = "炭を一つ\n作ってみてください";
                break;
            case 2:
                speechText.text = "まず木を\n切ってみましょう";
                break;
            case 3: // 👈 木が切られたらここになる
                speechText.text = "台においてみてください";
                break;
        }
    }

    // 💡 【重要】木が切られたときに、外部からこの命令を呼び出します
    public void OnTreeCut()
    {
        textStage = 3; // ステージを3（台において〜）に強制進行
        UpdateText();
    }
}　　