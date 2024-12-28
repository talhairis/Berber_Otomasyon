//private const string apiKey = "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";  // API Anahtarınızı buraya ekleyin
//private const string apiUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-xl-refiner-1.0";
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class AIController : Controller
{
    private readonly HttpClient _httpClient;

    public AIController()
    {
        _httpClient = new HttpClient();
    }

    // View'e fotoğraf yükleme formunu gösteren action
    public IActionResult Index()
    {
        return View();
    }

    // Fotoğrafı base64'e çevirip API'ye gönderme
    [HttpPost]
    public async Task<IActionResult> UploadPhoto(IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
        {
            ViewBag.ErrorMessage = "Lütfen bir fotoğraf seçin.";
            return View();
        }

        // Fotoğrafı base64 formatına çevirme
        string base64Photo = await ConvertToBase64(photo);

        // Fotoğrafı AI modeline gönderme
        string apiUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-xl-refiner-1.0"; // Buraya modelin API URL'sini ekleyin.
        var result = await SendPhotoToAI(base64Photo, apiUrl);

        if (result != null)
        {
            // API yanıtını işleme (örneğin, bir görsel dönerse base64 string olarak)
            ViewBag.Image = result.Image; // Görseli view'e gönderiyoruz.
            return View();
        }
        else
        {
            ViewBag.ErrorMessage = "Bir hata oluştu, lütfen tekrar deneyin.";
            return View();
        }
    }

    // Base64 formatına dönüştürme
    private async Task<string> ConvertToBase64(IFormFile photo)
    {
        using (var memoryStream = new System.IO.MemoryStream())
        {
            await photo.CopyToAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    // AI modeline fotoğraf gönderme
    private async Task<AIResponse> SendPhotoToAI(string base64Photo, string apiUrl)
    {
        var requestData = new
        {
            photo = base64Photo
        };

        var jsonContent = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // API anahtarı (key) gerekiyorsa, bunu ekleyin
        var apiKey = "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"; // Anahtarınızı buraya ekleyin
        content.Headers.Add("Authorization", $"Bearer {apiKey}");

        try
        {
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AIResponse>(responseContent);
            }
            else
            {
                // Hata mesajını işleyebilirsiniz
                return null;
            }
        }
        catch (Exception ex)
        {
            // Hata durumunu işleyebilirsiniz
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}

