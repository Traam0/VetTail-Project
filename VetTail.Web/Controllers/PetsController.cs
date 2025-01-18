using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Application.Common.DTOs.Pets;
using VetTail.Application.Common.DTOs.Wrappers;
using VetTail.Application.Common.Exceptions;
using VetTail.Application.Common.Interfaces;
using VetTail.Domain.Common.Exceptions;
using VetTail.Web.Models.Pets;

namespace VetTail.Web.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService petService;
        private readonly IClientService clientService;
        private readonly IMapper mapper;

        public PetsController(IPetService service, IClientService clientService, IMapper mapper)
        {
            this.petService = service;
            this.clientService = clientService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            PaginatedList<PetBriefDto> pets = await this.petService.GetAllPaginated(pageNumber, pageSize, cancellationToken);
            PetsVM viewModel = new()
            {
                Pets = pets,
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Create(CancellationToken cancellation = default)
        { 
            IEnumerable<ClientMacroDto> clients = await this.clientService.GetAllClientsMacroAsync(cancellation);
            ViewBag.Clients = clients;
            return View();
        }


        public async Task<IActionResult> Create(CreatePetVM viewModel,  CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            try
            {
                CreatePetDto dto = this.mapper.Map<CreatePetDto>(viewModel);
                Result<Guid> result = await this.petService.CreatePetAsync(dto, cancellationToken);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, result.Error);
                };

                return RedirectToAction("Index");
            }
            catch(ValidationFailureException execption)
            {
                foreach (KeyValuePair<string, string[]> item in execption.Errors) { 
                    foreach(string error in item.Value)
                    {
                        ModelState.AddModelError(item.Key, error);
                    }
                }
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Update(Guid guid, CancellationToken cancellationToken)
        {
            try
            {
                PetDto client = await this.petService.GetPetById(guid, cancellationToken);

                UpdatePetVM viewModel = this.mapper.Map<UpdatePetVM>(client);

                return View(viewModel);
            }
            catch(Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }


        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                Result result = await this.petService.DeletePetAsync(id, cancellationToken);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }catch(EntityNullReferenceException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
