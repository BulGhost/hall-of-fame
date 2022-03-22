using AutoMapper;

namespace HallOfFame.BusinessLogic.Common.Mappings;

public interface IMappable
{
    void Mapping(Profile profile);
}