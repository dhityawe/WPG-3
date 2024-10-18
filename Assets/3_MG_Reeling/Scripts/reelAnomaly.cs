using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MG_Reeling {
    public class reelAnomaly : reelingBase
    {
        public Image damageAreaImage1;
        public Image damageAreaImage2; // Second damage area
        public Image damageAreaImage3; // Third damage area

        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite invertedSprite;

        private List<int> activeDamageAreas = new List<int>();
        private Dictionary<int, Image> damageAreaImageMap = new Dictionary<int, Image>();
        private Dictionary<int, bool> damageAreaTypeMap = new Dictionary<int, bool>(); // true for inverted, false for normal

        protected override void ActivateRandomDamageAreas()
        {
            activeDamageAreas.Clear();
            damageAreaImageMap.Clear();
            damageAreaTypeMap.Clear();

            while (activeDamageAreas.Count < 3)
            {
                int newArea = UnityEngine.Random.Range(0, 8);
                if (!activeDamageAreas.Contains(newArea))
                {
                    activeDamageAreas.Add(newArea);
                    damageAreaTypeMap[newArea] = UnityEngine.Random.value > 0.5f; // Randomly assign normal or inverted
                }
            }

            damageAreaImageMap[activeDamageAreas[0]] = damageAreaImage1;
            damageAreaImageMap[activeDamageAreas[1]] = damageAreaImage2;
            damageAreaImageMap[activeDamageAreas[2]] = damageAreaImage3;

            SetDamageAreaImage(damageAreaImage1, activeDamageAreas[0]);
            SetDamageAreaImage(damageAreaImage2, activeDamageAreas[1]);
            SetDamageAreaImage(damageAreaImage3, activeDamageAreas[2]);

            hasDamaged = false; // Reset hasDamaged here

            // Log the current damage areas
            Debug.Log($"Current damage areas: {string.Join(", ", activeDamageAreas)}");
        }

        protected override void SetDamageAreaImage(Image damageAreaImage, int areaIndex)
        {
            damageAreaImage.gameObject.SetActive(true);
            damageAreaImage.fillAmount = 1f / 8f; // Set fill amount to 1/8
            var area = GetAreaAngles(areaIndex);
            damageAreaImage.transform.rotation = Quaternion.Euler(0, 0, area.startAngle + 45f);
            damageAreaImage.sprite = damageAreaTypeMap[areaIndex] ? invertedSprite : normalSprite; // Set sprite based on type
        }

        protected override (float startAngle, float endAngle) GetAreaAngles(int index)
        {
            return index switch
            {
                0 => (337.5f, 22.5f), // Mengubah posisi area damage untuk index 0
                1 => (22.5f, 67.5f),
                2 => (67.5f, 112.5f),
                3 => (112.5f, 157.5f),
                4 => (157.5f, 202.5f),
                5 => (202.5f, 247.5f),
                6 => (247.5f, 292.5f),
                7 => (292.5f, 337.5f),
                _ => (0f, 0f),
            };
        }

        protected override KeyCode[] GetKeyCodeForArea(int index)
        {
            if (damageAreaTypeMap[index])
            {
                return GetInvertedKeyCodeForArea(index);
            }

            return index switch
            {
                0 => new KeyCode[] { KeyCode.UpArrow },
                1 => new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow },
                2 => new KeyCode[] { KeyCode.LeftArrow },
                3 => new KeyCode[] { KeyCode.LeftArrow, KeyCode.DownArrow },
                4 => new KeyCode[] { KeyCode.DownArrow },
                5 => new KeyCode[] { KeyCode.DownArrow, KeyCode.RightArrow },
                6 => new KeyCode[] { KeyCode.RightArrow },
                7 => new KeyCode[] { KeyCode.RightArrow, KeyCode.UpArrow },
                _ => new KeyCode[] { KeyCode.None },
            };
        }

        private KeyCode[] GetInvertedKeyCodeForArea(int index)
        {
            return index switch
            {
                0 => new KeyCode[] { KeyCode.DownArrow },
                1 => new KeyCode[] { KeyCode.DownArrow, KeyCode.RightArrow },
                2 => new KeyCode[] { KeyCode.RightArrow },
                3 => new KeyCode[] { KeyCode.RightArrow, KeyCode.UpArrow },
                4 => new KeyCode[] { KeyCode.UpArrow },
                5 => new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow },
                6 => new KeyCode[] { KeyCode.LeftArrow },
                7 => new KeyCode[] { KeyCode.LeftArrow, KeyCode.DownArrow },
                _ => new KeyCode[] { KeyCode.None },
            };
        }

        protected override void DeactivateDamageArea(int index)
        {
            if (index < activeDamageAreas.Count)
            {
                int areaIndex = activeDamageAreas[index];
                if (damageAreaImageMap.TryGetValue(areaIndex, out Image damageAreaImage))
                {
                    damageAreaImage.gameObject.SetActive(false);
                }

                activeDamageAreas.RemoveAt(index);
                damageAreaImageMap.Remove(areaIndex);
                damageAreaTypeMap.Remove(areaIndex);

                // Reset hasDamaged to false after deactivating the damage area
                hasDamaged = false;

                // Log the deactivation
                Debug.Log($"Deactivated damage area: {areaIndex}");
            }
        }

        protected override List<int> GetActiveDamageAreas()
        {
            return activeDamageAreas;
        }
    }
}