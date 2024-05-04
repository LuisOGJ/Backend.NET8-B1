﻿using Backend2.DTOs;
using Backend2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        public StoreContext _context;

        // CONSTRUCTOR
        /*
         StoreContext have methods for interact whith DB
         */
        public BeerController(StoreContext context)
        {
            _context = context;
        }


        // GET ALL BEER
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beer.Select(b => new BeerDto
        {
            Id = b.BeerID,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandID,
        }).ToListAsync();


        // GET BY ATTRIBUTE
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id) {

            // Get response, cant be the object or null
            var beer = await _context.Beer.FindAsync(id);

            // validate
            if (beer == null) {
                return NotFound();
            }

            // Preparate Dto Object
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID,
            };

            // response and status code
            return Ok(beerDto);
        }



        // CREATE BEER
        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto) {

            // SAVE IN A OBJECT BEER
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            // ADDING TO DB
            await _context.Beer.AddAsync(beer); // agrega los datos faltantes a beer y se puede usar lineas abajo
            await _context.SaveChangesAsync(); // para que se representen los datos en la db

            // USING DTO FOR RETURN DATA
            var beerDto = new BeerDto()
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol,
            };

            // ADDING INFORMATION AT THE HEADER
            // nameof(GetByID) return path for get the object
            // new { id = beer.BeerID } return id of the beer created
            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID}, beerDto);
        }

    }
}
