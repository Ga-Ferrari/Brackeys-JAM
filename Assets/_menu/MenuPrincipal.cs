using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Painéis do Menu")]
    public GameObject painelPrincipal;
    public GameObject painelOpcoes;

    [Header("Configurações de Áudio")]
    public AudioMixer audioMixer;

    [Header("Botões Iniciais (Controle/Teclado)")]
    public GameObject botaoPrimeiroPrincipal;
    public GameObject botaoPrimeiroOpcoes;

    void Start()
    {
        // Garante que o menu inicie focado no botão certo para navegação por controle
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botaoPrimeiroPrincipal);
    }

    public void IniciarJogo()
    {
        // Carrega a próxima cena na fila do Build Settings (Cena 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FecharJogo()
    {
        Application.Quit();
        Debug.Log("O jogo foi fechado!");
    }

    public void AbrirOpcoes()
    {
        painelPrincipal.SetActive(false);
        painelOpcoes.SetActive(true);
        Debug.Log("Abrindo menu de opções");
        
        StartCoroutine(FocarBotaoAtrasado(botaoPrimeiroOpcoes));
    }

    public void FecharOpcoes()
    {
        painelPrincipal.SetActive(true);
        painelOpcoes.SetActive(false);
        Debug.Log("Voltando ao menu principal");
        
        StartCoroutine(FocarBotaoAtrasado(botaoPrimeiroPrincipal));
    }

    public void MudarVolume(float valorDoSlider)
    {
        // O Mathf.Max impede que o valor seja exatamente 0, evitando o erro de -Infinity no Log10
        float valorSeguro = Mathf.Max(valorDoSlider, 0.0001f);
        float volumeEmDecibeis = Mathf.Log10(valorSeguro) * 20;
        
        audioMixer.SetFloat("VolumeMaster", volumeEmDecibeis);
    }

    private IEnumerator FocarBotaoAtrasado(GameObject botaoAlvo)
    {
        // 1. Limpa o foco atual para evitar conflitos
        EventSystem.current.SetSelectedGameObject(null);
        
        // 2. O Segredo: Pausa a função e espera o próximo frame da Unity acontecer
        yield return null; 
        
        // 3. Agora que o botão/painel existe na tela, o controle foca nele com sucesso
        EventSystem.current.SetSelectedGameObject(botaoAlvo);
    }
}