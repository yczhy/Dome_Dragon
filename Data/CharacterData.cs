using UnityEngine;

namespace UIToolkitDemo
{
    // stores data for character instance + static data from a ScriptableObject

    public class CharacterData : MonoBehaviour
    {
        private float curPointsLife;
        private float curPointsDefense;
        private float curPointsAttack;
        private float curPointsAttackSpeed;
        private float curPointsSpecialAttack;
        private float curPointsCriticalHit;

        public float CurPointsLife { get { return curPointsLife; } set { curPointsLife += value; } }
        public float CurPointsDefense { get { return curPointsDefense; } set { curPointsDefense += value; } }
        public float CurPointsAttack { get { return curPointsAttack; } set { curPointsAttack += value; } }
        public float CurPointsAttackSpeed { get { return curPointsAttackSpeed; } set { curPointsAttackSpeed += value; } }
        public float CurPointsSpecialAttack { get { return curPointsSpecialAttack; } set { curPointsSpecialAttack += value; } }
        public float CurPointsCriticalHit { get { return curPointsCriticalHit; } set { curPointsCriticalHit += value; } }

        // how quickly XP requirements increase as level increases
        const float k_ProgressionFactor = 10f;

        // currently equipped gear
        [SerializeField] EquipmentSO[] m_GearData = new EquipmentSO[4];

        // baseline data and common shared stats
        [SerializeField] CharacterBaseSO m_CharacterBaseData;

        // experience and level, serialized for demo purposes
        [SerializeField] int m_CurrentLevel;

        GameObject m_PreviewInstance;

        public int CurrentLevel { get { return m_CurrentLevel; } set { m_CurrentLevel = value; } }

        public GameObject PreviewInstance { get { return m_PreviewInstance; } set { m_PreviewInstance = value; } }
        public CharacterBaseSO CharacterBaseData => m_CharacterBaseData;
        public EquipmentSO[] GearData => m_GearData;

        // clamped to non-negative values  
        public uint GetXPForNextLevel()
        {
            return (uint) GetPotionsForNextLevel(m_CurrentLevel, k_ProgressionFactor);
        }
        // Power potions needed to increment character level
        int GetPotionsForNextLevel(int currentLevel, float progressionFactor)
        {
            currentLevel = Mathf.Clamp(currentLevel, 1, currentLevel);
            progressionFactor = Mathf.Clamp(progressionFactor, 1f, progressionFactor);

            float xp = (progressionFactor * (currentLevel)) ;
            xp = Mathf.Ceil((float)xp);
            return (int)xp;
        }

        public uint GetCurrentPower()
        {
            // Formula for calculating "character power"
            float basePoints = m_CharacterBaseData.basePointsAttack + m_CharacterBaseData.basePointsDefense + m_CharacterBaseData.basePointsLife + m_CharacterBaseData.basePointsCriticalHit;
            float equipmentPoints = 0f;

            return (uint)(CurrentLevel * basePoints + equipmentPoints) / 10;
        }

        public void IncrementLevel()
        {
            m_CurrentLevel++;

            CharEvents.LevelIncremented?.Invoke(this);
        }
    }
}