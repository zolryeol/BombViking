//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// 캐릭터가 좌우 꺾이는 것을 나타내는 클래스
///// 
///// 애니메이터와 본의 기능을 이용하고 싶었으나 잘 안되서 무식하게 사용해본다.
///// </summary>
///// 
//public class CharacterTwist : MonoBehaviour
//{
//    // 그냥 억지로 들고와서 해본다
//    public Transform spine;

//    //     public Animator characterAnimator;     // 애니메이터
//    //     public HumanBodyBones characterBoneInfo;  // 캐릭터 본 정보
//    // 
//    //     //Quaternion charactorSlope;
//    // 
//    [Header("꺾일 각도")]
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

//        //         //휴머노이드 아바타일때
//        //         characterAnimator.SetBoneLocalRotation(characterBoneInfo, Quaternion.Euler(
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.x,
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.y,
//        //         characterAnimator.GetBoneTransform(characterBoneInfo).rotation.eulerAngles.z + slopeAngle));
//        //         slopeAngle++;
//    }
//}