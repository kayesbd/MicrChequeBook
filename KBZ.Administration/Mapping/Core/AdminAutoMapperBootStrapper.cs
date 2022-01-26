using AutoMapper;

namespace KBZ.Administration.Mapping.Core 
{
	public partial class AdminAutoMapperBootStrapper : Profile
	{
		public static void Initialize()
        {
            Mapper.AddProfile(new AdminDatabaseToDomainProfile());
            Mapper.AddProfile(new AdminDomainToDatabaseProfile());

           
        }
	}
}

