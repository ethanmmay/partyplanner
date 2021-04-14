using System;
using System.Data;
using Dapper;
using partyplanner.Models;

namespace partyplanner.Repositories
{
  public class PartyMembersRepository
  {
    private readonly IDbConnection _db;

    public PartyMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal void Create(PartyMember pm)
    {
      string sql = @"
        INSERT INTO partymembers
        (memberId, partyId, creatorId)
        VALUES
        (@MemberId, @PartyId, @CreatorId);";
      _db.Execute(sql, pm);
    }

    internal PartyMember GetById(int id)
    {
      string sql = "SELECT * FROM partymembers WHERE id = @id;";
      return _db.QueryFirstOrDefault<PartyMember>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM partymembers WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}