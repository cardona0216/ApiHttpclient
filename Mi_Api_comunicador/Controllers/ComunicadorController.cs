using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Mi_Api_comunicador.Models;

namespace Mi_Api_comunicador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunicadorController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public ComunicadorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClientFactory = httpClientFactory;

        }

        [HttpGet]

        public async Task<IActionResult> ObtenerDatosDeApiExistente()
        {
            try
            {
                // Definir la URL de la API existente
                string apiUrl = "https://localhost:7083/api/mascota";

                // Realizar una solicitud GET
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Verificar si la solicitud fue exitosa (código de estado 200)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta en formato JSON
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    // Devolver un resultado de error si la solicitud no fue exitosa
                    return StatusCode((int)response.StatusCode, $"Error al obtener datos de la API existente. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un resultado de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerdatoId(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"https://localhost:7083/api/mascota/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error al obtener datos. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDatos(int id,Mascota mascota)
        {
            try
            {
               

                var httpClient = _httpClientFactory.CreateClient();
                var url = $"https://localhost:7083/api/mascota/{id}";

                // Serializa el modeloActualizar a JSON y envía la solicitud PUT
                var content = new StringContent(JsonConvert.SerializeObject(mascota), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error al actualizar datos. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> crearNuevo(Mascota mascota)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var url = "https://localhost:7083/api/mascota";
                var content = new StringContent(JsonConvert.SerializeObject(mascota), Encoding.UTF8, "application/json");
                var respuesta = await httpClient.PostAsync(url, content);

                if (respuesta.IsSuccessStatusCode)
                {
                    var respuestaData = await respuesta.Content.ReadAsStringAsync();
                    return Ok(respuestaData);
                }
                else 
                {
                    return StatusCode((int)respuesta.StatusCode, $"Error al crear nuevo dato. Código de estado: {respuesta.StatusCode}");
                }
            
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");

            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> eliminar(int id)
        {
            try
            {
                var httClient = _httpClientFactory.CreateClient();
                var url = $"https://localhost:7083/api/mascota/{id}";

                var respuesta = await httClient.DeleteAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    return NoContent();
                }
                else {
                    return StatusCode((int)respuesta.StatusCode, $"Error al eliminar dato. Código de estado: {respuesta.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



    }
}
