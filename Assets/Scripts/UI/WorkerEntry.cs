using UnityEngine;
using UnityEngine.UI;

public class WorkerEntry : MonoBehaviour
{
    public enum State
    {
        Occupied = 0,
        Locked = 1,
        Free = 2
    }

    public Sprite Occupied, Locked, Free;
    public Image BackgroundSprite;
    public GameObject Worker;

    private State _currentState;

    public State CurrentState { get { return _currentState; } }

    public void Init(State state)
    {
        ChangeState(state);
    }

    public virtual void ChangeState(State target)
    {
        switch (target)
        {
            case State.Occupied:
                BackgroundSprite.sprite = Occupied;
                break;
            case State.Locked:
                BackgroundSprite.sprite = Locked;
                break;
            case State.Free:
                BackgroundSprite.sprite = Free;
                break;
        }

        _currentState = target;
        SetWorker(target == State.Occupied);
    }

    public void SetWorker(bool active)
    {
        Worker.SetActive(active);
    }
}
