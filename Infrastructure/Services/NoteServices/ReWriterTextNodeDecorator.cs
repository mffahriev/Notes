using Core.DTOs;
using Core.Interfaces;
using FluentValidation;

namespace Infrastructure.Services.NoteServices
{
    public class ReWriterTextNodeDecorator : IReWriterTextNote
    {
        private readonly IReWriterTextNote _rewriter;
        private readonly IValidator<UserDataDTO<NoteUpdateContentDTO>> _validator;

        public ReWriterTextNodeDecorator(
            IReWriterTextNote rewriter,
            IValidator<UserDataDTO<NoteUpdateContentDTO>> validator
        )
        {
            _rewriter = rewriter;
            _validator = validator;
        }

        public async Task ReWriteText(UserDataDTO<NoteUpdateContentDTO> dto, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(dto, cancellationToken);
            await _rewriter.ReWriteText(dto, cancellationToken);
        }
    }
}
