using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validations
{
    public class AiportCodesEqualityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return request?.From?.Airport?.Trim().ToLower() != request?.To?.Airport?.Trim().ToLower();
        }
    }
}
