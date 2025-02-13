﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using ProiectPractica.Services;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CodeSnippetsController : ControllerBase
    {
        private readonly ILogger<CodeSnippetsController> _logger;
        private readonly ICodeSnippetsService _codeSnippetService;
        public CodeSnippetsController(ILogger<CodeSnippetsController> logger, ICodeSnippetsService codeSnippetService)
        {
            _logger = logger;
            _codeSnippetService = codeSnippetService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            DbSet<CodeSnippet> codeSnippets = _codeSnippetService.Get();
            if (codeSnippets != null) 
                if (codeSnippets.ToList().Count > 0)
                {
                    return StatusCode(200, _codeSnippetService.Get());
                }
            return StatusCode(404);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodeSnippet codeSnippet) //adauga inregistrare in tabel
        {
            try
            {
                if (codeSnippet != null) {
                    _codeSnippetService.Post(codeSnippet);
                    return StatusCode(201, Constants.CreateCodeSnippet);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CodeSnippet codeSnippet) //updateaza inregistrare in tabel
        {
            try
            {
                if (codeSnippet != null)
                {
                    _codeSnippetService.Put(codeSnippet);
                    return StatusCode(204, Constants.UpdateCodeSnippet);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] CodeSnippet codeSnippet) //sterge inregistrare in tabel
        {
            try
            {

                if (codeSnippet != null)
                {
                    _codeSnippetService.Delete(codeSnippet);
                    return StatusCode(204, Constants.DeleteCodeSnippet);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
