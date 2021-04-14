namespace partyplanner.Models
{
  public class PartyMember
  {
    public int Id { get; set; }
    public string MemberId { get; set; }
    public int PartyId { get; set; }
    public string CreatorId { get; set; }
  }
}