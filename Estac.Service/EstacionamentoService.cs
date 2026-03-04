using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Estacionamento;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class EstacionamentoService : ServiceResult<EstacionamentoOutput>, IEstacionamentoService
    {
        private readonly IEstacionamentoRepositories _repositories;
        private readonly IMapper _mapper;

        public EstacionamentoService(IErrorServices _errorServices,
                               IEstacionamentoRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.SelecionarPorIdCompleto(id);

                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                return await RetornOk(_mapper.Map<EstacionamentoOutput>(result));
            }
            catch(Exception ex) 
            {   
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Buscar(EstacionamentoFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(EstacionamentoPostInput input)
        {
            try
            {
                //var validations = EstacionamentoPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Estacionamento>(input);
                ValoresPadrao(result);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> UploadFotos(EstacionamentoFotosInput input)
        {
            try
            {
                var fotos = new List<EstacionamentoFoto>();

                for (int i = 0; i < input.Fotos.Count(); i++)
                {
                    var arquivo = input.Fotos.ToArray()[i];

                    using var memoryStream = new MemoryStream();
                    await arquivo.CopyToAsync(memoryStream);

                    fotos.Add(new EstacionamentoFoto
                    {
                        EstacionamentoId = input.EstacionamentoId,
                        Foto = memoryStream.ToArray(),
                        Descricao = arquivo.FileName,
                        ContentType = arquivo.ContentType,
                        DataCriacao = DateTime.UtcNow,
                        TamanhoBytes = arquivo.Length,
                        Principal = input.PadraoIndex.HasValue && input.PadraoIndex.Value == i,
                        Ordem = input.Ordem.HasValue ? input.Ordem.Value + i : (int?)null,
                    });
                }

                await _repositories.UploadFotos(input.EstacionamentoId, fotos);

                return await RetornOk(true);
            }
            catch(Exception ex)
            {
                return await RetornNo(ex, "Ocorreu um erro ao salvar fotos.");
            }
        }

        public async Task<ActionResult> Alterar(EstacionamentoPutInput input)
        {
            try
            {
                //var validations = EstacionamentoPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Estacionamento>(input);
                ValoresPadrao(result);

                await _repositories.Alterar(result);

                return await RetornOk(await _repositories.Alterar(result));
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            var result = await _repositories.Existe(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var estacionamento = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

        public async Task<ActionResult> BuscarFotos(int id)
        {
            try
            {
               var resultado = await _repositories.ListarFotosPorEstacionamentoAsNoTracking(id);

               return await RetornOk(resultado);
            }
            catch (Exception ex)
            {
                return await RetornNo(ex, "Ocorreu um erro ao deletar fotos.");
            }
        }

        public async Task<ActionResult> ExcluirFotos(int fotoId)
        {
            try
            {
                await _repositories.ExcluirFotos(fotoId);

                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                return await RetornNo(ex, "Ocorreu um erro ao deletar fotos.");
            }
        }

        private static void ValoresPadrao(Estacionamento result)
        {
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Juridica);
            result.Pessoa.AdicionarPapel(TipoPapel.Estacionamento);
        }
    }
}
