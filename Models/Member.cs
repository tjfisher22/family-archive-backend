namespace FamilyArchiveBackend.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? MaidenName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? Gender { get; set; }

        // Owner relationship
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }

        // Self-relations
        public ICollection<MemberRelationship> Parents { get; set; } = new List<MemberRelationship>();
        public ICollection<MemberRelationship> Children { get; set; } = new List<MemberRelationship>();
        public ICollection<MemberSpouse> Spouses1 { get; set; } = new List<MemberSpouse>();
        public ICollection<MemberSpouse> Spouses2 { get; set; } = new List<MemberSpouse>();

        //Used to query all spouses of a member
        public IEnumerable<Member> Spouses =>
            Spouses1.Select(s => s.Spouse2).Concat(Spouses2.Select(s => s.Spouse1));
    }
}
