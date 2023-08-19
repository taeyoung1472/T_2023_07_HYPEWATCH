using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool")]
public class PoolDataSO : ScriptableObject
{
    public PoolData[] poolDatas;

    [Serializable]
    public class PoolData/*Ǯ�� ������*/
    {
        [SerializeField] private PoolType poolType;
        [SerializeField] private int initCount;
        [SerializeField] private PoolAbleObject poolAbleObject;
        public PoolType PoolType { get { return poolType; } }
        public int InitCount { get { return initCount; } }
        public PoolAbleObject PoolAbleObject { get { return poolAbleObject; } }
    }
}