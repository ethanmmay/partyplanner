using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using partyplanner.Models;
using partyplanner.Services;

namespace partyplanner.Controllers
{
  [ApiController]
  [Route("[controller]")]
  // REVIEW[epic=Authentication] this tag enforces the user must be logged in
  [Authorize]
  public class AccountController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly PartiesService _partiesServ;

    public AccountController(ProfilesService ps, PartiesService partiesServ)
    {
      _ps = ps;
      _partiesServ = partiesServ;
    }

    [HttpGet]
    // REVIEW[epic=Authentication] async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        // REVIEW[epic=Authentication] how to get the user info from the request token
        // same as to req.userInfo
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("parties")]
    public async Task<ActionResult<IEnumerable<PartyPartyMemberViewModel>>> GetPartiesAsync()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_partiesServ.GetByAccountId(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}