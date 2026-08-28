using UnityEngine;
using TMPro;

public class EfeitoTremor : MonoBehaviour, IEfeitoTexto
{
    public float intensidade = 2f;

    public void AplicarEfeito(TMP_TextInfo textInfo)
    {
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            Vector3 offset = new Vector3(Random.Range(-intensidade, intensidade),
                                         Random.Range(-intensidade, intensidade), 0);

            int matIndex = charInfo.materialReferenceIndex;
            int vertIndex = charInfo.vertexIndex;
            Vector3[] vertices = textInfo.meshInfo[matIndex].vertices;

            vertices[vertIndex + 0] += offset;
            vertices[vertIndex + 1] += offset;
            vertices[vertIndex + 2] += offset;
            vertices[vertIndex + 3] += offset;
        }
    }
}