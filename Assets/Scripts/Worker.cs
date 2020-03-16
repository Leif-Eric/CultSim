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

    private bool _selected, _clicked;
    private State _currentState;

    private void OnMouseEnter()
    {
        Debug.Log("** selected");
        _selected = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("** exited");
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
       

        if(_clicked && Input.GetMouseButtonDown(1))
        {
            StartChangingRoom();
        }
    }

    private void StartChangingRoom()
    {
        _currentState = State.Changing;
        List<Transform> points = GameController.Instance.WayPointHandler.GetWayPoints(1,4);

        StartCoroutine(MoveToDestination(points));
    }

    private IEnumerator MoveToDestination(List<Transform> points)
    {
        int index = 0;
       
        while (index < points.Count - 1)
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
}
