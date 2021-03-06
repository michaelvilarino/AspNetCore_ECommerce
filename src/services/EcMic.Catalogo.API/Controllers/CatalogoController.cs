﻿using EcMic.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMic.WebApi.Core.Controllers;

namespace EcMic.Catalogo.API.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository IProdutoRepository)
        {
            _produtoRepository = IProdutoRepository;
        }

       
        [HttpGet("catalogo/produtos")]
        public async Task<PagedResult<Produto>> Index([FromQuery] int ps = 8, 
                                                      [FromQuery] int page = 1, 
                                                      [FromQuery] string q = null)
        {
            return await _produtoRepository.ObterTodos(ps, page, q);
        }

        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {            
            return await _produtoRepository.ObterPorId(id);
        }

        [HttpGet("catalogo/produtos/lista/{ids}")]
        public async Task<IEnumerable<Produto>> ObterProdutosPorId(string ids)
        {
            return await _produtoRepository.ObterProdutosPorId(ids);
        }

    }
}
