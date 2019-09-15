using UnityEngine;
using UnityEngine.UI;

public class UIinterafaceJogador : MonoBehaviour 
{
    #region VARIAVEIS DE AMBIENTE

    [SerializeField] private Text geracoes, tiros, matouInimigo, pegouOuros, tempoJogo;

	#endregion

	#region METODOS PARA CONTROLE DO TEXTO

	private void Update () 
	{
        geracoes.text = " GERAÇÕES : " + IAJogoAtualizada.geracoes.ToString();
        tiros.text = " ATIROU : " + IAJogoAtualizada.tiros.ToString();
        matouInimigo.text = " INIMIGO ABATIDO : " + IAJogoAtualizada.matouInimigo.ToString();
        pegouOuros.text = " PEGOU OURO : " + IAJogoAtualizada.pegouOuro.ToString();
        tempoJogo.text = " TEMPO JOGO : " + ((int)IAJogoAtualizada.ContadorMorte + 1).ToString();
    }

	#endregion
}
