using AutoMapper;
using Estac.Domain.Dtos;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Output;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;
using Estac.Infra.Interface;
using Estac.Infra.Shared;
using Estac.Domain.Shared;
using Estac.Domain.Extensions;

namespace Estac.Service
{
    public class ImportaExcelServices : ServiceResult<Receita>, IImportarExcelServices
    {
        private readonly IReceitaRepositories _repo;
        private readonly IMapper _mapper;
        private readonly IExcel _excel;

        private static string CaminhoPlanilha  = "C:\\Users\\jeanc\\OneDrive\\Documentos\\Finanças-Excel.xlsx";
        public ImportaExcelServices(IReceitaRepositories repo,
                               IMapper mapper,
                               IErrorServices _errorApplication,
                               IExcel excel
                               ) : base(_errorApplication)
        {
            _repo = repo;
            _mapper = mapper;
            _excel = excel;
        }

        public async void ObterAsync()
        {
            var dataTable = _excel.LerPlanilha(CaminhoPlanilha, DataExtesions.ObterMesAtualString());
        }
    }
}