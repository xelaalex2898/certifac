using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Certifac.Controllers
{
    public class ProcessDocument : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProcessDocument(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDatosDesdeApi()
        {
            
            string baseUrl = _configuration["baseUrl"]+ "ProcessDocument";

            try
            {
                HttpResponseMessage respuesta = await _httpClient.GetAsync(baseUrl);
                if (respuesta.IsSuccessStatusCode)
                {
                    string json = await respuesta.Content.ReadAsStringAsync();
                    return Ok("Solicitud exitosa: " + json);
                }
                else
                {
                    return StatusCode((int)respuesta.StatusCode, "Error al realizar la solicitud");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Error de comunicación con la API: " + ex.Message);
            }
        }
    }
}
