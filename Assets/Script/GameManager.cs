using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    #region Singelton
    private static GameManager _instance;

    public static GameManager GetInstance() => _instance;

    public void InitInstance()
    {
        if (_instance is null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }
    #endregion

    public readonly TagConfig TagConfig = new TagConfig();

    public readonly InputConfig InputConfig = new InputConfig();

    private void Awake()
    {
        InitInstance();
    }

}