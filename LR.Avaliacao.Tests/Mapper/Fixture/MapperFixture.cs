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
                opts.AddProfile(new OperadorMapper());
                opts.AddProfile(new UsuarioMapper());
                opts.AddProfile(new MarcaMapper());
                opts.AddProfile(new ModeloMapper());
            });

            Mapper = config.CreateMapper();
        }
    }
}
