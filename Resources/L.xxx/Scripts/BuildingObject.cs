using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ȯ �������� ��õ���ֽ�
/// 
/// ��ֹ������Ҷ� ���� �غ� ��
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
//                     // �������� ������ �ٲ��ش�.
//                 }
//         }

    }
}
