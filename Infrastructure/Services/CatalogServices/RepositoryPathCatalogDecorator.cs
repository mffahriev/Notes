using Core.DTOs;
using Core.Interfaces;
using FluentValidation;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Services.CatalogServices
{
    public class RepositoryPathCatalogDecorator : IRepositoryPathCatalog
    {
        private readonly IRepositoryPathCatalog _repositoryService;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UserDataDTO<CatalogContentQueryDTO>> _validatorGetterContent;
        private readonly IValidator<UserDataDTO<CatalogInsertDTO>> _validatorInsert;
        private readonly IValidator<UserDataDTO<CatalogItemDTO>> _validatorCatalogItem;
        private readonly IValidator<UserDataDTO<CatalogUpdateDTO>> _validatorCatalogUpdate;

        public RepositoryPathCatalogDecorator(
            IRepositoryPathCatalog repositoryService,
            IConfiguration configuration,
            IValidator<UserDataDTO<CatalogContentQueryDTO>> validatorGetterContent,
            IValidator<UserDataDTO<CatalogItemDTO>> validatorCatalogItem,
            IValidator<UserDataDTO<CatalogUpdateDTO>> validatorCatalogUpdate,
            IValidator<UserDataDTO<CatalogInsertDTO>> validatorInsert
        )
        {
            _repositoryService = repositoryService;
            _configuration = configuration;
            _validatorGetterContent = validatorGetterContent;
            _validatorCatalogItem = validatorCatalogItem;
            _validatorCatalogUpdate = validatorCatalogUpdate;
            _validatorInsert = validatorInsert;
        }

        public async Task<PageDTO<CatalogContentItemDTO>> GetContent(
            UserDataDTO<CatalogContentQueryDTO> dto,
            CancellationToken cancellationToken
        )
        {
            await _validatorGetterContent.ValidateAndThrowAsync(dto, cancellationToken);

            return await _repositoryService.GetContent(
                new UserDataDTO<CatalogContentQueryDTO>(
                    new CatalogContentQueryDTO(
                    string.IsNullOrWhiteSpace(dto.Value.FullPath) 
                        ? _configuration["DefaultCatalog"] 
                        : dto.Value.FullPath,
                      dto.Value.PageNumber ?? Convert.ToInt32(_configuration["DefaultPageNamber"]),
                      dto.Value.PageSize ?? Convert.ToInt32(_configuration["DefaultPageSize"])
                    ),
                    dto.UserId
                ),
                cancellationToken
            );
        }
        public async Task Delete(
            UserDataDTO<CatalogItemDTO> dto,
            CancellationToken cancellationToken
        )
        {
            await _validatorCatalogItem.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryService.Delete(dto, cancellationToken);
        }

        public async Task Insert(
            UserDataDTO<CatalogInsertDTO> dto,
            CancellationToken cancellationToken
        )
        {
            await _validatorInsert.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryService.Insert(dto, cancellationToken);
        }

        public async Task Update(
            UserDataDTO<CatalogUpdateDTO> dto,
            CancellationToken cancellationToken
        )
        {
            await _validatorCatalogUpdate.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryService.Update(dto, cancellationToken);
        }
    }
}
