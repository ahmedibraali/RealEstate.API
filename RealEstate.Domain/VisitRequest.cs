using RealEstate.Domain.Base;

namespace RealEstate.Domain
{
    public class VisitRequest : ReferenceData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int FkRealEstateId { get; set; }
    }
}
