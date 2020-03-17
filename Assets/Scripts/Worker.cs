﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class Worker : MonoBehaviour
{
    public enum State
    {
        Working,
        Changing
    }

    private bool _selected, _clicked, _isPermanent;
    private State _currentState;
    private int _currentRoomIndex;
    private Animator _animator;
    //todo change sorting-order while moving to new destination and back
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentRoomIndex = 1;
    }

    private void OnMouseEnter()
    {
        _selected = true;
    }

    private void OnMouseExit()
    {
        _selected = false;   
    }

    private void Update()
    {
        if(_selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _clicked = true;
            }
        }
       

        if(_clicked && Input.GetMouseButtonDown(1) && _currentState != State.Changing)
        {
            GetClickedRoom();
        }
    }

    private void StartChangingRoom(int current, int target, Room r)
    {
        _currentState = State.Changing;
        List<Transform> points = GameController.Instance.WayPointHandler.GetWayPoints(current, target);

        StartCoroutine(MoveToDestination(points, r));
        _currentRoomIndex = target;
    }

    /// <summary>
    /// move to destinated room
    /// </summary>
    /// <param name="points">way-points to reach room</param>
    /// <returns>null</returns>
    private IEnumerator MoveToDestination(List<Transform> points, Room r)
    {
        int index = 0;
       
        while (index < points.Count)
        {
            transform.localPositionTransition(points[index].position, 2f);

            while (transform.position != points[index].position)
            {
                yield return new WaitForEndOfFrame();
            }
            index++;
        }

        _currentState = State.Working;

        WorkSpot workingSpot = r.GetWorkingSpot();

        if (workingSpot.DestinationWay.Count > 0)
        {
            StartCoroutine(MoveToDestinationSpot(workingSpot.DestinationWay));
        }
        else
        {
            workingSpot.IsFree = false;
            transform.localPositionTransition(workingSpot.SpotPosition.position, 1f);
        }

        yield return null;
    }

    /// <summary>
    /// move to destinated working spot
    /// </summary>
    /// <param name="points">waypoints to reach spot</param>
    /// <returns>null</returns>
    private IEnumerator MoveToDestinationSpot(List<Transform> points)
    {
        int index = 0;

        while (index < points.Count)
        {
            transform.localPositionTransition(points[index].position, 2f);

            while (transform.position != points[index].position)
            {
                yield return new WaitForEndOfFrame();
            }
            index++;
        }
        yield return null;
    }

    private void GetClickedRoom()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.transform.GetComponent<Room>() != null)
        {
            Room r = hit.transform.GetComponent<Room>();
            int targetRoom = r.RoomIndex;

            if(_currentRoomIndex != targetRoom && r.HasFreeSpot())
            {
                StartChangingRoom(_currentRoomIndex, targetRoom, r);
            }
        }
        else
        {
            _selected = _clicked = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(Input.mousePosition, Vector3.back);
    }
}
