using RealEstate.Domain;

namespace RealEstate.Structure.Dto.Response
{
    public class ReferenceDataResponseDto
    {
        public List<Typology> Typologies { get; set; } = new();
        public List<RealEstateType> RealEstateTypes { get; set; } = new();
        public List<City> Cities { get; set; } = new();
        public List<Amenities> Amenities { get; set; } = new();
    }
}