using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Blog.Blazor.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload/image")]
        public IActionResult Image(IFormFile file)
        {
            try
            {
                // Verifica se o arquivo é válido
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Arquivo inválido");
                }

                // Pasta onde os uploads serão armazenados
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                // Verifica se a pasta de uploads existe, se não, cria
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Gera um nome único para o arquivo
                var fileName = $"upload-{DateTime.Today.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                // Caminho completo do arquivo
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Salva o arquivo no servidor
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Retorna a URL do arquivo
                var url = Url.Content($"~/uploads/{fileName}");

                return Ok(new { Url = url });
            }
            catch (Exception ex)
            {
                // Retorna um código de erro 500 com a mensagem de exceção
                return StatusCode(500, ex.Message);
            }
        }
    }
}
