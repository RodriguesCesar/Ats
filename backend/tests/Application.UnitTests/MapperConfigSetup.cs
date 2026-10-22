using Totvs.Ats.Application.Common.Mappers;

namespace Totvs.Ats.Application.UnitTests;

public class MapperConfigSetup
{
    public MapperConfigSetup()
    {
        ApplicationMapperConfig.AddMappingConfigs();
    }
}