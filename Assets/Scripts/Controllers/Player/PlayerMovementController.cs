using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isNotStarted = true;

        private bool _isOnRight = true;


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager>();
            _data = _manager.GetData();
        }


        private void FixedUpdate()
        {
            ClampControl();

            if (_isNotStarted)
            {
                return;
            }

            if (_isClicked)
            {
                _rig.velocity = Vector3.zero;
                _rig.AddForce(new Vector3(_data.ForceX * (_isOnRight ? 1 : -1), _data.ForceY, 0), ForceMode.Impulse);
                _isClicked = false;
            }
        }

        private void ClampControl()
        {
            if ((!_isOnRight && _rig.position.x <= -_data.MaxHorizontalPoint) || (_isOnRight && _rig.position.x >= _data.MaxHorizontalPoint))
            {
                _rig.velocity = new Vector3(0, _rig.velocity.y);
            }

            if (_rig.position.y >= _data.MaxVerticalPoint)
            {
                _rig.position = new Vector3(_rig.position.x, _data.MaxVerticalPoint - 0.1f);

            }
        }

        public void OnClicked(int direction)
        {
            _isClicked = true;
            _isOnRight = direction == 1;
        }


        public void OnPlay()
        {
            _isNotStarted = false;
            _rig.useGravity = true;
        }
        public void OnPlayerDie()
        {
            //_rig.velocity = Vector3.zero;
        }

        public void OnReset()
        {
            _rig.useGravity = false;
            _isOnRight = true;
            transform.position = new Vector3(_data.InitializePosX,_data.InitializePosY);
            _rig.velocity = Vector3.zero;
            _rig.angularVelocity = Vector3.zero;
            _rig.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}