using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_2DExplore {
    public class cameraFollow : MonoBehaviour
    {
        public Transform boat; // Referensi ke objek kapal
        private Vector3 offset; // Offset antara kamera dan kapal

        void Start()
        {
            // Hitung offset berdasarkan posisi awal kamera dan kapal
            offset = transform.position - boat.position;
        }

        void LateUpdate()
        {
            // Atur posisi kamera agar mengikuti posisi kapal dengan mempertahankan offset
            transform.position = boat.position + offset;
        }
    }
}