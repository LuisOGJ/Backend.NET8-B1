using Backend2.DTOs;
using Backend2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;


        // CONSTRUCTOR
        /*
         StoreContext have methods for interact whith DB
         */
        public BeerController(
            IValidator<BeerInsertDto> beerInsertValidator, 
            IValidator<BeerUpdateDto> beerUpdateValidator,
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService
            )
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }


        // GET ALL BEER
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();


        // GET BY ATTRIBUTE
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id) {
            var beetDto = await _beerService.GetById(id);
            return beetDto == null ? NotFound() : Ok(beetDto);
        }



        // CREATE BEER
        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto) {

            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid) { 
                return BadRequest(validationResult.Errors);
            }

            if (!_beerService.Validate(beerInsertDto)) {
                return BadRequest(_beerService.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDto);

            

            // ADDING INFORMATION AT THE HEADER
            // nameof(GetByID) return path for get the object
            // new { id = beer.BeerID } return id of the beer created
            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id}, beerDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto) {
            
            var validatioResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validatioResult.IsValid) {
                return BadRequest(validatioResult.Errors);
            }

            if (!_beerService.Validate(beerUpdateDto)) {
                return BadRequest(_beerService.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdateDto);
            return beerDto == null ? NotFound() : Ok(beerDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id) {

            var beerDto = await _beerService.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);

        }

    }
}
