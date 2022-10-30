using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 김주환 교수님이 추천해주심
/// 
/// 장애물구현할때 참고 해볼 것
/// </summary>

public enum eBuildingStyle
{
    eDefault, eGrass, eRunis, eTower,
}

public class BuildingObject : MonoBehaviour
{
    public eBuildingStyle myStyle;

    public void Reset()
    {
        SetBuildingStyle(myStyle);
    }

    private void SetBuildingStyle(eBuildingStyle style)
    {
//         switch (style)
//         {
//             case eBuildingStyle.eDefault:
//                 {
//                     // 프리팹을 적당히 바꿔준다.
//                 }
//         }

    }
}
