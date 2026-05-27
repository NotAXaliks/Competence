using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace Desktop.Services;

public static class ApiService
{
    public static HttpClient HttpClient = new();
    public static string BaseUrl = "http://localhost:5299/api/";
    public static string Token = "";

    public static async Task<ApiResponse<T?>> Request<T>(HttpMethod method, string url, object? data = null)
    {
        try
        {
            var request = new HttpRequestMessage(method, BaseUrl + url);

            if (Token != "") request.Headers.Add("Authorization", $"Bearer {Token}");
            if (data != null) request.Content = JsonContent.Create(data);

            var response = await HttpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            return new ApiResponse<T?>(default, "Ошибка запроса", 500);
        }
    }
}