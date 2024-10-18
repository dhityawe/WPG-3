using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MG_Reeling {
    public class ReelUncommon : reelingBase
    {
        public Image damageAreaImage1;
        public Image damageAreaImage2;

        private List<int> activeDamageAreas = new List<int>();
        private Dictionary<int, Image> damageAreaImageMap = new Dictionary<int, Image>();

        protected override void ActivateRandomDamageAreas()
        {
            activeDamageAreas.Clear();
            damageAreaImageMap.Clear();

            while (activeDamageAreas.Count < 2)
            {
                int newArea = UnityEngine.Random.Range(0, 6);
                if (!activeDamageAreas.Contains(newArea))
                {
                    activeDamageAreas.Add(newArea);
                }
            }

            damageAreaImageMap[activeDamageAreas[0]] = damageAreaImage1;
            damageAreaImageMap[activeDamageAreas[1]] = damageAreaImage2;

            SetDamageAreaImage(damageAreaImage1, activeDamageAreas[0]);
            SetDamageAreaImage(damageAreaImage2, activeDamageAreas[1]);
        }

        protected override void SetDamageAreaImage(Image damageAreaImage, int areaIndex)
        {
            damageAreaImage.gameObject.SetActive(true);
            damageAreaImage.fillAmount = 1f / 6f;
            var area = GetAreaAngles(areaIndex);
            damageAreaImage.transform.rotation = Quaternion.Euler(0, 0, area.startAngle + 60f);
        }

        protected override (float startAngle, float endAngle) GetAreaAngles(int index)
        {
            return index switch
            {
                0 => (330f, 30f),
                1 => (30f, 90f),
                2 => (90f, 150f),
                3 => (150f, 210f),
                4 => (210f, 270f),
                5 => (270f, 330f),
                _ => (0f, 0f),
            };
        }

        protected override KeyCode[] GetKeyCodeForArea(int index)
        {
            return index switch
            {
                0 => new KeyCode[] { KeyCode.UpArrow },
                1 => new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow },
                2 => new KeyCode[] { KeyCode.LeftArrow, KeyCode.DownArrow },
                3 => new KeyCode[] { KeyCode.DownArrow },
                4 => new KeyCode[] { KeyCode.DownArrow, KeyCode.RightArrow },
                5 => new KeyCode[] { KeyCode.RightArrow, KeyCode.UpArrow },
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
            }
        }

        protected override List<int> GetActiveDamageAreas()
        {
            return activeDamageAreas;
        }
    }
}