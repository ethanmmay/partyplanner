using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using partyplanner.Models;
using partyplanner.Services;

namespace partyplanner.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PartiesController : ControllerBase
  {
    private readonly PartiesService _service;

    public PartiesController(PartiesService service)
    {
      _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Party>> Get()
    {
      try
      {
        return Ok(_service.Get());
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Party>> Get(int id)
    {
      try
      {
        return Ok(_service.GetById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Party>> CreateAsync([FromBody] Party newParty)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newParty.CreatorId = userInfo.Id;
        newParty.Creator = userInfo;
        return Ok(_service.Create(newParty));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Party>> Edit([FromBody] Party updated, int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        updated.CreatorId = userInfo.Id;
        updated.Id = id;
        updated.Creator = userInfo;
        return Ok(_service.Edit(updated));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Party>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_service.Delete(id, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}