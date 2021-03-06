using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;

        private Player playerShooting;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private Vector3 _bodyDirection = new Vector3(0f, 0f, 1f);
        private float _bodyAngle = 0f;
        private Vector3 _lookDirection;
        private IGameCamera _gameCamera;

        public Transform tankBody;
        public Transform tankTurret;

        private void Awake()
        {
            _gameCamera = ServiceLocator.GetGameCamera();

            _direction = Vector3.zero;
            _rigidbody = GetComponent<Rigidbody>();
            playerShooting = GetComponent<Player>();
        }

        private void Update()
        {
            if (playerShooting.PlayerAlive)
            {
                _direction.x = Input.GetAxisRaw("Horizontal");
                _direction.z = Input.GetAxisRaw("Vertical");
            }

            _bodyDirection = _direction;

            if (_bodyDirection != Vector3.zero)
            {
                _bodyAngle = Vector3.SignedAngle(new Vector3(0f, 0f, 1f), _bodyDirection, new Vector3(0f, 1f, 0f));
                tankBody.rotation = Quaternion.Euler(0f, _bodyAngle, 0f);
            }
        }

        private void FixedUpdate()
        {
            var offset = _rigidbody.position + _direction * moveSpeed * Time.fixedDeltaTime;

            if (playerShooting.PlayerAlive)
            {
                _rigidbody.MovePosition(offset);
                TurretDirectionUpdate();
            }
        }

        private void TurretDirectionUpdate()
        {
            var mouseRay = _gameCamera.ScreenPointToRay(Input.mousePosition);
            var midPoint = (transform.position - _gameCamera.Position).magnitude * 0.8f;
            _lookDirection = mouseRay.origin + mouseRay.direction * midPoint;
            _lookDirection.y = tankTurret.position.y;
            tankTurret.LookAt(_lookDirection);
        }
    }
}
