using UnityEngine;

public class Menu : MonoBehaviour
{
    #region VARIAVEIS PARA CONTROLE DO MENU

    [SerializeField] private GameObject menu;

    private bool podeAbrir = true;

    #endregion

    #region METODO ABRIR OPÇÕES QUE AO CLICAR ELE ABRE E FECHA AS DICAS

    public void AbrirOpcoes ()
    {
        if (podeAbrir)
        {
            menu.SetActive(true);
            podeAbrir = false;
        }
        else if (podeAbrir == false)
        {
            menu.SetActive(false);
            podeAbrir = true;
        }
    }

    #endregion
}
