namespace FamilyArchiveBackend.Models
{
    public class MemberSpouse
    {
        public int Spouse1Id { get; set; }
        public Member? Spouse1 { get; set; } = null!;
        public int Spouse2Id { get; set; }
        public Member? Spouse2 { get; set; } = null!;
        public DateTime? MarriedAt { get; set; }
        public DateTime? DivorcedAt { get; set; }
    }
}
