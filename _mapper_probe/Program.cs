using AutoMapper;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Mappers.Auth;
using Estac.Domain.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

var json = """
{
    "id": 1030,
    "transportadoraId": 1,
    "cnh": "70165178027",
    "pessoaId": 2051,
    "pessoaFisica": {
        "id": 0,
        "nome": "Elder AAS",
        "cpf": "70165178027",
        "ativo": true,
        "enderecos": [
            {
                "id": 0,
                "pessoaId": 2051,
                "principal": true,
                "tipoEndereco": 1,
                "cep": "",
                "logradouro": "",
                "numero": "",
                "complemento": "",
                "bairro": "",
                "cidade": "",
                "estado": ""
            }
        ],
        "contatos": [
            {
                "id": 0,
                "pessoaId": 2051,
                "descricao": "Elder AAS",
                "cpf": "70165178027",
                "telefone": "44912312312",
                "celular": "44912312312",
                "email": "eduardoaugusto@mpc.com.br",
                "principal": true,
                "observacao": ""
            }
        ]
    },
    "validadeCNH": "2030-07-30T12:00:00.000Z"
}
""";

var input = JsonConvert.DeserializeObject<MotoristaPutInput>(json);
Console.WriteLine($"INPUT Contatos count: {input.PessoaFisica?.Contatos?.Count()}");
Console.WriteLine($"INPUT Enderecos count: {input.PessoaFisica?.Enderecos?.Count()}");

var cfg = new MapperConfiguration(c =>
{
    c.AddProfile<MotoristaProfile>();
    c.AddProfile<PessoaProfile>();
}, NullLoggerFactory.Instance);

try
{
    cfg.AssertConfigurationIsValid();
    Console.WriteLine("AssertConfigurationIsValid: OK");
}
catch (Exception ex)
{
    Console.WriteLine("AssertConfigurationIsValid FAILED:");
    Console.WriteLine(ex.Message);
}

var mapper = cfg.CreateMapper();

try
{
    var motorista = mapper.Map<Motorista>(input);
    Console.WriteLine($"MAP Contatos count: {motorista.Pessoa?.Contatos?.Count}");
    Console.WriteLine($"MAP Enderecos count: {motorista.Pessoa?.Enderecos?.Count}");

    if (motorista.Pessoa?.Contatos?.FirstOrDefault() is { } ctt)
        Console.WriteLine($"MAP Contato: Desc={ctt.Descricao}, Tel={ctt.Telefone}, Email={ctt.Email}");

    if (motorista.Pessoa?.Enderecos?.FirstOrDefault() is { } end)
        Console.WriteLine($"MAP Endereco: Tipo={end.TipoEndereco}, Principal={end.Principal}");
}
catch (Exception ex)
{
    Console.WriteLine("MAP FAILED:");
    Console.WriteLine(ex.ToString());
}

try
{
    var pessoa = mapper.Map<Pessoa>(input.PessoaFisica);
    Console.WriteLine($"DIRECT Contatos: {pessoa.Contatos?.Count}, Enderecos: {pessoa.Enderecos?.Count}");
}
catch (Exception ex)
{
    Console.WriteLine("DIRECT MAP FAILED:");
    Console.WriteLine(ex.ToString());
}
