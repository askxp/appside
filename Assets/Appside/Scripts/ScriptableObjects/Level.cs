using UnityEngine;

namespace Appside.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/Level", order = 2)]
    public class Level : ScriptableObject
    {
        public string label;
        public Sprite preview;
        public LevelManager levelPrefab;
    
    }
}