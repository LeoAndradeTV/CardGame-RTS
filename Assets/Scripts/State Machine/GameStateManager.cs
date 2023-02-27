using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public Player player;

    public int activePlayerNumber = 0;

    public PhotonView photonView;

    public static GameStateManager instance;

    public GameStateAbstract currentState;
    public GameStateAbstract lastState;
    public AttackState attackState = new AttackState();
    public BuildState buildState = new BuildState();
    public BuyCardsState buyCardsState = new BuyCardsState();
    public EndTurnState endTurnState = new EndTurnState();
    public PlayCardsState playCardsState = new PlayCardsState();
    public StartTurnState startTurnState = new StartTurnState();
    public DrawCardsState drawCardsState = new DrawCardsState();
    public WaitingForTurnState waitingForTurnState = new WaitingForTurnState();

    public bool hasAttacked = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = PhotonNetwork.LocalPlayer;

        photonView = GetComponent<PhotonView>();

        currentState = waitingForTurnState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null) { return; }

        currentState.UpdateState(this);
    }
    
    public void SwitchState(GameStateAbstract state)
    {
        lastState = currentState;
        currentState = state;
        state.EnterState(this);
    }

    public GameStateAbstract GetLastState()
    {
        return lastState;    
    }
    [PunRPC]
    private void ChangeActivePlayer()
    {
        activePlayerNumber = (activePlayerNumber + 1) % PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log($"My number is {player.ActorNumber - 1} and the active player is {activePlayerNumber}");
    }

}
