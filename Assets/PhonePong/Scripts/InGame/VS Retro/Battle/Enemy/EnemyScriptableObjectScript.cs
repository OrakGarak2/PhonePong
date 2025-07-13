using UnityEngine;
using PhonePong.VSRetro.Enmity;

namespace PhonePong.VSRetro.Enmity
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/EnemyScriptableObject")]
    public class EnemyScriptableObjectScript : ScriptableObject
    {
        public EnemyData[] enemyDatas;
    }
}