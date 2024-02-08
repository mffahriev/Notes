using Castle.Core.Configuration;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Services.CatalogServices
{
    public class ReaderCatalogContentDecorator : IReaderCatalogContent
    {
        private readonly IReaderCatalogContent _readerCatalogContentService;
        private readonly IConfiguration _configuration;
        private readonly IValidator<CatalogContentQueryDTO> _validator;

        public ReaderCatalogContentDecorator(
            IReaderCatalogContent readerCatalogContentService,
            IConfiguration configuration,
            IValidator<CatalogContentQueryDTO> validator
        )
        {
            _readerCatalogContentService = readerCatalogContentService;
            _configuration = configuration;
            _validator = validator;
        }

        public async Task<PageDTO<CatalogContentItemDTO>> GetCatalogContent(
            CatalogContentQueryDTO dto,
            string userId,
            CancellationToken cancellationToken
        )
        {
            await _validator.ValidateAndThrowAsync(dto, cancellationToken);

            return await _readerCatalogContentService.GetCatalogContent(
                new CatalogContentQueryDTO(
                    string.IsNullOrWhiteSpace(dto.FullPath) ? _configuration["DefaultCatalog"] : dto.FullPath, 
                      dto.PageNumber ?? Convert.ToInt32(_configuration["DefaultPageNamber"]),
                      dto.PageSize ?? Convert.ToInt32(_configuration["DefaultPageSize"])
                    ),
                userId,
                cancellationToken
            );
        }
    }
}
