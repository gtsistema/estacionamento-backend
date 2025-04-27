using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;
using Gp.Infra.Interface;
using Gp.Infra.Shared;
using Gp.Domain.Shared;
using Gp.Domain.Extensions;

namespace Gp.Service
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