//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// ĳ���Ͱ� �¿� ���̴� ���� ��Ÿ���� Ŭ����
///// 
///// �ִϸ����Ϳ� ���� ����� �̿��ϰ� �;����� �� �ȵǼ� �����ϰ� ����غ���.
///// </summary>
///// 
//public class CharacterTwist : MonoBehaviour
//{
//    // �׳� ������ ���ͼ� �غ���
//    public Transform spine;

//    //     public Animator characterAnimator;     // �ִϸ�����
//    //     public HumanBodyBones characterBoneInfo;  // ĳ���� �� ����
//    // 
//    //     //Quaternion charactorSlope;
//    // 
//    [Header("���� ����")]
//    public static float slopeAngle;

//    //private void Start()
//    //{
//    //    //characterAnimator = this.gameObject.GetComponentInChildren<Animator>();

//    //    //charactorSlope = characterAnimator.GetBoneTransform(characterBoneInfo).rotation;
//    //    //characterAnimator.SetBoneLocalRotation(characterBoneInfo, Quaternion.Euler(0, 0, 90));
//    //}

//    void LateUpdate()
//    {
//        spine.rotation = Quaternion.Euler(spine.rotation.eulerAngles.x, spine.rotation.eulerAngles.y, spine.rotation.eulerAngles.z + slopeAngle);
//        // 
//        //         slopeAngle++;

//        //         charactorSlope = Quaternion.Euler(characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.x,
//        //                          characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.y,
//        //                         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.z + slopeAngle);

//        //         //�޸ӳ��̵� �ƹ�Ÿ�϶�
//        //         characterAnimator.SetBoneLocalRotation(characterBoneInfo, Quaternion.Euler(
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.x,
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.y,
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.z + slopeAngle));
//        //         slopeAngle++;
//    }
//}