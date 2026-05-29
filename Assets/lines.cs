using UnityEngine;
using TMPro; // TextMeshProを使うために必要

public class Villager : MonoBehaviour
{
    // インスペクターで、頭上のTMPテキストコンポーネントをドラッグ＆ドロップして紐付ける
    public TextMeshProUGUI speechText;

    void Start()
    {
        // 最初のセリフを設定
        SetRequest("薪を10個、持ってきてください・・・");
    }

    // セリフを変更する関数（ユーザーの行動によって呼び出す）
    public void SetRequest(string newRequest)
    {
        if (speechText != null)
        {
            speechText.text = newRequest;
        }
    }
}