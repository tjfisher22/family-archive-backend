namespace FamilyArchiveBackend.Models
{
    public class MemberRelationship
    {
        public int ParentId { get; set; }
        public Member Parent{ get; set; } = null!;
        public int ChildId { get; set; }
        public Member Child { get; set; } = null!;
    }
}
