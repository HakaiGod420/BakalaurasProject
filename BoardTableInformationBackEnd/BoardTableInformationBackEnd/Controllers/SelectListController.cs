using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/selectList")]
    public class SelectListController : Controller
    {
        private readonly ISelectListService _selectListService;

        public SelectListController(ISelectListService selectListService)
        {
            _selectListService = selectListService;
        }

        [HttpGet("typesAndCategories")]
        [ProducesResponseType(typeof(TypesAndCategoriesDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTypesAndCategories()
        {
            var result = await _selectListService.GetTypesAndCategories();
            return Ok(result);
        }
    }
}
