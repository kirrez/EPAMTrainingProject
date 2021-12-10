using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class BackgroundScroll : MonoBehaviour
    {
        public float scrollSpeed;
        public float tileSizeX;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }


        private void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
            transform.position = _startPosition - Vector3.right * newPosition;
        }
    }
}