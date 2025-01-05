using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public record GetQuoteRequest([FromQuery]string CryptoCode)
{
    public class Validator : AbstractValidator<GetQuoteRequest>
    {
        public Validator()
        {
            RuleFor(request => request.CryptoCode).NotEmpty().WithMessage("Invalid Crypto Code");
        }
    }
}
