using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
/// <summary>
/// 스팀 도전과제
/// </summary>

public enum eAchieveState
{ Default, Run50 = 50, Run100 = 100, Run10 = 10, Run200 = 200, Run500 = 500, Run1000 = 1000, Run2000 = 2000, Defuse1, Defuse5, Defuse10, Defuse20, Rank1, RankFisrt }

public class Achievement : MonoBehaviour
{
    private static Achievement instance = null;

    public eAchieveState nowAchieveState = eAchieveState.Default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AchieveCheck(eAchieveState _nowState)
    {
        nowAchieveState = _nowState;

        switch (nowAchieveState)
        {
            case eAchieveState.Default:
                {
                    break;
                }
            case eAchieveState.Defuse1:
                {
                    SteamUserStats.SetAchievement("ACH_FirstTime");
                    break;
                }
            case eAchieveState.Run50:
                {
                    SteamUserStats.SetAchievement("ACH_50M");
                    break;
                }
            case eAchieveState.Run100:
                {
                    SteamUserStats.SetAchievement("ACH_100M");
                    break;
                }
            case eAchieveState.Run10:
                {
                    SteamUserStats.SetAchievement("ACH_10M");
                    break;
                }
            case eAchieveState.Run200:
                {
                    SteamUserStats.SetAchievement("ACH_200M");
                    break;
                }
            case eAchieveState.Run500:
                {
                    SteamUserStats.SetAchievement("ACH_500M");
                    break;
                }
            case eAchieveState.Run1000:
                {
                    SteamUserStats.SetAchievement("ACH_1000M");
                    break;
                }
            case eAchieveState.Run2000:
                {
                    SteamUserStats.SetAchievement("ACH_2000M");
                    break;
                }
            case eAchieveState.Defuse5:
                {
                    SteamUserStats.SetAchievement("ACH_5Defuse");
                    break;
                }
            case eAchieveState.Defuse10:
                {
                    SteamUserStats.SetAchievement("ACH_10Defuse");
                    break;
                }
            case eAchieveState.Defuse20:
                {
                    SteamUserStats.SetAchievement("ACH_20Defuse");
                    break;
                }
            case eAchieveState.Rank1:
                {
                    SteamUserStats.SetAchievement("ACH_Winner");
                    break;
                }
            case eAchieveState.RankFisrt:
                {
                    SteamUserStats.SetAchievement("ACH_FirstScore");
                    break;
                }
        }

        SteamUserStats.StoreStats();

        nowAchieveState = eAchieveState.Default;
    }

    public static Achievement Instance
    {
        get
        { return instance; }
    }
}
