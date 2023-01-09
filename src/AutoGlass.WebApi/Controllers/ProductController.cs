using AutoGlass.Application.Interfaces;
using AutoGlass.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace AutoGlass.WebApi.Controllers;

[Route("products")]
public sealed class ProductController : ApiController
{
    private readonly IProductServices _productServices;

	public ProductController(IProductServices productServices)
	{
        _productServices = productServices;
    }

    /// <summary>
    /// Recupera todos os registros.
    /// </summary>
    /// <remarks>
    /// Exemplo de utilização:
    /// 
    /// $top => 10 |
    /// $skip => 10 |
    /// $filter => isActive eq true |
    /// $orderby => productionDate |
    /// $select => id, description 
    ///     
    /// </remarks>
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productServices.GetAll());
    }

    /// <summary>
    /// Recupera um registro por código
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _productServices.GetById(id));
    }

    /// <summary>
    /// Cria um produto
    /// </summary>
    /// <remarks>
    /// Exemplo de payload:
    /// 
    ///     {
    ///        "description": "Produto Amarelo",
    ///        "productionDate": "2023-12-16",
    ///        "expirationDate": "2023-12-20",
    ///        "isActive": true,
    ///        "provider": {
    ///             "description": "Fornecedor Ltda.",
    ///             "cnpj": "72356890000180"
    ///         }
    ///     }
    ///     
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RequestProductViewModel request)
    {
        var response = await _productServices.Create(request);

        return CustomResponse(response.Message);
    }

    /// <summary>
    /// Edita um produto
    /// </summary>
    /// <remarks>
    /// Exemplo de payload:
    /// 
    ///     {
    ///        "description": "Produto Amarelo",
    ///        "productionDate": "2023-12-16",
    ///        "expirationDate": "2023-12-20",
    ///        "isActive": true,
    ///        "provider": {
    ///             "description": "Fornecedor Ltda.",
    ///             "cnpj": "72356890000180"
    ///         }
    ///     }
    ///     
    /// </remarks>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] RequestProductViewModel request)
    {
        var response = await _productServices.Edit(id, request);

        return CustomResponse(response.Message);
    }

    /// <summary>
    /// Remove um produto, atualizando o campo Ativo para falso
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _productServices.Remove(id);

        return CustomResponse(response.Message);
    }
}