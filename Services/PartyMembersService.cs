using System;
using partyplanner.Models;
using partyplanner.Repositories;

namespace partyplanner.Services
{
  public class PartyMembersService
  {
    private readonly PartyMembersRepository _repo;
    private readonly PartiesRepository _partiesrepo;

    public PartyMembersService(PartyMembersRepository repo, PartiesRepository partiesrepo)
    {
      _repo = repo;
      _partiesrepo = partiesrepo;
    }

    internal string Create(PartyMember pm)
    {
      Party party = _partiesrepo.GetById(pm.PartyId);
      if (party == null)
      {
        throw new Exception("Not a valid party");
      }
      if (party.CreatorId != pm.CreatorId)
      {
        throw new Exception("You are not the owner of this party");
      }
      _repo.Create(pm);
      return "Created";
    }

    internal void Delete(int id, string userId)
    {
      PartyMember member = _repo.GetById(id);
      if (member == null)
      {
        throw new Exception("Invalid member");
      }
      if (member.CreatorId != userId)
      {
        throw new Exception("Invalid User");
      }
      _repo.Delete(id);
    }
  }
}