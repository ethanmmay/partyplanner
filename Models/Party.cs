namespace partyplanner.Models
{
  public class Party
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public bool Public { get; set; }
    public Profile Creator { get; set; }
  }

  public class PartyPartyMemberViewModel : Party
  {
    public int PartyMemberId { get; set; }
  }
}