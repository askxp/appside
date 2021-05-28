using System.Collections.Generic;
using UnityEngine;

namespace Appside.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AllLevels", menuName = "Data/AllLevels", order = 1)]
    public class AllLevels : ScriptableObject, ILevelStorage
    {
        public List<Level> levels;
        public List<Level> GetAllLevels()
        {
            return levels;
        }
    }
}