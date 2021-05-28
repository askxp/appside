using System.Collections.Generic;

namespace Appside.Scripts.ScriptableObjects
{
    public interface ILevelStorage
    {
        List<Level> GetAllLevels();
    }
}