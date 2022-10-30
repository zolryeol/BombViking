using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ���ݶ��̴��� ������ ĳ���Ͱ� ȸ���Ѵ�.
/// </summary>

public class HealCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Viking"))
        {
            EffectManager.Instance.PlayEffect(other.transform.position + new Vector3(0, 2, 0), Vector3.forward, other.transform, EffectManager.eEffectType.Heal);
            SoundManager.Instance.healSound.Play();
            for (int i = 0; i < 5; ++i)
            {
                BombWick.BomWickUp();
            }
        }
    }

}
