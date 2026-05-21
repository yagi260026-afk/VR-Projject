using UnityEngine;
using System.Collections;

public class AxeAttack : MonoBehaviour
{
    public Transform axe;

    public bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SwingAxe());
        }
    }

    IEnumerator SwingAxe()
    {
        isAttacking = true;

        Quaternion startRot = axe.localRotation;

        Quaternion attackRot =
            Quaternion.Euler(60, -30, 0);

        float time = 0;

        while (time < 0.15f)
        {
            axe.localRotation =
                Quaternion.Slerp(
                    startRot,
                    attackRot,
                    time / 0.15f
                );

            time += Time.deltaTime;
            yield return null;
        }

        time = 0;

        while (time < 0.15f)
        {
            axe.localRotation =
                Quaternion.Slerp(
                    attackRot,
                    startRot,
                    time / 0.15f
                );

            time += Time.deltaTime;
            yield return null;
        }

        axe.localRotation = startRot;

        isAttacking = false;
    }
}