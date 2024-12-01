using AutoMapper;



namespace E_COMMERCE_APP.Core.Mappings.Authentication
{
    public partial class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            AddLoginCommandMappings();
        }
    }
}
