using Mapster;
using SurveyBasket.Contracts.Responses;
using SurveyBasket.Models;

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
