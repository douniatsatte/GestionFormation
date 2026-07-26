using System.Text;
using System.Text.Json;

namespace GestionFormation.Services
{
    public class AvatarService
    {
        private readonly string _apiKey;
        private readonly string _personaId;
        private readonly HttpClient _httpClient;

        public AvatarService(IConfiguration config, HttpClient httpClient)
        {
            _apiKey = Environment.GetEnvironmentVariable("ANAM_API_KEY") ?? config["AnamSettings:ApiKey"];
            _personaId = Environment.GetEnvironmentVariable("ANAM_PERSONA_ID") ?? config["AnamSettings:PersonaId"];
            _httpClient = httpClient;
        }

        public async Task<string> GenerateSessionTokenAsync(string contenuModule)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anam.ai/v1/auth/session-token");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            var body = new
            {
                personaConfig = new
                {
                    name = "Formateur IA",
                    avatarId = "30fa96d0-26c4-4e55-94a0-517025942e18",
                    avatarModel = "cara-4",
                    voiceId = "6bfbe25a-979d-40f3-a92b-5394170af54b",
                    llmId = "a7cf662c-2ace-4de1-a21e-ef0fbf144bb7",
                    systemPrompt = $"Tu es un formateur pédagogue qui s'exprime UNIQUEMENT en français. Voici le contenu du module à présenter à l'apprenant : {contenuModule}. Présente ce contenu de façon claire et réponds aux questions de l'apprenant sur ce sujet uniquement, toujours en français."
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur API Anam ({response.StatusCode}): {errorContent}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement.GetProperty("sessionToken").GetString();
        }
    }
}
