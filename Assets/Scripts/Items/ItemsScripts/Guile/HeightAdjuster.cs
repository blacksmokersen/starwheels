using UnityEngine;

namespace Items
{
    public class HeightAdjuster : MonoBehaviour
    {
        public bool Adjusting;

        [SerializeField] private HeightAdjusterSettings _settings;

        private float _myHeight;
        private float _heightToAdjustTo;

        // CORE

        private void Start()
        {
            _myHeight = transform.position.y;
            Adjusting = _settings.AdjustingOnStart;
            SelectLevelToAdjustTo();
        }

        private void Update()
        {
            if (Adjusting)
            {
                var newHeight = transform.position + new Vector3(0, _heightToAdjustTo - transform.position.y, 0);
                transform.position = Vector3.Lerp(transform.position, newHeight, _settings.AdjustmentSpeed * Time.deltaTime);
            }
        }

        // PRIVATE

        private void SelectLevelToAdjustTo()
        {
            var closestHeight = _settings.LevelsHeights[0];
            var closestDistance = Mathf.Abs(closestHeight - _myHeight);
            for (var i = 1; i < _settings.LevelsHeights.Count; i++)
            {
                var distanceToThisLevel = Mathf.Abs(_settings.LevelsHeights[i] - _myHeight);
                if (distanceToThisLevel < closestDistance)
                {
                    closestHeight = _settings.LevelsHeights[i];
                    closestDistance = distanceToThisLevel;
                }
            }
            _heightToAdjustTo = closestHeight;
        }
    }
}
