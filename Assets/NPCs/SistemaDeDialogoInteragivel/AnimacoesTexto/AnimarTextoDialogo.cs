using System.Collections;
using UnityEngine;
using TMPro;

public class AnimarTexto : MonoBehaviour
{
    public float tempoEntreLetras = 0.05f;
    public TMP_Text textoTMP;

    // Lista de todos os efeitos (Tremor, Onda, Cor, etc) grudados neste GameObject
    private IEfeitoTexto[] efeitosAtivos;
    private Coroutine rotinaAnimacaoMesh;
    private Coroutine rotinaTexto;

    // Adicione estas duas variáveis no topo da classe AnimarTextoDialogo:
    private bool estaAtivo = false;
    private string mensagemAtual = "";

    public void IniciarAnimacao(string mensagem)
    {
        // Se já está ativo e é a mesma mensagem, ignora o comando para não reiniciar o texto
        if (estaAtivo && mensagemAtual == mensagem) return;

        estaAtivo = true;
        mensagemAtual = mensagem;

        if (rotinaTexto != null) StopCoroutine(rotinaTexto);
        rotinaTexto = StartCoroutine(DigitarLetras(mensagem));

        if (rotinaAnimacaoMesh == null)
            rotinaAnimacaoMesh = StartCoroutine(AnimarEfeitosVisuais());
    }

    public void DesativarAnimacao()
    {
        // Se já estiver desativado, ignora o comando
        if (!estaAtivo) return;

        estaAtivo = false;
        mensagemAtual = ""; // Limpa para permitir que a mesma mensagem seja ativada no futuro

        if (rotinaTexto != null) StopCoroutine(rotinaTexto);
        rotinaTexto = StartCoroutine(ApagarLetras());
    }

    private IEnumerator ApagarLetras()
    {
        // Apaga uma letra por vez de trás para frente
        while (textoTMP.text.Length > 0)
        {
            textoTMP.text = textoTMP.text.Substring(0, textoTMP.text.Length - 1);
            yield return new WaitForSeconds(tempoEntreLetras);
        }

        // 1. Para a atualização contínua dos efeitos visuais primeiro
        if (rotinaAnimacaoMesh != null)
        {
            StopCoroutine(rotinaAnimacaoMesh);
            rotinaAnimacaoMesh = null;
        }

        // 2. Garante que a string está vazia
        textoTMP.text = "";

        // 3. Força o TextMeshPro a apagar qualquer "letra fantasma" que sobrou na tela
        textoTMP.ClearMesh();
    }

    private void Awake()
    {
        // Procura todos os scripts neste objeto que possuem a interface IEfeitoTexto
        efeitosAtivos = GetComponents<IEfeitoTexto>();
    }

    public void MostrarTexto(string mensagem)
    {
        rotinaTexto = StartCoroutine(DigitarLetras(mensagem));

        if (rotinaAnimacaoMesh == null)
            rotinaAnimacaoMesh = StartCoroutine(AnimarEfeitosVisuais());
    }

    private IEnumerator DigitarLetras(string mensagem)
    {
        textoTMP.text = "";
        foreach (char letra in mensagem.ToCharArray())
        {
            textoTMP.text += letra;
            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }

    private IEnumerator AnimarEfeitosVisuais()
    {
        while (true)
        {
            textoTMP.ForceMeshUpdate();
            TMP_TextInfo textInfo = textoTMP.textInfo;

            // Aplica cada efeito da lista (você pode ter Tremor e Onda juntos!)
            foreach (var efeito in efeitosAtivos)
            {
                efeito.AplicarEfeito(textInfo);
            }

            // Atualiza a malha visual de uma vez só no final
            for (int i = 0; i < textInfo.materialCount; i++)
            {
                if (textInfo.meshInfo[i].mesh != null)
                {
                    textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    textoTMP.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }
            }

            // Atualiza os efeitos a cada frame
            yield return null;
        }
    }
}