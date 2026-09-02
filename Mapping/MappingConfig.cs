using Mapster;
using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<Poll, PollResponse>().Map(dest => dest.note, src => src.description);
        }

    }
}
