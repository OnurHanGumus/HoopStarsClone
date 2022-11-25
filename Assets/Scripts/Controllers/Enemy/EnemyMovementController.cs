using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Data.UnityObject;
using Data.ValueObject;

public class EnemyMovementController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    #endregion
    #region Private Variables
    private Rigidbody _rig;
    private EnemyManager _manager;
    private EnemyData _data;

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
        _manager = GetComponent<EnemyManager>();
        _data = _manager.GetData();
    }


    private void FixedUpdate()
    {
        ClampControl();
    }

    private IEnumerator ForceDelay()
    {
        yield return new WaitForSeconds(_data.JumpDelay);
        if (_manager.Target.position.y > _rig.transform.position.y)
        {
            AddForce(_manager.Target.position.x > _rig.transform.position.x);
        }
        StartCoroutine(ForceDelay());
    }

    private void AddForce(bool isTargetOnRight)
    {
        _rig.velocity = Vector3.zero;
        _rig.AddForce(new Vector3(_data.ForceX * (isTargetOnRight ? 1 : -1), _data.ForceY, 0), ForceMode.Impulse);
        _isClicked = false;

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

    public void OnPlay()
    {
        _isNotStarted = false;
        _rig.useGravity = true;
        StartCoroutine(ForceDelay());
    }

    public void OnReset()
    {
        _rig.useGravity = false;
        _isOnRight = true;
        transform.position = new Vector3(_data.InitializePosX, _data.InitializePosY);
        _rig.velocity = Vector3.zero;
        _rig.angularVelocity = Vector3.zero;
        _rig.rotation = Quaternion.Euler(Vector3.zero);
        StopAllCoroutines();
    }
}
