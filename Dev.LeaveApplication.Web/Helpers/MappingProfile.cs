using AutoMapper;
using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Web.Models;

namespace Dev.LeaveApplication.Web.Helpers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<FormEditViewModel, FormModel>();
	}
}
