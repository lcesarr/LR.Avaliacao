using AutoMapper;
using LR.Avaliacao.Application.Mapping;

namespace LR.Avaliacao.Tests.Mapper.Fixture
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new ClienteMapper());
            });

            Mapper = config.CreateMapper();
        }
    }
}
