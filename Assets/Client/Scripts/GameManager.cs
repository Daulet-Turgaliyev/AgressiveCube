using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts
{
	public class GameManager: MonoBehaviour
	{
		private CubeStateManager _cubeStateManager;

		[SerializeField] private Player _player;

		[SerializeField] private Button _createCubesButton;
		[SerializeField] private Button _moveCubesButton;
		[SerializeField] private Button _attackCubesButton;
		[SerializeField] private Button _aggressiveCubeButton;

		private CreateCubesState _createCubesState;
		
		private void Awake()
		{
			_cubeStateManager = new CubeStateManager();
		}

		public void CreateCubesActivate()
		{
			_createCubesButton.interactable = false;
			_createCubesState = new CreateCubesState();
			_cubeStateManager.SetState(_createCubesState);
		}
		
		public void MoveCubesActivate()
		{
			if (_createCubesState == null)
			{
				Debug.LogWarning($"{_createCubesState} not created");
				return;
			}
			
			_moveCubesButton.interactable = false;
			_cubeStateManager.SetState(new MoveCubesState(_createCubesState));
		}
		
		public void AttackCubesActivate()
		{
			if (_createCubesState == null)
			{
				Debug.LogWarning($"{_createCubesState} or {_player} not created");
				return;
			}
			
			_attackCubesButton.interactable = false;
			_cubeStateManager.SetState(new AttackCubesState(_createCubesState, _player));
		}
		
		public void AggressiveCubeActivate()
		{
			if (_createCubesState == null)
			{
				Debug.LogWarning($"{_createCubesState} not created");
				return;
			}
			
			_aggressiveCubeButton.interactable = false;
			_cubeStateManager.SetState(new AggressiveCubeState(_createCubesState));
		}
	}
}
