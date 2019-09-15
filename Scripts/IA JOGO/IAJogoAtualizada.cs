using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IAJogoAtualizada : MonoBehaviour
{
    #region VARIAVEIS GLOBAIS, PRINCIPAIS PARA O FUNCIONAMENTO DO SCRIPT

        [SerializeField] private List<Transform> objetosPosicionais = new List<Transform> ();

        [SerializeField] private List<GameObject> objetosPrincipais = new List<GameObject> ();

        [SerializeField] private GameObject personagemPrincipal;

        [SerializeField] private int dificuldadeJogadorAtirar = 8;

        private int[] matriz = new int[16];

        private GameObject prefab;

        public static int geracoes, tiros, matouInimigo, pegouOuro;
        private bool matouInimigoCompletou = false;

        public BancoDeDadosPlayer bancoDeDados;

        #region CRONOMETRO DE CONTAGEM PARA CADA MOVIMENTO DO JOGADOR E MORTE

            private float contador;

            public static float ContadorMorte, tempoLimite = 0.9f;
            
            private int posicaoAtual;

        #endregion

    #endregion

    #region METODO START, NELE É CARREGADO TODO O JOGO, SEM A UTILIZAÇÃO DO METODO UPDATE

    private void Awake ()
    {
        MotorDoJogo();
    }

    #endregion

    #region METODO UPDATE QUE SERVE APENAS PARA PASSAR AS FAZES E TESTAR BUGS

    private void Update ()
    {
        TempoMorte();
        IA();
    }

    #endregion

    #region POSICIONANDO CORRETAMENTE CADA OBJETO EM SUAS RESPECTIVAS POSICOES

    private void MotorDoJogo ()
    {
        AdicionandoBancoDeDadosConstantemente();
        Matriz();
        PersonagemPrincipal();
    }

    #endregion

    #region ADICIONANDO O BANCO DE DADOS DO PLAYER PARA QUE SETE SEMPRE NO AWEKE O OBJETO GAMEMANAGER

    private void AdicionandoBancoDeDadosConstantemente()
    {
        bancoDeDados = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BancoDeDadosPlayer>();
    }
    
    #endregion

    #region GERANDO A MATRIZ RESPONSAVEL POR GARDAR AS INFORMAÇÕES

    private void Matriz ()
    {
        for(int i = 0; i < matriz.Length; i++)
        {
            matriz[i] = i;

            PosicionamentoDosObjetos(matriz[i]);            
        }
    }

    #endregion

    #region POSICIONAMENTO DOS OBJETOS

    private void PosicionamentoDosObjetos (int i)
    {
        if (i == 0) /* ========== BURACO ========== */
        {
            SpawningOtimizado(objetosPrincipais[1], objetosPosicionais[i], 3f);
        }
        else if (i == 1) /* ========== BRIZA ========== */
        {
            SpawningOtimizado(objetosPrincipais[0], objetosPosicionais[i], 3f);
        }
        else if(i == 2) /* ========== PODE ANDAR ==========*/
        {
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 3f);
        }
        else if (i == 3) /* ========== PODE ANDAR ==========*/
        {
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 3f);
        }
        else if (i == 4) /* ========== BRIZA ========== */
        {
            SpawningOtimizado(objetosPrincipais[0], objetosPosicionais[i], 3f);
        }
        else if (i == 5) /* ========== FEDO ========== */
        {
            SpawningOtimizado(objetosPrincipais[2], objetosPosicionais[i], 3f);
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 4f);
        }
        else if (i == 6) /* ========== PODE ANDAR ==========*/
        {
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 3f);
        }
        else if (i == 7) /* ========== PODE ANDAR ==========*/
        {
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 3f);
        }
        else if (i == 8) /* ========== FEDO ========== */
        {
            SpawningOtimizado(objetosPrincipais[2], objetosPosicionais[i], 3f);
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 4f);
        }
        else if (i == 9) /* ========== INIMIGO ========== */
        {
            SpawningOtimizado(objetosPrincipais[5], objetosPosicionais[i], 3f);
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 4f);
        }
        else if (i == 10) /* ========== FEDO ========== */
        {
            SpawningOtimizado(objetosPrincipais[2], objetosPosicionais[i], 3f);
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 4f);
        }
        else if (i == 11) /* ========== BRIZA ========== */
        {
            SpawningOtimizado(objetosPrincipais[0], objetosPosicionais[i], 3f);
        }
        else if (i == 12) /* ========== OURO ==========*/
        {
            SpawningOtimizado(objetosPrincipais[3], objetosPosicionais[i], 3f);
        }
        else if (i == 13) /* ========== FEDO ========== */
        {
            SpawningOtimizado(objetosPrincipais[2], objetosPosicionais[i], 3f);
            SpawningOtimizado(objetosPrincipais[4], objetosPosicionais[i], 4f);
        }
        else if (i == 14) /* ========== BRIZA ========== */
        {
            SpawningOtimizado(objetosPrincipais[0], objetosPosicionais[i], 3f);
        }
        else if (i == 15) /* ========== BURACO ========== */
        {
            SpawningOtimizado(objetosPrincipais[1], objetosPosicionais[i], 3f);
        }
    }

    #endregion

    #region SPAWNANDO O PERSONAGEM PRINCIPAL
    
    private void PersonagemPrincipal()
    {
        for (int i = 0; i < matriz.Length; i++)
        {
            if (matriz[i] == 3)
            {
                posicaoAtual = matriz[i];
                SpawningOtimizado(personagemPrincipal, objetosPosicionais[i], 2f);
            }
        }
    }

    #endregion

    #region TEMPO DO JOGO

    private void TempoMorte()
    {
        ContadorMorte += Time.deltaTime;
    }

    #endregion

    #region IA PERSONAGEM PRINCIPAL

    private void IA ()
    {
        contador += Time.deltaTime;

        if (contador >= tempoLimite)
        {
            contador = 0;

            if (posicaoAtual == 0) /* ========== BURACO ========== */
            {
                bancoDeDados.PassarInteracao(false, false, true, false, false);
                geracoes += 1;

                DestroiPlayer();
            }
            else if (posicaoAtual == 1) /* ========== BRIZA ========== */
            {
                int numeroRandomico = Random.Range(0, 4);    
                               
                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 0 && bancoDeDados.ObterInteracao(matriz[i], 1).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(false, true, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 2)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 5)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 2) /* ========== PODE ANDAR ========== */
            {
                int numeroRandomico = Random.Range(0, 4);
                
                if(numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {    
                        if (matriz[i] == 1)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {    
                        if (matriz[i] == 6)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if(numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 3)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 3) /* ========== PODE ANDAR ========== */
            {
                int numeroRandomico = Random.Range(0, 3);

                if(numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 2)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 7)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 4) /* ========== BRIZA ========== */
            {                
                int numeroRandomico = Random.Range(0, 4);

                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 0 && bancoDeDados.ObterInteracao(matriz[i], 1).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(false, true, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 5)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 8)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 5) /* ========== FEDO ========== */
            {   
                int numeroRandomico = Random.Range(0, 5);

                if (numeroRandomico == 0)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 1)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");
                                    matouInimigoCompletou = true;
                                }

                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else 
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 1)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 4)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 4)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 6)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else 
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 6)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 3)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 9 && bancoDeDados.ObterInteracao(matriz[i], 2).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 6) /* ========== PODE ANDAR ========== */
            {
                int numeroRandomico = Random.Range(0, 5);

                if(numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 2)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 5)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 7)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 3)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 10)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 7) /* ========== PODE ANDAR ========== */
            {
                int numeroRandomico = Random.Range(0, 4);

                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 3)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 6)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 11)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 8) /* ========== FEDO ========== */
            {
                int numeroRandomico = Random.Range(0, 2);

                if (numeroRandomico == 0)
                {
                    if (matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 4)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");
                                    
                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 4)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 9 && bancoDeDados.ObterInteracao(matriz[i], 2).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(true, false, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 12)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, true);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;
                                    
                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 12)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, true);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
            }
            else if (posicaoAtual == 9) /* ========== INIMIGO ========== */
            {
                
                if(matouInimigoCompletou == false)
                {
                    bancoDeDados.PassarInteracao(false, false, false, true, false);
                    geracoes += 1;
                    
                    matouInimigoCompletou = true;
                    DestroiPlayer();
                }
                else
                {
                    int numeroRandomico = Random.Range(0, 3);

                    if(numeroRandomico == 0)
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 8)
                            {
                                posicaoAtual = matriz[i];
                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else if (numeroRandomico == 1)
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 10)
                            {
                                posicaoAtual = matriz[i];
                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else if (numeroRandomico == 2)
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 5)
                            {
                                posicaoAtual = matriz[i];
                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else if (numeroRandomico == 3)
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 3)
                            {
                                posicaoAtual = matriz[i];
                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
            }
            else if (posicaoAtual == 10) /* ========== FEDO ========== */
            {
                int numeroRandomico = Random.Range(0, 5);

                if (numeroRandomico == 0)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 6)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else 
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 6)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {   
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 9 && bancoDeDados.ObterInteracao(matriz[i], 2).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(true, false, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }   
                else if (numeroRandomico == 2)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 11)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 11)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 3)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 14)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 14)
                            {
                                posicaoAtual = matriz[i];
                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
            }
            else if (posicaoAtual == 11) /* ========== BRIZA ========== */
            {
                int numeroRandomico = Random.Range(0, 3);

                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 7)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 10)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 15 && bancoDeDados.ObterInteracao(matriz[i], 1).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(false, true, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 12) /* ========== OURO ==========*/
            {
                bancoDeDados.PassarInteracao(false, false, false, false, true);
                geracoes += 1;
                pegouOuro += 1;
                
                DestroiPlayer();
            }
            else if (posicaoAtual == 13) /* ========== FEDO ========== */
            {
                int numeroRandomico = Random.Range(0, 4);

                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 9 && bancoDeDados.ObterInteracao(matriz[i], 2).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(true, false, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 1)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 12)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, true);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 12)
                            {
                                posicaoAtual = matriz[i];

                                //bancoDeDados.PassarInteracao(true, false, false, false, true);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    if(matouInimigoCompletou == false)
                    {
                        int randomDoTiro = Random.Range(0, dificuldadeJogadorAtirar); //Variavel randomica para o tiro do player, 0 - acertou, os restantes errou

                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 14)
                            {
                                posicaoAtual = matriz[i];

                                bancoDeDados.PassarInteracao(true, false, false, false, false);

                                if (randomDoTiro == 0 && bancoDeDados.ObterInteracao(matriz[i], 4).Equals(true))
                                {
                                    tiros += 1;
                                    matouInimigo += 1;

                                    DestroiObjetos("Fedo");
                                    DestroiObjetos("Inimigo");

                                    matouInimigoCompletou = true;
                                }
                                if (randomDoTiro != 0)
                                {
                                    geracoes += 1;
                                    tiros += 1;

                                    DestroiPlayer();
                                }

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < matriz.Length; i++)
                        {
                            if (matriz[i] == 14)
                            {
                                posicaoAtual = matriz[i];

                                //bancoDeDados.PassarInteracao(true, false, false, false, false);

                                GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                            }
                        }
                    }
                }
            }
            else if (posicaoAtual == 14) /* ========== BRIZA ========== */
            {
                int numeroRandomico = Random.Range(0, 4);

                if (numeroRandomico == 0)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 10)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }   
                    }
                }
                else if (numeroRandomico == 1)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {    
                        if (matriz[i] == 13)
                        {
                            posicaoAtual = matriz[i];

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
                else if (numeroRandomico == 2)
                {
                    for (int i = 0; i < matriz.Length; i++)
                    {
                        if (matriz[i] == 15 && bancoDeDados.ObterInteracao(matriz[i], 1).Equals(false))
                        {
                            posicaoAtual = matriz[i];

                            bancoDeDados.PassarInteracao(false, true, false, false, false);

                            GameObject.FindWithTag("Player").transform.position = new Vector3(objetosPosicionais[i].transform.position.x, objetosPosicionais[i].transform.position.y, 2f);
                        }
                    }
                }
            }
            else if (posicaoAtual == 15) /* ========== BURACO ========== */
            {
                bancoDeDados.PassarInteracao(false, false, true, false, false);
                geracoes += 1;

                DestroiPlayer();
            }
        }
    }
    
    #endregion

    #region OTIMIZAÇÃO DE SPAWNING DE OBJETOS

    private void SpawningOtimizado (GameObject _prefab, Transform _posicoes, float z)
    {
        prefab = Instantiate (_prefab, new Vector3 (_posicoes.position.x, _posicoes.position.y, z), Quaternion.identity);
    }

    #endregion

    #region DESTROI O PLAYER

    private void DestroiPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));
        SceneManager.LoadScene("cenaJogo");
    }

    #endregion

    #region DESTROI OBJETOS

    private void DestroiObjetos (string tagObjeto)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tagObjeto);

        foreach (var item in objetos)
        {
            Destroy(item);
        } 
    }

    #endregion
}