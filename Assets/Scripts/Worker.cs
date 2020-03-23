using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class Worker : MonoBehaviour
{
    private const int DefaultSorting = 4;
    private const int MoveSorting = 2;

    private ParticleSystem _particles;

    //needed for pooling check
    private bool _isActiveWorker;
    public enum State
    {
        Waiting,
        Working,
        Changing
    }

    public int RoomIndex { get { return _currentRoomIndex; } }
    public int WorkerType { get; set; }

    private bool _selected, _clicked, _isPermanent;
    private State _currentState;
    private int _currentRoomIndex;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private WorkSpot _currentWorkSpot;

    public bool IsActivePool 
    {
        get{ return _isActiveWorker; }
        set 
        {
            _isActiveWorker = value;
            _currentRoomIndex = -1;
            gameObject.SetActive(value); 
        } 
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particles = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer.sortingOrder = DefaultSorting;
        _currentState = State.Waiting;
        _selected = false;
        _particles.Stop();
    }

    private void OnMouseEnter()
    {
        _selected = true;
        _particles.Play();
    }

    private void OnMouseExit()
    {
        _selected = false;
        _particles.Stop();
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
            Room r = GetClickedRoom();

            if( r != null)
            {

                WorkSpot workingSpot = r.GetWorkingSpot();
                workingSpot.IsFree = false;
                if (_currentState == State.Waiting)
                {
                    StartChangingRoom(0, r.RoomIndex, r, true, workingSpot);
                }
                if (_currentState == State.Working)
                {
                    Panel data = UpgradeManager.Instance.GetRoomData(_currentRoomIndex);
                    if(data != null)
                    {
                        data.RemoveWorker();
                    }
                    UpgradeManager.Instance.GetRoomData(_currentRoomIndex).RemoveWorker();

                    if (_currentWorkSpot != null && _currentWorkSpot.DestinationWay.Count > 0)
                    {
                        _currentState = State.Changing;
                        _spriteRenderer.sortingOrder = MoveSorting;
                        StartCoroutine(MoveToDestination(GameController.Instance.WayPointHandler.GetWayFromOutside(r.RoomIndex), r, workingSpot));
                        _currentRoomIndex = r.RoomIndex;
                    }
                    else
                    {
                        StartChangingRoom(_currentRoomIndex, r.RoomIndex, r, false, workingSpot);
                    }
                }
            }
        }
    }

    private void StartChangingRoom(int current, int target, Room r, bool isFirstPlace, WorkSpot targetSpot)
    {
        _currentState = State.Changing;
        _spriteRenderer.sortingOrder = MoveSorting;
        List<Transform> points = GameController.Instance.WayPointHandler.GetWayPoints(current, target, isFirstPlace);

        StartCoroutine(MoveToDestination(points, r, targetSpot));
        _currentRoomIndex = target;
    }

    /// <summary>
    /// move to destinated room
    /// </summary>
    /// <param name="points">way-points to reach room</param>
    /// <returns>null</returns>
    private IEnumerator MoveToDestination(List<Transform> points, Room r, WorkSpot workSpot)
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
        UpgradeManager.Instance.GetRoomData(_currentRoomIndex).AddWorker();

        _spriteRenderer.sortingOrder = DefaultSorting;
        if (workSpot.DestinationWay.Count > 0)
        {
            StartCoroutine(MoveToDestinationSpot(workSpot.DestinationWay));
        }
        else
        {
            transform.localPositionTransition(workSpot.SpotPosition.position, 1f);
        }

        _currentWorkSpot = workSpot;

        _clicked = false;

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

    private Room GetClickedRoom()
    {
        Room r = null;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.transform.GetComponent<Room>() != null)
        {
            r = hit.transform.GetComponent<Room>();
            int targetRoom = r.RoomIndex;

            if(_currentRoomIndex != targetRoom && r.HasFreeSpot())
            {
                return r;
            }

            return null;
        }
        else
        {
            _selected = _clicked = false;
        }
        return r;
    }
}
