using System.Collections;
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

    private void Start()
    {
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

    private void StartChangingRoom(int current, int target)
    {
        _currentState = State.Changing;
        List<Transform> points = GameController.Instance.WayPointHandler.GetWayPoints(current, target);

        StartCoroutine(MoveToDestination(points));
        _currentRoomIndex = target;
    }

    private IEnumerator MoveToDestination(List<Transform> points)
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

        yield return null;
    }

    private void GetClickedRoom()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.transform.GetComponent<Room>() != null)
        {
            int targetRoom = hit.transform.GetComponent<Room>().RoomIndex;

            if(_currentRoomIndex != targetRoom)
            {
                StartChangingRoom(_currentRoomIndex, targetRoom);
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
