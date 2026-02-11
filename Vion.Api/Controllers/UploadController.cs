using Microsoft.AspNetCore.Mvc;

namespace Vion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("produto")]
        public async Task<IActionResult> UploadProdutoImage(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            // Validar extensão
            var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
            if (!extensoesPermitidas.Contains(ext))
                return BadRequest("Formato de imagem inválido.");

            // Caminho para salvar (Tentando salvar na pasta do projeto Web para que as imagens fiquem visíveis lá)
            // A API roda em Vion.Api, precisamos subir um nível e entrar em Vion.Web
            // Nota: Isso assume a estrutura de pastas padrão do projeto.
            var webRootPath = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, "..", "Vion.Web", "wwwroot"));
            
            if (!Directory.Exists(webRootPath))
            {
                // Se não achar a pasta do Web, salva na pasta da API mesmo (fallback)
                webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
            }

            var uploadFolder = Path.Combine(webRootPath, "images", "products");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = Guid.NewGuid().ToString() + ext;
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Retorna a URL relativa que será salva no banco
            var relativeUrl = $"/images/products/{fileName}";
            return Ok(new { url = relativeUrl });
        }
    }
}
