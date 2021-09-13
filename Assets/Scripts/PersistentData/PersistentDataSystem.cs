using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PersistentDataSystem : MonoBehaviour
{
    [HideInInspector] public List<DataProviderBase> dataProviders = new List<DataProviderBase>();

    public DataProviderPlayerPrefs dataProviderPlayer;
    public static readonly string key_Money = "TLD_MONEY";
    public static readonly string key_Diamond = "TLD_Diamond";
    public readonly string key_Sector = " TLD_LEVEL";

    public readonly string key_Employees_Sector = "TLD_EMPLOYEES_SECTOR";
    public readonly string key_Timing_Sector = "TLD_TIMING_SECTOR";
    public readonly string key_BasePrice_Sector = "TLD_BasePrice_Sector";
    public readonly string key_PERCENTEGE_SALARY_SECTOR = "TLD_Percentege_Salary_Sector";
    public readonly string key_Building_SECTOR = "TLD_Building_Sector";
    public readonly string key_LEVEL_SECTOR = "TLD_LEVEL_SECTOR";
    public readonly string key_MONEY_Sector = "TLD_MONEY_SECTOR";
    public readonly string key_LIMITLEVEL_SECTOR = "TLD_LIMITLEVEL_SECTOR";
    public readonly string key_CityLevel_Sector = "TLD_CITYLEVEL_SECTOR";
    public readonly string key_ReduceSalaryByPerks_Sector = "TLD_ReduceSalary_SECTOR";
    public readonly string key_UpgradeSectorReducedByPerks_Sector = "TLD_UpgradeSectorReducedByPerks_SECTOR";
    public readonly string key_MotorizedHelpByPerks_Sector = "TLD_MotorizedHelps_SECTOR";
    public readonly string key_ReduceTimeForHelpCustomers_Sector = "TLD_ReduceTimeForHelpCustomers_SECTOR";
    public readonly string key_SubSector_Sector = "TLD_SubSector_SECTOR";
    public readonly string key_TimeForEventHappen = "TLD_TimeForEventHappen_PERKS";
    public readonly string key_MaxTimeForAnEventHappen = "TLD_MaxTimeForAnEventHappen";
    public readonly string key_ReproductionAnCurrentEvent = "TLD_ReproductionAnCurrentEvent_PERKS";
    public readonly string key_EventHappenningNumber = "TLD_EventHappenningNumber";
    public readonly string key_CurrentMissionsActivate_EMERGENCY = "TLD_CurrentMissionsActivate_EMERGENCY";
    public readonly string key_CurrentMissionsState_EMERGENCY = "TLD_CurrentMissionsState_EMERGENCY";
    public readonly string key_CitizenData_SERIALIZABLE = "TLD_CITIZENDATA_SERIALIZABLE";
    public readonly string key_CurrentParkingSystem_SERIALIZABLE = "TLD_CurrentPARKINGSystem_SERIALIZABLE";

    private DataProviderPlayerPrefs _providerDataForSectors;
    [HideInInspector] public PersistentDataInt_LOCAL cityLevelData;
    [HideInInspector] public PersistentDataFloat_LOCAL moneyData;
    [HideInInspector] public PersistentDataInt_LOCAL diamondData;
    [HideInInspector] public PersistenDataString_LOCAL missionTables;
    [HideInInspector] public PersistentDataFloat_LOCAL currentParkingPersistentSystem;
    [HideInInspector] public float moneyPerMinute;
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> localTimingSectorDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataInt_LOCAL> localEmployeesSectorDictionary = new Dictionary<string, PersistentDataInt_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> localBasePriceSectorDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> localPercentegeSalaryDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> localDelayTimingForBuildDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataInt_LOCAL> localLevelSectorDictionary = new Dictionary<string, PersistentDataInt_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> localMoneySectorDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataInt_LOCAL> localSubSectorDictionary = new Dictionary<string, PersistentDataInt_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> perksDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> timeEventsDictionary = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public Dictionary<string, PersistentDataFloat_LOCAL> currentDelayForMission = new Dictionary<string, PersistentDataFloat_LOCAL>();
    [HideInInspector] public PersistenDataString_LOCAL dataStringLocal;
    [HideInInspector] public PersistenDataString_LOCAL citizenDataLocalString;
    
    private void Awake()
    {
        GameManager.managerGame.systemDataPersistent = this;
    }

  
}
