using Elmah.Io.AspNetCore;
using TesteArquitetura.Documentos.Application.Services.Interfaces;
using TesteArquitetura.Documentos.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteArquitetura.WebApi.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FluxoCaixaController : Controller
    {
        private readonly IFluxoCaixaAppService _service;

        public FluxoCaixaController(IFluxoCaixaAppService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("consolidado-diario")]
        public async Task<IEnumerable<FluxoCaixaViewModel>> ConsolidadoDiario()
        {
            try
            {
                var obj = await _service.GetAll();
                return obj ?? Enumerable.Empty<FluxoCaixaViewModel>();
            }
            catch (Exception ex)
            {
                ElmahIoApi.Log(ex, HttpContext);
                return Enumerable.Empty<FluxoCaixaViewModel>();
            }
        }

        [AllowAnonymous]
        [HttpPost("controle-lancamentos")]
        public async Task<FluxoCaixaViewModel> ControleLançamentos(FluxoCaixaViewModel viewModel)
        {
            try
            {
                var obj = await _service.SaveAsync(viewModel);
                return obj ?? new FluxoCaixaViewModel();
            }
            catch (Exception ex)
            {
                ElmahIoApi.Log(ex, HttpContext);
                return new FluxoCaixaViewModel();
            }
        }
    }
}
