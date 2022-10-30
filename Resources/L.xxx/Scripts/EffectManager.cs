using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 이팩트를 보관하여 전송해준다 싱글톤
/// </summary>

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance = null;

    public ParticleSystem dying;
    public ParticleSystem bombboom;
    public ParticleSystem bombboomAfter;
    public ParticleSystem defuseBomb;
    public ParticleSystem crash;
    public ParticleSystem redSmoke;
    public ParticleSystem healCharacter;


    [SerializeField]
    List<Transform> dyingBombPos = new List<Transform>();

    public enum eEffectType
    {
        None, Dying, BombBoom, BombBoomAfter, DefuseBomb, Crash, RedSmoke,Heal,
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static EffectManager Instance
    {
        get { return instance; }
    }

    public void PlayEffect(Vector3 _pos, Vector3 _normal, Transform _parent = null, eEffectType _noweffect = eEffectType.None)
    {
        var target = dying;

        switch (_noweffect)
        {
            case eEffectType.None:
                break;

            case eEffectType.Dying:
                target = dying;
                break;

            case eEffectType.DefuseBomb:
                target = defuseBomb;
                break;

            case eEffectType.BombBoom:
                target = bombboom;
                break;

            case eEffectType.BombBoomAfter:
                target = bombboomAfter;
                break;

            case eEffectType.Crash:
                target = crash;
                break;

            case eEffectType.RedSmoke:
                target = redSmoke;
                break;

            case eEffectType.Heal:
                target = healCharacter;
                break;
        }

        target = Instantiate(target, _pos, Quaternion.LookRotation(_normal));
        target.transform.SetParent(_parent);

        target.Play();
    }

    public IEnumerator BombEffectTwice(int num, GameObject[] _object)
    {
        //PlayEffect(_object[num].gameObject.transform.position, Vector3.forward, _object[num].gameObject.transform, eEffectType.BombBoomPre);

        for (int i = 0; i < 2; ++i)
        {
            PlayEffect(_object[num].gameObject.transform.position, Vector3.forward, _object[num].gameObject.transform, eEffectType.BombBoom);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator BombEffectAfter(int num, GameObject[] _object)
    {
        //PlayEffect(_object[num].gameObject.transform.position, Vector3.forward, _object[num].gameObject.transform, eEffectType.BombBoomPre);

        //for (int i = 0; i < 2; ++i)
        {
            PlayEffect(_object[num].gameObject.transform.position, Vector3.forward, _object[num].gameObject.transform, eEffectType.BombBoomAfter);
              yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator Crashing(GameObject _object)
    {
        for (int i = 0; i < 2; ++i)
        {
            PlayEffect(_object.transform.position, Vector3.up, _object.transform, eEffectType.Crash);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DyingBomb(int i)
    {
        PlayEffect(dyingBombPos[i].position, Vector3.up, null, eEffectType.Dying);
    }
}
