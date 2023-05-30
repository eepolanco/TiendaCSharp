using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProductosController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ProductosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoListDto>>> Get()
    {
        var productos = await unitOfWork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoListDto>>(productos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var producto = await unitOfWork.Productos.GetByIdAsync(id);
        if (producto == null)
            return NotFound();


        return mapper.Map<ProductoDto>(producto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoAddUpdateDto>> Post(ProductoAddUpdateDto productoDto)
    {
        var producto = mapper.Map<Producto>(productoDto);
        unitOfWork.Productos.Add(producto);
        await unitOfWork.SaveAsync();

        if (producto == null)
        {
            return BadRequest();
        }
        productoDto.Id = producto.Id;
        return CreatedAtAction(nameof(Post), new { id = productoDto.Id }, productoDto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoAddUpdateDto>> Put(int id, [FromBody] ProductoAddUpdateDto productoDto)
    {
        if (productoDto == null)
            return NotFound();

        var producto = mapper.Map<Producto>(productoDto);
        unitOfWork.Productos.Update(producto);
        await unitOfWork.SaveAsync();

        return productoDto;
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var producto = await unitOfWork.Productos.GetByIdAsync(id);

        if (producto == null)
            return NotFound();

        unitOfWork.Productos.Remove(producto);
        await unitOfWork.SaveAsync();

        return NoContent();
    }
}
