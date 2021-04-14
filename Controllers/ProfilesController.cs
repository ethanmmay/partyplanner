using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using partyplanner.Models;
using partyplanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace partyplanner.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _service;
    private readonly PartiesService _partiesServ;

    public ProfilesController(ProfilesService service, PartiesService partiesServ)
    {
      _service = service;
      _partiesServ = partiesServ;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/parties")]
    public ActionResult<IEnumerable<PartyPartyMemberViewModel>> GetParties(string id)
    {
      try
      {
        return Ok(_partiesServ.GetByProfileId(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}