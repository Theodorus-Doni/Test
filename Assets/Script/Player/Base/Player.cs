using UnityEngine;

public sealed class Player : MonoBehaviour
{
	[SerializeField]
	private PlayerModel _model;

	private void Awake() =>
		_model.InitModel();

	private void FixedUpdate()
	{
		float inputX = Input.GetAxis(GameManager.GetInstance().InputConfig.Horizontal);

		transform.position = new Vector3(NewPlayerXPosition(inputX), transform.position.x, transform.position.z);
	}

	private float NewPlayerXPosition(float inputX) =>
		transform.position.x + inputX * _model.Speed * Time.fixedDeltaTime;
}
