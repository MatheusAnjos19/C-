using System.Collections.Generic;
using UnityEngine;

public class BancoDeDadosPlayer : MonoBehaviour 
{
    #region VARIAVEL LISTA DE INFORMAÇÕE PASSAR, QUE ARMAZENA TODAS AS JOGADAS DO PLAYER

    public List<DadosPlayer> InformacoesPassar = new List<DadosPlayer>();

    #endregion

    #region METODO AWAKE PARA PERMANECER O GAMEMANAGER ENTRE CENAS

    private void Awake () 
	{
		GameObject [] objs = GameObject.FindGameObjectsWithTag ("GameManager"); 

        if (objs.Length> 1)
        {
            Destroy (this.gameObject);
        } 

		DontDestroyOnLoad (this.gameObject);
	}

    #endregion

    #region METODO PASSAR INTERAÇÃO QUE RECEBE DO SCRIPT IA JOGO ATUALIZADO AS INFORMAÇÕES PARA ARMAZENAR

    public void PassarInteracao(bool _podeAtacar, bool _temBuraco, bool _buraco, bool _inimigo, bool _pegouOuro)
    {
        InformacoesPassar.Add (new DadosPlayer(_podeAtacar, _temBuraco, _buraco, _inimigo, _pegouOuro));
    }

    #endregion

    #region METODO OBTER INTERAÇÃO, QUE OBTEM ATRAVEZ DE ESCOLHA E RETORNA VERDADEIRO OU FALSO PARA QUE AQUELA INFORMAÇÃO SEJA VERDADEIRA OU NÃO

    public bool ObterInteracao (int posicao, int variavelControle)
    {
        if(variavelControle == 1) /*CAIU NO BURACO (BURACO 1 E BURACO 2)*/
        {
            for(int i = 0; i < InformacoesPassar.Count; i++)
            {
                if (posicao == 0 && InformacoesPassar[i].buraco == true)
                {
                    return true;
                }
                else if(posicao == 15 && InformacoesPassar[i].buraco == true)
                {
                    return true;
                }
            }
        }

        if (variavelControle == 2) /*MORREU PARA O INIMIGO*/
        {
            for (int i = 0; i < InformacoesPassar.Count; i++)
            {
                if (posicao == 9 && InformacoesPassar[i].inimigo == true)
                {
                    return true;
                }
            }
        }

        if (variavelControle == 3) /*TEM BURACO NAS IMEDIAÇÕES*/
        {
            for (int i = 0; i < InformacoesPassar.Count; i++)
            {
                if (InformacoesPassar[i].temBuraco == true)
                {
                    return true;
                }
            }
        }

        if (variavelControle == 4) /*PODE ATACAR O INIMIGO*/
        {
            for (int i = 0; i < InformacoesPassar.Count; i++)
            {
                if (InformacoesPassar[i].podeAtacar == true)
                {
                    return true;
                }
            }
        }

        if (variavelControle == 5) /*PEGOU OURO*/
        {
            for (int i = 0; i < InformacoesPassar.Count; i++)
            {
                if (InformacoesPassar[i].pegouOuro == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}

#region CLASSE DADOS PLAYER, QUE POSSUI OS DADOS DO JOGADOR NECESSARIO PARA QUE ELE FIQUE INTELIGENTE

public class DadosPlayer
{
    public bool podeAtacar { get; set; }

    public bool temBuraco { get; set; }

    public bool buraco { get; set; }

    public bool inimigo { get; set; }

    public bool pegouOuro { get; set; }

    public DadosPlayer (bool _podeAtacar, bool _temBuraco, bool _buraco, bool _inimigo, bool _pegouOuro)
    {
        podeAtacar = _podeAtacar;
        temBuraco = _temBuraco;
        buraco = _buraco;
        inimigo = _inimigo;
        pegouOuro = _pegouOuro;
    }
}

#endregion