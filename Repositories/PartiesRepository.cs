using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using partyplanner.Models;

namespace partyplanner.Repositories
{
  public class PartiesRepository
  {
    private readonly IDbConnection _db;

    public PartiesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Party> Get()
    {
      string sql = @"
      SELECT
      p.*,
      pr.*
      FROM parties p
      JOIN profiles pr ON p.creatorId = pr.id";
      return _db.Query<Party, Profile, Party>(sql, (party, profile) =>
      {
        party.Creator = profile;
        return party;
      }, splitOn: "id");
    }

    internal Party GetById(int id)
    {
      string sql = @"
      SELECT 
      part.*,
      pr.*
      FROM parties part
      JOIN profiles pr ON part.creatorId = pr.id
      WHERE part.id = @id;";
      return _db.Query<Party, Profile, Party>(sql, (party, profile) =>
      {
        party.Creator = profile;
        return party;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Party Create(Party newParty)
    {
      string sql = @"
      INSERT INTO parties
      (creatorId, name, public)
      VALUES
      (@CreatorId, @Name, @Public);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newParty);
      newParty.Id = id;
      return newParty;
    }

    internal IEnumerable<PartyPartyMemberViewModel> GetPartiesByProfileId(string id)
    {
      string sql = @"
      SELECT
      party.*,
      pm.id AS PartyMemberId,
      pr.*
      FROM partymembers pm
      JOIN parties party ON pm.partyId = party.id
      JOIN profiles pr ON pr.id = party.creatorId
      WHERE pm.memberId = @id;";

      return _db.Query<PartyPartyMemberViewModel, Profile, PartyPartyMemberViewModel>(sql, (party, profile) =>
      {
        party.Creator = profile;
        return party;
      }, new { id }, splitOn: "id");
    }

    internal Party Edit(Party updated)
    {
      string sql = @"
        UPDATE parties
        SET
            name = @Name
        WHERE id = @Id;";
      _db.Execute(sql, updated);
      return updated;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM parties WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}