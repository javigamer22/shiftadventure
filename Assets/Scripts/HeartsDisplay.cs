using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsDisplay : MonoBehaviour
{
    private PlayerMove2 player;
    private List<Image> hearts = new List<Image>();
    public int heartSize = 32;  // Tamaño de los corazones en píxeles
    public Color heartColor = Color.red; // Color del corazón
    private Canvas canvas; // Canvas creado dinámicamente

    void Start()
    {
        //player = FindObjectOfType<PlayerMove2>();
        player = FindPlayer();
        CreateCanvas();
        UpdateHearts();
    }

    void Update()
    {
        if (player != null &&  player.getVidas() != hearts.Count)
        {
            UpdateHearts();
        }
    }

    PlayerMove2 FindPlayer()
    {
        // Buscar primero en DontDestroyOnLoad
        PlayerMove2[] allPlayers = Object.FindObjectsOfType<PlayerMove2>();
        foreach (PlayerMove2 p in allPlayers)
        {
            if (p.gameObject.scene.name == "DontDestroyOnLoad")
            {
                return p;
            }
        }

        // Si no se encuentra en DontDestroyOnLoad, buscar en la escena actual
        PlayerMove2 playerInstance = FindObjectOfType<PlayerMove2>();
        if (playerInstance == null)
        {
            Debug.LogError("PlayerMove2 not found!");
        }
        return playerInstance;
    }

    void CreateCanvas()
    {
        GameObject canvasGO = new GameObject("HeartsCanvas");
        canvasGO.transform.SetParent(Camera.main.transform, false);

        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = canvasGO.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        canvasGO.AddComponent<GraphicRaycaster>();
    }

    void UpdateHearts()
    {
        // Destruir los corazones actuales
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        // Crear nuevos corazones
        for (int i = 0; i < player.getVidas(); i++)
        {
            GameObject heartGO = new GameObject("Heart");
            heartGO.transform.SetParent(canvas.transform, false);

            Image heartImage = heartGO.AddComponent<Image>();
            heartImage.sprite = CreateHeartSprite();
            heartImage.color = heartColor;

            RectTransform rectTransform = heartImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(heartSize, heartSize);
            rectTransform.anchorMin = new Vector2(0, 1); // Ancla al borde superior izquierdo
            rectTransform.anchorMax = new Vector2(0, 1); // Ancla al borde superior izquierdo
            rectTransform.pivot = new Vector2(0, 1); // Pivot en la esquina superior izquierda
            rectTransform.anchoredPosition = new Vector2(50 + i * (heartSize + 10), -50); // Ajusta la posición de los corazones

            hearts.Add(heartImage);
        }
    }

    Sprite CreateHeartSprite()
    {
        Texture2D texture = new Texture2D(heartSize, heartSize, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[heartSize * heartSize];

        // Rellena todos los píxeles con transparencia
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }

        // Dibujar un corazón
        int width = heartSize;
        int height = heartSize;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float normalizedX = (x - width / 2.0f) / (width / 2.0f);
                float normalizedY = (y - height / 2.0f) / (height / 2.0f);
                float value = Mathf.Pow(normalizedX * normalizedX + normalizedY * normalizedY - 0.3f, 3) - normalizedX * normalizedX * normalizedY * normalizedY * normalizedY;

                if (value <= 0)
                {
                    pixels[y * width + x] = heartColor;
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        return Sprite.Create(texture, new Rect(0.0f, 0.0f, heartSize, heartSize), new Vector2(0.5f, 0.5f), heartSize);
    }
}






