using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuIniciar : MonoBehaviour 
{
	#region DEFINIÇÕES DAS VARIAVEIS PARA O DROPDOWN
	
	[SerializeField] private Dropdown dropdown;
	
	#endregion

	#region METODO START QUE EXECUTA O METODO ESCOLHER VELOCIDADE

	private void Start()
	{
		EscolherVelocidade();
	}

	#endregion

	#region METODO ESCOLHER VELOCIDADE

	private void EscolherVelocidade()
	{
		List<string> velocidades = new List <string> () {"LENTA", "MEDIA", "RAPIDA"};

		dropdown.AddOptions(velocidades);
	}

	#endregion

	#region METODO PASSAR DE FASE

	public void PassarDeFase()
	{
		SceneManager.LoadScene("cenaJogo");
	}

	#endregion

	#region METODO SETAR VELOCIDADE

	public void SetarVelocidade ()
	{
		if (dropdown.value == 1)
		{
			IAJogoAtualizada.tempoLimite = 0.4f;
		}
		else if (dropdown.value == 2)
		{
			IAJogoAtualizada.tempoLimite = 0.1f;
		}
		else
		{
			IAJogoAtualizada.tempoLimite = 0.9f;
		}
	}

	#endregion
}
