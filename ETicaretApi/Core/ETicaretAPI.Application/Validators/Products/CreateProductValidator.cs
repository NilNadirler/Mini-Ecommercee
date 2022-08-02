
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lutfen urun adini bos gecmeyiniz")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Lutden urun adi 5-10");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lutfen stok bilgisini bos gecmeyiniz")
                .Must(s => s >= 0)
                .WithMessage("Fiyat bilgisi 0 dan buyuk olmali");

            RuleFor(p => p.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Lutfen fiyat bilgisini bos gecmeyiniz")
               .Must(s => s >= 0)
               .WithMessage("Fiyat fiyat 0 dan buyuk olmali");
        }
    }
}
