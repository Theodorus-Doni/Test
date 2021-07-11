using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField]
    private Player _mainPlayer;

    public Vector3 GetMainPlayerPosition() =>
        _mainPlayer.transform.position;
}
