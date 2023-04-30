using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Create global level settings")]
    internal class LevelSettings : ScriptableObject
    {
        [SerializeField]
        private LevelInfo[] _levelInfo;

        public LevelInfo GetInfo(int level)
        {
            int levelIndex = GetLevelIndex(level);
            int nextLevelIndex = GetLevelIndex(level + 1);

            var info = _levelInfo[levelIndex];
            var nextInfo = _levelInfo[nextLevelIndex];

            info.FillColor = nextInfo.BackgroundColor;

            return info;
        }
        int GetLevelIndex(int level) => level % _levelInfo.Length;
    }
}