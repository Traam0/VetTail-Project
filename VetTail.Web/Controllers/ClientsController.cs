using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Application.Common.DTOs.Wrappers;
using VetTail.Application.Common.Exceptions;
using VetTail.Application.Common.Interfaces;
using VetTail.Domain.Common.Exceptions;
using VetTail.Web.Models.Clients;

namespace VetTail.Web.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService clientService;
    private readonly IMapper mapper;

    public ClientsController(IClientService clientService, IMapper mapper)
    {
        this.clientService = clientService;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        PaginatedList<ClientBriefDto> clients = await this.clientService.GetAllPaginatedAsync(pageNumber, pageSize, cancellationToken);

        ClientsVM viewModel = new()
        {
            Clients = clients
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Show(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            ClientDto client = await this.clientService.GetClientByIdAsync(id, cancellationToken);
            ClientVM viewModel = this.mapper.Map<ClientVM>(client);
            return View(viewModel);
        }
        catch(EntityNullReferenceException exception)
        {
            return NotFound(exception.Message);
        }
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateClientVM viewModel, CancellationToken cancellation = default)
    {
        if (!ModelState.IsValid) {
            return View(viewModel);
        }

        try
        {
            CreateClientDto dto = this.mapper.Map<CreateClientDto>(viewModel);
            Result<Guid> result = await this.clientService.CreateClientAsync(dto, cancellation);

            if (result.Succeeded) {
                return RedirectToAction(nameof(Index));
            }
            
            ModelState.AddModelError(string.Empty, result.Error);
            return View(viewModel);
            
        }
        catch(ValidationFailureException exception)
        {
            foreach (KeyValuePair<string, string[]> item in exception.Errors) {
                foreach (string error in item.Value) {
                    ModelState.AddModelError(item.Key, error);
                }
            }

            return View(viewModel);
        }
    }


    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            ClientDto client = await this.clientService.GetClientByIdAsync(id, cancellationToken);
            UpdateClientVM viewModel =  this.mapper.Map<UpdateClientVM>(client);
            return View(viewModel);
        }
        catch (EntityNullReferenceException exception)
        {
            return NotFound(exception.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Update(UpdateClientVM viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            UpdateClientDto dto = this.mapper.Map<UpdateClientDto>(viewModel);
            Result<Guid> result = await this.clientService.UpdateClientAsync(dto, cancellationToken);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index), new { L = result.Value });
            }
            ModelState.AddModelError(string.Empty, result.Error);
            return View(viewModel);
        }
        catch(EntityNullReferenceException exception)
        {
            return NotFound(exception.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Result result = await this.clientService.DeleteClientAsync(id, cancellationToken);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }


            return StatusCode(StatusCodes.Status500InternalServerError, "Oops something went wrong!");
        }
        catch (EntityNullReferenceException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
