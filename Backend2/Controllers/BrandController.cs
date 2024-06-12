using Backend2.DTOs.BrandDTOs;
using Backend2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IValidator<BrandInsertDTOs> _brandInsertValidator;
        private IValidator<BrandUpdateDto> _brandUpdateValidator;
        private ICommonService<BrandDTO, BrandInsertDTOs, BrandUpdateDto> _brandServices;

        public BrandController(
            IValidator<BrandInsertDTOs> brandInsertValidator,
            IValidator<BrandUpdateDto> brandUpdateValidator,
            [FromKeyedServices("brandService")] ICommonService<BrandDTO, BrandInsertDTOs, BrandUpdateDto> brandServices
            ) { 
            _brandInsertValidator = brandInsertValidator;
            _brandUpdateValidator = brandUpdateValidator;
            _brandServices = brandServices;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDTO>> Get() => await _brandServices.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDTO>> GetById(int id) { 
            var brandDto = await _brandServices.GetById(id);
            return brandDto == null ? NotFound(): Ok(brandDto);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDTO>> Add(BrandInsertDTOs brandInsertDTOs) { 
            var validatorResult = await _brandInsertValidator.ValidateAsync(brandInsertDTOs);
            if (!validatorResult.IsValid) {
                return BadRequest(validatorResult.Errors);
            }
            var brandDto = await _brandServices.Add(brandInsertDTOs);
            return CreatedAtAction(nameof(GetById), new { id = brandDto.Id}, brandDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BrandDTO>> Update(int id, BrandUpdateDto brandUpdateDto) { 
            var validatorResult = await _brandUpdateValidator.ValidateAsync(brandUpdateDto);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var brandDto = await _brandServices.Update(id, brandUpdateDto);
            return brandDto == null ? NotFound() : Ok(brandDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BrandDTO>> Delete(int id) { 
            var brandDto = await _brandServices.Delete(id);
            return brandDto == null ? NotFound(): Ok(brandDto);
        }

    }
}
