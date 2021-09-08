using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersController(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna datalhes de usuário
        /// </summary>
        /// <param name="id">Identificador de usuário</param>
        /// <returns>Objeto de detalhes de usuário</returns>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="200">Usuário encontrado com sucesso</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Cadastrar um usuário
        /// </summary>
        /// <remarks>
        /// Requesição de exemplo: 
        /// {
        /// "fullName": "Mateus Gouveia",
        /// "height": 1.80,
        /// "weight" 80,
        /// "birthDate": 1996-01-01 00:00:00
        /// }
        /// </remarks>
        /// <param name="inputModel">Objeto com dados de cadastro do Usuário</param>
        /// <returns>Objeto recém-criado.</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <reponse code="400">Dados Inválidos</reponse>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateUserInputModel inputModel)
        {
            // var user = new User(inputModel.FullName, inputModel.Height, inputModel.Weight, inputModel.BirthDate);
            var user = _mapper.Map<User>(inputModel);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users
                .SingleOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            user.Update(inputModel.Weight, inputModel.Height);

            _dbContext.SaveChanges();

            return NoContent();

        }

        
    }
}
