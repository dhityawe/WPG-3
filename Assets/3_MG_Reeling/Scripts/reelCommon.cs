using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MG_Reeling {
    public class ReelCommon : reelingBase
    {
        public Image damageAreaImage;

        private int activeDamageArea;

        protected override void ActivateRandomDamageAreas()
        {
            activeDamageArea = Random.Range(0, 4);
            SetDamageAreaImage(damageAreaImage, activeDamageArea);
        }

        protected override void SetDamageAreaImage(Image damageAreaImage, int areaIndex)
        {
            damageAreaImage.gameObject.SetActive(true);
            damageAreaImage.fillAmount = 0.25f;
            var area = GetAreaAngles(areaIndex);
            damageAreaImage.transform.rotation = Quaternion.Euler(0, 0, area.startAngle + 90f);
        }

        protected override (float startAngle, float endAngle) GetAreaAngles(int index)
        {
            return index switch
            {
                0 => (315f, 45f),
                1 => (45f, 135f),
                2 => (135f, 225f),
                3 => (225f, 315f),
                _ => (0f, 0f),
            };
        }

        protected override KeyCode[] GetKeyCodeForArea(int index)
        {
            return index switch
            {
                0 => new KeyCode[] { KeyCode.UpArrow },
                1 => new KeyCode[] { KeyCode.LeftArrow },
                2 => new KeyCode[] { KeyCode.DownArrow },
                3 => new KeyCode[] { KeyCode.RightArrow },
                _ => new KeyCode[] { KeyCode.None },
            };
        }

        protected override void DeactivateDamageArea(int index)
        {
            damageAreaImage.gameObject.SetActive(false);
            StartCoroutine(ReactivateDamageAreaAfterDelay());
        }


        protected override List<int> GetActiveDamageAreas()
        {
            return new List<int> { activeDamageArea };
        }
    }
}