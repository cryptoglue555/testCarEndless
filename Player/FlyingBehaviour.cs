using UnityEngine;
using System.Collections;

public class FlyingBehaviour : MonoBehaviour
{
    #region Public Members
    public float flyingHeight = 5.0f;
    public float verticalSpeed = 2.0f;
    #endregion

    #region Protected Members
    protected FlyingStates state;
    #endregion

    #region Public properties
    public enum FlyingStates
    {
        None,
        TakeOff,
        Flying,
        Landing
    }

    public bool IsFLying
    {
        get { return state != FlyingStates.None; }
    }
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        state = FlyingStates.None;
    }

    void FixedUpdate()
    {
        float dt = TimeManager.Instance.MasterSource.DeltaTime;

        switch (state)
        {
            case FlyingStates.TakeOff:
                UpdateTakeOff(dt);
                break;
            case FlyingStates.Flying:
                UpdateFlying(dt);
                break;
            case FlyingStates.Landing:
                UpdateLanding(dt);
                break;
        }
    }
    #endregion

    #region Functions
    void UpdateTakeOff(float dt)
    {
        float playerHeight = rigidbody.transform.position.y;
        playerHeight += dt * verticalSpeed;
        if (playerHeight > flyingHeight)
        {
            playerHeight = flyingHeight;
            state = FlyingStates.Flying;
        }
        rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, playerHeight, rigidbody.transform.position.z);
    }
    void UpdateFlying(float dt)
    {
        rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, flyingHeight, rigidbody.transform.position.z);
    }
    void UpdateLanding(float dt)
    {
        float playerHeight = rigidbody.transform.position.y;
        playerHeight -= dt * verticalSpeed;
        if (playerHeight < 0.0f)
        {
            playerHeight = 0.0f;
            state = FlyingStates.None;
        }
        rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, playerHeight, rigidbody.transform.position.z);
    }
    #endregion

    #region Messages
    void TestFlying()
    {
        if (state == FlyingStates.None)
            StartFlying();
        else if (state == FlyingStates.Flying)
            EndFlying();
    }
    void StartFlying()
    {
        state = FlyingStates.TakeOff;
    }
    void EndFlying()
    {
        state = FlyingStates.Landing;
    }
    #endregion
}
