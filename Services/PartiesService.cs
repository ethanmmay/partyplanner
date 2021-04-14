using System;
using System.Collections.Generic;
using System.Linq;
using partyplanner.Models;
using partyplanner.Repositories;

namespace partyplanner.Services
{
  public class PartiesService
  {
    private readonly PartiesRepository _repo;

    public PartiesService(PartiesRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Party> Get()
    {
      return _repo.Get();
    }

    internal Party GetById(int id)
    {
      Party data = _repo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid id");
      }
      return data;
    }

    internal Party Create(Party newParty)
    {
      return _repo.Create(newParty);
    }

    internal Party Edit(Party updated)
    {
      Party original = GetById(updated.Id);
      if (updated.CreatorId != original.CreatorId)
      {
        throw new Exception("You cannot edit this.");
      }
      updated.Name = updated.Name != null ? updated.Name : original.Name;
      return _repo.Edit(updated);
    }

    internal Party Delete(int id, string userId)
    {
      Party original = GetById(id);
      if (userId != original.CreatorId)
      {
        throw new Exception("You cannot delete this.");
      }
      _repo.Delete(id);
      return original;
    }

    internal IEnumerable<PartyPartyMemberViewModel> GetByProfileId(string id)
    {
      IEnumerable<PartyPartyMemberViewModel> parties = _repo.GetPartiesByProfileId(id);
      return parties.ToList().FindAll(p => p.Public);
    }

    internal IEnumerable<PartyPartyMemberViewModel> GetByAccountId(string id)
    {
      return _repo.GetPartiesByProfileId(id);
    }
  }
}