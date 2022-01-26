using AutoMapper;
using Ninject;

namespace KBZ.BLL
{
    //public static class AutoMapperBootstrapper
    //{
    //    public static void Initialize(IKernel kernel)
    //    {
    //        object p = Mapper.Initialize(cfg =>
    //        {
    //            cfg.ConstructServicesUsing(type => kernel.Get(type));                
    //        });

    //    }
    //}
    public  class AutoMapperBootstrapper
    {
        protected  IMapper _mapper;

        public void AutoMapperBase(IKernel kernel)
        {
            var config = new MapperConfiguration(x =>
            {
                x.ConstructServicesUsing(type => kernel.Get(type));
            });

            _mapper = config.CreateMapper();
        }
    }
}
